using Microsoft.AspNetCore.Authorization;

namespace GarageGenius.Shared.Abstractions.Authorization;

public class EmployeeRequirementHandler : AuthorizationHandler<EmployeeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EmployeeRequirement requirement)
    {
        if (context.User.IsInRole(AuthorizationConstants.Employee))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}

public static partial class Extensions
{
	public static void AddEmployeeRolePolicy(this AuthorizationOptions options)
    {
		options.AddPolicy(AuthorizationConstants.Employee, authorizationPolicyBuilder => authorizationPolicyBuilder.Requirements.Add(new EmployeeRequirement()));
	}
}