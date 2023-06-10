using Microsoft.AspNetCore.Authorization;

namespace GarageGenius.Shared.Abstractions.Authorization;

public class CustomerRequirementHandler : AuthorizationHandler<CustomerRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomerRequirement requirement)
    {
        if (context.User.IsInRole(AuthorizationConstants.Customer))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}

public static partial class Extensions
{
	public static void AddCustomerRolePolicy(this AuthorizationOptions options)
    {
		options.AddPolicy(AuthorizationConstants.Customer, authorizationPolicyBuilder => authorizationPolicyBuilder.Requirements.Add(new CustomerRequirement()));
	}
}