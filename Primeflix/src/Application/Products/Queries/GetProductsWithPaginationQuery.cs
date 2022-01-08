using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Primeflix.Application.Common.Interfaces;
using Primeflix.Application.Common.Mappings;
using Primeflix.Application.Common.Models;
using Primeflix.Application.Products.Models;

namespace Primeflix.Application.Products.Queries;

public class GetProductsWithPaginationQuery : IRequest<PaginatedList<ProductDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetProductsWithPaginationQueryHandler : IRequestHandler<GetProductsWithPaginationQuery, PaginatedList<ProductDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProductsWithPaginationQueryHandler(
        IApplicationDbContext context, 
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ProductDto>> Handle(GetProductsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var items = await _context.Products.OrderBy(x => x.Title)
                                           .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                                           .PaginatedListAsync(request.PageNumber, request.PageSize);

        return items;
    }

    
}
