using Microsoft.AspNetCore.Authorization;

namespace GarageGenius.Shared.Abstractions.Authorization.AdministratorRequirement;

public class AdministratorRequirement : IAuthorizationRequirement
{
	public AdministratorRequirement()
	{
	}
}

