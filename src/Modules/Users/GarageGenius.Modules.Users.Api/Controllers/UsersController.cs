using GarageGenius.Modules.Users.Core.Commands.SignUp;
using GarageGenius.Modules.Users.Core.Dto;
using GarageGenius.Modules.Users.Core.Queries.GetUser;
using GarageGenius.Modules.Users.Core.Queries.SignIn;
using GarageGenius.Shared.Abstractions.Authentication.JsonWebToken;
using GarageGenius.Shared.Abstractions.Dispatcher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GarageGenius.Modules.Users.Api.Controllers;
public class UsersController : BaseController
{
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //[ProducesResponseType(StatusCodes.Status403Forbidden)]

    private readonly IDispatcher _dispatcher;

    public UsersController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    [SwaggerOperation("Get user by ID")]
    public async Task<ActionResult<GetUserDto>> GetUserAsync(Guid id)
    {
        return await _dispatcher.QueryAsync<GetUserDto>(new GetUserQuery(id));
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
    public async Task<ActionResult<JsonWebTokenDto>> SignInAsync(SignInQuery signInQuery)
    {
        JsonWebTokenDto token = await _dispatcher.QueryAsync<JsonWebTokenDto>(signInQuery);
        return Ok(token);
    }

}
