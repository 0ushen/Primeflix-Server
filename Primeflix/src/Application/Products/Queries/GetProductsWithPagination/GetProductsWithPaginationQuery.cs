using System.Text.Json;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Primeflix.Application.Common.Interfaces;
using Primeflix.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Primeflix.Application.OMDB.Models;
using Primeflix.Application.Products.Models;

namespace Primeflix.Application.Products.Queries.GetProductsWithPagination;

public class GetProductsWithPaginationQuery : IRequest<PaginatedList<ProductDto>>
{
    public int ListId { get; set; }
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

        //using StreamReader r = new("C:\\repos\\Primeflix-Web\\Primeflix\\src\\assets\\data\\products2.json");

        //var json = await r.ReadToEndAsync();

        //var items = JsonSerializer.Deserialize<List<ProductDto>>(json, new JsonSerializerOptions
        //{
        //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        //});

        var items = await _context.Products.AsNoTracking()
                                           .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                                           .ToListAsync(cancellationToken);

        return new PaginatedList<ProductDto>(items, 5, 1, 8);
    }

    
}
