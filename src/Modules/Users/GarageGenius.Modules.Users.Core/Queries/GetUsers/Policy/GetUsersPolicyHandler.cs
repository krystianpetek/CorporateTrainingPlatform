using GarageGenius.Modules.Users.Shared;
using GarageGenius.Shared.Abstractions.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace GarageGenius.Modules.Users.Core.Queries.GetUsers.Policy;

internal class GetUsersPolicyHandler : AuthorizationHandler<GetUsersPolicyRequirement>
{
	private readonly Serilog.ILogger _logger;

	public GetUsersPolicyHandler(Serilog.ILogger logger)
	{
		_logger = logger;
	}

	protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, GetUsersPolicyRequirement requirement)
	{
		// if requirement operation is not read, fail
		if (requirement._operationAuthorizationRequirement != AuthorizationSharedConstants.ReadRequirement)
		{
			context.Fail();
			return Task.CompletedTask;
		}

		// if user is an administrator or employee, allow add new vehicle
		if (context.User.IsInRole(AuthorizationSharedConstants.AdministratorRequirement) ||
			context.User.IsInRole(AuthorizationSharedConstants.EmployeeRequirement))
		{
			context.Succeed(requirement);
			return Task.CompletedTask;
		}

		context.Fail();
		_logger.Warning("User {UserId} is not authorized to fetch a users", context.User.Identity.Name);
		return Task.CompletedTask;
	}
}

public static partial class Extensions
{
	public static void GetUsersPolicy(this AuthorizationOptions options)
	{
		options.AddPolicy(
			   UsersPolicyConstants.GetUsersPolicy,
			   authorizationPolicyBuilder => authorizationPolicyBuilder.Requirements.Add(new GetUsersPolicyRequirement(AuthorizationSharedConstants.ReadRequirement)));
	}
}