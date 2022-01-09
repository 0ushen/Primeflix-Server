using Microsoft.AspNetCore.Mvc;
using Primeflix.Application.Common.Models;
using Primeflix.Application.Products.Models;
using Primeflix.Application.Products.Queries;

namespace Primeflix.WebUI.Controllers;

public class ProductsController : ApiControllerBase
{
    [HttpGet("GetAllProducts")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
    {
        return await Mediator.Send(new GetAllProductsQuery());
    }

    [HttpGet("GetProductsWithPagination")]
    public async Task<ActionResult<PaginatedList<ProductDto>>> GetProductsWithPagination(
        [FromQuery] GetProductsWithPaginationQuery query)
    {
        return await Mediator.Send(query);
    }
}