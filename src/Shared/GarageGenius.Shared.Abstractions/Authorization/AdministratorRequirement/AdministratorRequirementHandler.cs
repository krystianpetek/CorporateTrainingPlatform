using Microsoft.AspNetCore.Authorization;

namespace GarageGenius.Shared.Abstractions.Authorization;

public class AdministratorRequirementHandler : AuthorizationHandler<AdministratorRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdministratorRequirement requirement)
    {
        if (context.User.IsInRole(AuthorizationConstants.Administrator))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}

public static partial class Extensions
{
    public static void AddAdministratorRolePolicy(this AuthorizationOptions options)
    {
		options.AddPolicy(AuthorizationConstants.Administrator, authorizationPolicyBuilder => authorizationPolicyBuilder.Requirements.Add(new AdministratorRequirement()));
	}
}