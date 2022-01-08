using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Primeflix.Application.Common.Interfaces;
using Primeflix.Application.Products.Models;

namespace Primeflix.Application.Products.Queries;

public class GetAllProductsQuery : IRequest<List<ProductDto>>
{
}

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllProductsQueryHandler(
        IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var items = await _context.Products.OrderBy(x => x.Title)
                                           .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                                           .ToListAsync(cancellationToken);

        return items;
    }


}