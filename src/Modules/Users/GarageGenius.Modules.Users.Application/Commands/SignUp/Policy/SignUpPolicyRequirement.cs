using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace GarageGenius.Modules.Users.Application.Commands.SignUp.Policy;

public class SignUpPolicyRequirement : IAuthorizationRequirement
{
    public readonly OperationAuthorizationRequirement _operationAuthorizationRequirement;

    public SignUpPolicyRequirement(OperationAuthorizationRequirement operationAuthorizationRequirement)
    {
        _operationAuthorizationRequirement = operationAuthorizationRequirement;
    }
}

