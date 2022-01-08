using System.Security.Claims;
using Primeflix.Application.Common.Models;
using Primeflix.Application.Products.Queries;
using Microsoft.AspNetCore.Mvc;
using Primeflix.Application.Products.Models;

namespace Primeflix.WebUI.Controllers;

public class ProductsController : ApiControllerBase
{
    [HttpGet("GetAllProducts")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts() =>
        await Mediator.Send(new GetAllProductsQuery());

    [HttpGet("GetProductsWithPagination")]
    public async Task<ActionResult<PaginatedList<ProductDto>>> GetProductsWithPagination(
        [FromQuery] GetProductsWithPaginationQuery query) => 
        await Mediator.Send(query);

    
}
