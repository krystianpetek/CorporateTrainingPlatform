using Microsoft.AspNetCore.Authorization;

namespace GarageGenius.Shared.Abstractions.Authorization.EmployeeRequirement;

public class EmployeeRequirementHandler : AuthorizationHandler<EmployeeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EmployeeRequirement requirement)
    {
        if (context.User.IsInRole(AuthorizationSharedConstants.Employee))
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
		options.AddPolicy(AuthorizationSharedConstants.Employee, authorizationPolicyBuilder => authorizationPolicyBuilder.Requirements.Add(new EmployeeRequirement()));
	}
}