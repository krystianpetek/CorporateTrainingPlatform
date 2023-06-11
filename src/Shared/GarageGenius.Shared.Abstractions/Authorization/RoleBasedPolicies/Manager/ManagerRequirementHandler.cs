using Microsoft.AspNetCore.Authorization;

namespace GarageGenius.Shared.Abstractions.Authorization.RoleBasedPolicies.Manager;

public class ManagerRequirementHandler : AuthorizationHandler<ManagerRequirement>
{
	protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ManagerRequirement requirement)
	{
		if (context.User.IsInRole(AuthorizationSharedConstants.ManagerRequirement))
			context.Succeed(requirement);

		return Task.CompletedTask;
	}
}

public static partial class Extensions
{
	public static void AddManagerRolePolicy(this AuthorizationOptions options)
	{
		options.AddPolicy(AuthorizationSharedConstants.ManagerRequirement, authorizationPolicyBuilder => authorizationPolicyBuilder.Requirements.Add(new ManagerRequirement()));
	}
}