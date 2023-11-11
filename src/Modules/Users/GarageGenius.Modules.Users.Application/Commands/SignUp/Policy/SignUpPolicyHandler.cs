using GarageGenius.Modules.Users.Core.Entities;
using GarageGenius.Modules.Users.Shared;
using GarageGenius.Shared.Abstractions.Authorization;
using Microsoft.AspNetCore.Authorization;
using Serilog;

namespace GarageGenius.Modules.Users.Application.Commands.SignUp.Policy;

public class SignUpPolicyHandler : AuthorizationHandler<SignUpPolicyRequirement, string>
{
    private readonly Serilog.ILogger _logger;

    public SignUpPolicyHandler(ILogger logger)
    {
        _logger = logger;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SignUpPolicyRequirement requirement, string resource)
    {
        // if requirement operation is not create, fail
        if (requirement._operationAuthorizationRequirement != AuthorizationSharedConstants.CreateRequirement)
        {
            context.Fail();
            return Task.CompletedTask;
        }
        
        // if sign-up user is Administrator, Manager or Employee, allow it only for Administrator
        if (resource == Roles.Administrator || 
            resource == Roles.Manager ||
            resource == Roles.Employee)
        {
            if (!context.User.IsInRole(AuthorizationSharedConstants.AdministratorRequirement))
            {
                context.Fail();
                return Task.CompletedTask;
            }
        }
        
        context.Succeed(requirement);
        return Task.CompletedTask;
        
    }
}

public static partial class Extensions
{
    public static void SignUpPolicy(this AuthorizationOptions options)
    {
        options.AddPolicy(
            UsersPolicyConstants.SignUpPolicy,
            authorizationPolicyBuilder => authorizationPolicyBuilder.Requirements.Add(new SignUpPolicyRequirement(AuthorizationSharedConstants.CreateRequirement)));
    }
}