using Microsoft.AspNetCore.Authorization;

namespace GarageGenius.Shared.Abstractions.Authorization.RoleBasedPolicies.Customer;

public class CustomerRequirementHandler : AuthorizationHandler<CustomerRequirement>
{
	protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomerRequirement requirement)
	{
		if (context.User.IsInRole(AuthorizationSharedConstants.CustomerRequirement))
			context.Succeed(requirement);

		return Task.CompletedTask;
	}
}

public static partial class Extensions
{
	public static void AddCustomerRolePolicy(this AuthorizationOptions options)
	{
		options.AddPolicy(AuthorizationSharedConstants.CustomerRequirement, authorizationPolicyBuilder => authorizationPolicyBuilder.Requirements.Add(new CustomerRequirement()));
	}
}