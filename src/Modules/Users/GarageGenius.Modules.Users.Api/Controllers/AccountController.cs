using GarageGenius.Modules.Users.Core.Commands.SignUp;
using GarageGenius.Shared.Abstractions.Dispatcher;
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
        await _dispatcher.SendAsync(signUpCommand);
        return NoContent();
    }
}
