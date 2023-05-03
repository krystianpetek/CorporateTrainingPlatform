using GarageGenius.Modules.Users.Core.Commands.SignIn;
using GarageGenius.Modules.Users.Core.Commands.SignUp;
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

    [HttpPost("sign-up")]
    [SwaggerOperation("Sign up")]
    public async Task<ActionResult> SignUpAsync(SignUpCommand signUpCommand)
    {
        await _dispatcher.SendAsync<SignUpCommand>(signUpCommand);
        return NoContent();
    }

    [HttpPost("sign-in")]
    [AllowAnonymous]
    [SwaggerOperation("Sign in")]
    public async Task<ActionResult> SignInAsync(SignInCommand signInCommand)
    {
        await _dispatcher.SendAsync<SignInCommand>(signInCommand);
        return Ok();
    }

    [HttpGet("health-check")]
    public async Task<ActionResult<string>> HealthCheck()
    {
        var json = new { message = "Users module - I'm alive." };
        return Ok(json);
    }
}
