using System.Security.Claims;
using Primeflix.Application.Common.Models;
using Primeflix.Application.Products.Queries;
using Primeflix.Application.Products.Queries.GetProductsWithPagination;
using Microsoft.AspNetCore.Mvc;
using Primeflix.Application.Products.Models;

namespace Primeflix.WebUI.Controllers;

public class ProductsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<ProductDto>>> GetProductsWithPagination(
        [FromQuery] GetProductsWithPaginationQuery query) => 
        await Mediator.Send(query);
}
