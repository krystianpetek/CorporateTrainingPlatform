using GarageGenius.Modules.Users.Application.Commands.CreateUser;
using GarageGenius.Modules.Users.Application.Commands.DeactivateUser;
using GarageGenius.Modules.Users.Application.Commands.SignIn;
using GarageGenius.Modules.Users.Application.Commands.SignUp;
using GarageGenius.Modules.Users.Application.Queries.GetUser;
using GarageGenius.Modules.Users.Application.Queries.GetUsers;
using GarageGenius.Shared.Abstractions.Authentication.JsonWebToken;
using GarageGenius.Shared.Abstractions.Authentication.JsonWebToken.Models;
using GarageGenius.Shared.Abstractions.Dispatcher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GarageGenius.Modules.Users.Api.Controllers;
public class UsersController(
		IJsonWebTokenStorage jsonWebTokenStorage,
		IDispatcher dispatcher) : BaseController
{
	[HttpPost("sign-up")]
	[AllowAnonymous]
	[SwaggerOperation("Sign up user")]
	[SwaggerResponse(StatusCodes.Status202Accepted, "User signed up")]
	public async Task<ActionResult> SignUpAsync(SignUpCommand signUpCommand)
	{
		await dispatcher.DispatchCommandAsync<SignUpCommand>(signUpCommand);
		return Accepted();
	}

	[HttpPost("sign-in")]
	[AllowAnonymous]
	[SwaggerOperation("Sign in user")]
	[SwaggerResponse(StatusCodes.Status200OK, "User signed in", typeof(JsonWebTokenResponse))]
	public async Task<ActionResult<JsonWebTokenResponse>> SignInAsync(SignInCommand signInCommand)
	{
		await dispatcher.DispatchCommandAsync<SignInCommand>(signInCommand);

		JsonWebTokenResponse? token = jsonWebTokenStorage.GetToken();
		return Ok(token);
	}

	[Authorize]
	[HttpGet("me")]
	[SwaggerOperation("Get current logged user")]
	[SwaggerResponse(StatusCodes.Status200OK, "User found", typeof(GetUserQueryDto))]
	public async Task<ActionResult<GetUserQueryDto>> GetCurrentUserAsync()
	{
		Guid.TryParse(HttpContext?.User?.Identity?.Name, out Guid userId);
		var user = await dispatcher.DispatchQueryAsync<GetUserQueryDto>(new GetUserQuery(userId));
		return Ok(user);
	}

	[Authorize]
	[HttpGet("{id:guid}")]
	[SwaggerOperation("Get user by ID")]
	[SwaggerResponse(StatusCodes.Status200OK, "User found", typeof(GetUserQueryDto))]
	public async Task<ActionResult<GetUserQueryDto>> GetUserAsync(Guid id)
	{
		var user = await dispatcher.DispatchQueryAsync<GetUserQueryDto>(new GetUserQuery(id));
		return Ok(user);
	}

	[Authorize]
	[HttpPost("sign-out")]
	[SwaggerOperation("Sign out user")]
	[SwaggerResponse(StatusCodes.Status200OK, "User signed out")]
	public new IActionResult SignOut()
	{
		jsonWebTokenStorage.RemoveToken();
		return Ok();
	}

	[Authorize]
	[HttpPost("deactivate")]
	[SwaggerOperation("Deactivate user")]
	[SwaggerResponse(StatusCodes.Status204NoContent, "User deactivated")]
	public async Task<ActionResult> DeactivateUserAsync(DeactivateUserCommand deactivateUserCommand)
	{
		await dispatcher.DispatchCommandAsync<DeactivateUserCommand>(deactivateUserCommand);
		return NoContent();
	}

	[Authorize]
	[HttpGet("users")]
	[SwaggerOperation("Get all users")]
	[SwaggerResponse(StatusCodes.Status200OK, "Users found", typeof(GetUsersQueryDto))]
	public async Task<ActionResult<GetUsersQueryDto>> GetUsersAsync()
	{
		var users = await dispatcher.DispatchQueryAsync<GetUsersQueryDto>(new GetUsersQuery());
		return Ok(users);
	}

	[Authorize]
	[HttpPost("users")]
	[SwaggerOperation("Create new user")]
	[SwaggerResponse(StatusCodes.Status200OK, "User created")]
	public async Task<ActionResult> CreateUserAsync(CreateUserCommand createUserCommand)
	{
		await dispatcher.DispatchCommandAsync<CreateUserCommand>(createUserCommand);
		return Ok();
	}

	// TODO change user password 
	// TODO activate user ?
}
