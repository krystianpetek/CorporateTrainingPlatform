using Microsoft.AspNetCore.Authorization;

namespace GarageGenius.Shared.Abstractions.Authorization;

public class CustomerRequirement : IAuthorizationRequirement
{
    public CustomerRequirement()
    {
    }
}

