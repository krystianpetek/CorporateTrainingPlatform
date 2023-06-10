using Microsoft.AspNetCore.Authorization;

namespace GarageGenius.Shared.Abstractions.Authorization;

public class ManagerRequirement : IAuthorizationRequirement
{
    public ManagerRequirement()
    {
    }
}

