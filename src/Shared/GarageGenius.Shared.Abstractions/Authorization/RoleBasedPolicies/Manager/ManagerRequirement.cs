using Microsoft.AspNetCore.Authorization;

namespace GarageGenius.Shared.Abstractions.Authorization.RoleBasedPolicies.Manager;

public class ManagerRequirement : IAuthorizationRequirement
{
	public ManagerRequirement()
	{
	}
}

