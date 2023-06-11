using Microsoft.AspNetCore.Authorization;

namespace GarageGenius.Shared.Abstractions.Authorization.RoleBasedPolicies.Employee;

public class EmployeeRequirement : IAuthorizationRequirement
{
	public EmployeeRequirement()
	{
	}
}

