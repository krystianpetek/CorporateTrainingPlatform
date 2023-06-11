using Microsoft.AspNetCore.Authorization;

namespace GarageGenius.Shared.Abstractions.Authorization.RoleBasedPolicies.Customer;

public class CustomerRequirement : IAuthorizationRequirement
{
	public CustomerRequirement()
	{
	}
}

