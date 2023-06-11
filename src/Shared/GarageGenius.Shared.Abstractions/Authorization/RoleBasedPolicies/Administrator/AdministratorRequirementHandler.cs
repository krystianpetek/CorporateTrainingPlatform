using Microsoft.AspNetCore.Authorization;

namespace GarageGenius.Shared.Abstractions.Authorization.RoleBasedPolicies.Administrator;

public class AdministratorRequirementHandler : AuthorizationHandler<AdministratorRequirement>
{
	protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdministratorRequirement requirement)
	{
		if (context.User.IsInRole(AuthorizationSharedConstants.AdministratorRequirement))
			context.Succeed(requirement);

		return Task.CompletedTask;
	}
}

public static partial class Extensions
{
	public static void AddAdministratorRolePolicy(this AuthorizationOptions options)
	{
		options.AddPolicy(AuthorizationSharedConstants.AdministratorRequirement, authorizationPolicyBuilder => authorizationPolicyBuilder.Requirements.Add(new AdministratorRequirement()));
	}
}