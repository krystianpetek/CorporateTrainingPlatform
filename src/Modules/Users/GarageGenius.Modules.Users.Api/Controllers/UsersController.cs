using GarageGenius.Modules.Users.Core.Commands.DeactivateUser;
using GarageGenius.Modules.Users.Core.Commands.SignIn;
using GarageGenius.Modules.Users.Core.Commands.SignUp;
using GarageGenius.Modules.Users.Core.Queries.GetUser;
using GarageGenius.Shared.Abstractions.Authentication.JsonWebToken;
using GarageGenius.Shared.Abstractions.Authentication.JsonWebToken.Models;
using GarageGenius.Shared.Abstractions.Dispatcher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GarageGenius.Modules.Users.Api.Controllers;
public class UsersController : BaseController
{
    private readonly IJsonWebTokenStorage _jsonWebTokenStorage;

    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //[ProducesResponseType(StatusCodes.Status403Forbidden)]

    private readonly IDispatcher _dispatcher;

    public UsersController(
        IJsonWebTokenStorage jsonWebTokenStorage,
        IDispatcher dispatcher)
    {
        _jsonWebTokenStorage = jsonWebTokenStorage;
        _dispatcher = dispatcher;
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    [SwaggerOperation("Get user by ID")]
    //[SwaggerResponse(StatusCodes.Status200OK, "ok" , typeof(GetUserDto))] // TODO or import from xml?
    public async Task<ActionResult<GetUserQueryDto>> GetUserAsync(Guid id)
    {
        return await _dispatcher.DispatchQueryAsync<GetUserQueryDto>(new GetUserQuery(id));
    }

    [HttpPost("sign-up")]
    [AllowAnonymous]
    [SwaggerOperation("Sign up")]
    public async Task<ActionResult> SignUpAsync(SignUpCommand signUpCommand)
    {
        await _dispatcher.DispatchCommandAsync<SignUpCommand>(signUpCommand);
        return Accepted();
    }

    [HttpPost("sign-in")]
    [AllowAnonymous]
    [SwaggerOperation("Sign in")]
    public async Task<ActionResult<JsonWebTokenResponse>> SignInAsync(SignInCommand signInCommand)
    {
        await _dispatcher.DispatchCommandAsync<SignInCommand>(signInCommand);
        
        JsonWebTokenResponse? token = _jsonWebTokenStorage.GetToken();
        return Ok(token);
    }

    //[Authorize(Policy = "Master")]
    [Authorize]
    [HttpPost("deactivate")]
    [SwaggerOperation("Deactivate user")]
    public async Task<ActionResult> DeactivateUserAsync(DeactivateUserCommand deactivateUserCommand)
    {
        await _dispatcher.DispatchCommandAsync<DeactivateUserCommand>(deactivateUserCommand);
        return NoContent();
    }
    // TODO change user password 
    // TODO activate user
    // TODO logout user
}
