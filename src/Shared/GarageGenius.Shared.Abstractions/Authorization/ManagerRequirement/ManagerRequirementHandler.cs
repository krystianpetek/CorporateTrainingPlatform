using Microsoft.AspNetCore.Authorization;

namespace GarageGenius.Shared.Abstractions.Authorization.ManagerRequirement;

public class ManagerRequirementHandler : AuthorizationHandler<ManagerRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ManagerRequirement requirement)
    {
        if (context.User.IsInRole(AuthorizationSharedConstants.Manager))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}

public static partial class Extensions
{
	public static void AddManagerRolePolicy(this AuthorizationOptions options)
    {
		options.AddPolicy(AuthorizationSharedConstants.Manager, authorizationPolicyBuilder => authorizationPolicyBuilder.Requirements.Add(new ManagerRequirement()));
	}
}