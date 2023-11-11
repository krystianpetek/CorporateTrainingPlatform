using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace GarageGenius.Modules.Users.Application.Queries.GetUsers.Policy;

public class GetUsersPolicyRequirement : IAuthorizationRequirement
{
	public readonly OperationAuthorizationRequirement _operationAuthorizationRequirement;

	public GetUsersPolicyRequirement(OperationAuthorizationRequirement operationAuthorizationRequirement)
	{
		_operationAuthorizationRequirement = operationAuthorizationRequirement;
	}
}

