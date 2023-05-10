using GarageGenius.Modules.Users.Core.Commands.SignUp;
using GarageGenius.Modules.Users.Core.Queries.SignIn;
using GarageGenius.Shared.Abstractions.Dispatcher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GarageGenius.Modules.Users.Api.Controllers;
public class AccountController : BaseController
{
    private readonly IDispatcher _dispatcher;

    public AccountController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpGet("health-check")]
    public ActionResult<string> HealthCheck()
    {
        var response = new { message = "Users module - I'm alive." };
        return Ok(response);
    }

    [HttpPost("sign-up")]
    [AllowAnonymous]
    [SwaggerOperation("Sign up")]
    public async Task<ActionResult> SignUpAsync(SignUpCommand signUpCommand)
    {
        await _dispatcher.SendAsync<SignUpCommand>(signUpCommand);
        return NoContent();
    }

    [HttpPost("sign-in")]
    [AllowAnonymous]
    [SwaggerOperation("Sign in")]
    public async Task<ActionResult<string>> SignInAsync(SignInQuery signInQuery)
    {
        string token = await _dispatcher.QueryAsync<string>(signInQuery);
        return Ok(token);
    }
}
