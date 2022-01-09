using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Primeflix.Application.Account.Commands;
using Primeflix.Application.Account.Models;
using Primeflix.Application.Account.Queries;

namespace Primeflix.WebUI.Controllers;

[Authorize]
public class AccountController : ApiControllerBase
{
    [HttpPost("UpsertUser")]
    public async Task<ActionResult> UpsertUser(UpsertUserCommand command)
    {
        await Mediator.Send(command);

        return NoContent();
    }

    [HttpGet("GetCurrentUserInfo")]
    public async Task<ActionResult<PrimeflixUserDto>> GetCurrentUserInfo()
    {
        return await Mediator.Send(new GetCurrentUserInfoQuery());
    }
}