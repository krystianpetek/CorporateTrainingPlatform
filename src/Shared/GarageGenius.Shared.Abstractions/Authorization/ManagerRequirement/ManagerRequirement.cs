using Microsoft.AspNetCore.Authorization;

namespace GarageGenius.Shared.Abstractions.Authorization.ManagerRequirement;

public class ManagerRequirement : IAuthorizationRequirement
{
	public ManagerRequirement()
	{
	}
}

