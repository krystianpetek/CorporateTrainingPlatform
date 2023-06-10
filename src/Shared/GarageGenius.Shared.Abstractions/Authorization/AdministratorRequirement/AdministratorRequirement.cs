using Microsoft.AspNetCore.Authorization;

namespace GarageGenius.Shared.Abstractions.Authorization;

public class AdministratorRequirement : IAuthorizationRequirement
{
    public AdministratorRequirement()
    {
    }
}

