using AutoMapper;
using MediatR;
using Primeflix.Application.Common.Exceptions;
using Primeflix.Application.Common.Interfaces;
using Primeflix.Application.Common.Models;
using Primeflix.Domain.Entities;

namespace Primeflix.Application.Account.Commands;

public class UpsertUserCommand : IRequest
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PhoneNumber { get; set; }

    public AddressDto Address { get; set; }
}

public class UpsertUserCommandHandler : IRequestHandler<UpsertUserCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public UpsertUserCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUserService,
        IMapper mapper)
    {
        _context = context;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpsertUserCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId;

        if (string.IsNullOrEmpty(currentUserId))
            throw new UserNotFoundException("Cannot find current user");

        var currentUserInfo = _context.Users.FirstOrDefault(x => x.NameIdentifier == currentUserId);

        if (currentUserInfo is null)
        {
            var newUser = _mapper.Map<UpsertUserCommand, PrimeflixUser>(request);
            newUser.NameIdentifier = currentUserId;

            await _context.Users.AddAsync(newUser, cancellationToken);
        }
        else
        {
            _mapper.Map(request, currentUserInfo);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}