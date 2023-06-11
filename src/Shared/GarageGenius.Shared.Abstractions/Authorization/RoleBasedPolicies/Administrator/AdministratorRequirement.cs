using Microsoft.AspNetCore.Authorization;

namespace GarageGenius.Shared.Abstractions.Authorization.RoleBasedPolicies.Administrator;

public class AdministratorRequirement : IAuthorizationRequirement
{
	public AdministratorRequirement()
	{
	}
}

