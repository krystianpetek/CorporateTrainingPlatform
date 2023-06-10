using Microsoft.AspNetCore.Authorization;

namespace GarageGenius.Shared.Abstractions.Authorization;

public class EmployeeRequirement : IAuthorizationRequirement
{
    public EmployeeRequirement()
    {
    }
}

