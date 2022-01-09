using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Primeflix.Application.Account.Models;
using Primeflix.Application.Common.Exceptions;
using Primeflix.Application.Common.Interfaces;

namespace Primeflix.Application.Account.Queries;

public class GetCurrentUserInfoQuery : IRequest<PrimeflixUserDto>
{
}

public class GetCurrentUserInfoQueryHandler : IRequestHandler<GetCurrentUserInfoQuery, PrimeflixUserDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public GetCurrentUserInfoQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService,
        IMapper mapper)
    {
        _context = context;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<PrimeflixUserDto> Handle(GetCurrentUserInfoQuery request, CancellationToken cancellationToken)
    {
        var currentUserNameIdentifier = _currentUserService.UserId;

        if (string.IsNullOrEmpty(currentUserNameIdentifier))
            throw new UserNotFoundException("Cannot find current user");

        var user = await _context.Users.Where(x => x.NameIdentifier == currentUserNameIdentifier)
            .ProjectTo<PrimeflixUserDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);


        return user ?? new PrimeflixUserDto();
    }
}