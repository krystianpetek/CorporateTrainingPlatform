using GarageGenius.Modules.Users.Core.Commands.DeactivateUser;
using GarageGenius.Modules.Users.Core.Commands.SignIn;
using GarageGenius.Modules.Users.Core.Commands.SignUp;
using GarageGenius.Modules.Users.Core.Queries.GetUser;
using GarageGenius.Modules.Users.Core.Queries.GetUsers;
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
	private readonly IDispatcher _dispatcher;

	public UsersController(
		IJsonWebTokenStorage jsonWebTokenStorage,
		IDispatcher dispatcher)
	{
		_jsonWebTokenStorage = jsonWebTokenStorage;
		_dispatcher = dispatcher;
	}

	[HttpPost("sign-up")]
	[AllowAnonymous]
	[SwaggerOperation("Sign up user")]
	public async Task<ActionResult> SignUpAsync(SignUpCommand signUpCommand)
	{
		await _dispatcher.DispatchCommandAsync<SignUpCommand>(signUpCommand);
		return Accepted();
	}

	[HttpPost("sign-in")]
	[AllowAnonymous]
	[SwaggerOperation("Sign in user")]
	public async Task<ActionResult<JsonWebTokenResponse>> SignInAsync(SignInCommand signInCommand)
	{
		await _dispatcher.DispatchCommandAsync<SignInCommand>(signInCommand);

		JsonWebTokenResponse? token = _jsonWebTokenStorage.GetToken();
		return Ok(token);
	}

	[Authorize]
	[HttpGet("me")]
	[SwaggerOperation("Get current logged user")]
	public async Task<ActionResult<GetUserQueryDto>> GetCurrentUserAsync()
	{
		Guid.TryParse(HttpContext?.User?.Identity?.Name, out Guid userId);
		var user = await _dispatcher.DispatchQueryAsync<GetUserQueryDto>(new GetUserQuery(userId));
		return Ok(user);
	}

	[Authorize]
	[HttpGet("{id:guid}")]
	[SwaggerOperation("Get user by ID")]
	public async Task<ActionResult<GetUserQueryDto>> GetUserAsync(Guid id)
	{
		var user = await _dispatcher.DispatchQueryAsync<GetUserQueryDto>(new GetUserQuery(id));
		return Ok(user);
	}

	[Authorize]
	[HttpPost("sign-out")]
	[SwaggerOperation("Sign out user")]
	public new IActionResult SignOut()
	{
		_jsonWebTokenStorage.RemoveToken();
		return Ok();
	}

	[Authorize]
	[HttpPost("deactivate")]
	[SwaggerOperation("Deactivate user")]
	public async Task<ActionResult> DeactivateUserAsync(DeactivateUserCommand deactivateUserCommand)
	{
		await _dispatcher.DispatchCommandAsync<DeactivateUserCommand>(deactivateUserCommand);
		return NoContent();
	}

	[Authorize]
	[HttpGet("users")]
	[SwaggerOperation("Get all users")]
	public async Task<ActionResult<GetUsersQueryDto>> GetUsersAsync()
	{
		var users = await _dispatcher.DispatchQueryAsync<GetUsersQueryDto>(new GetUsersQuery());
		return Ok(users);
	}

	//[ProducesResponseType(StatusCodes.Status200OK)] // TODO response types
	//[SwaggerResponse(StatusCodes.Status200OK, "ok" , typeof(GetUserDto))] // TODO or import from xml?
	// TODO change user password 
	// TODO activate user ?
}
