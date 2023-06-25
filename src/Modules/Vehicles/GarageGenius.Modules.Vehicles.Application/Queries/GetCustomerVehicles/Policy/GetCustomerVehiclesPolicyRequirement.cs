using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace GarageGenius.Modules.Vehicles.Application.Queries.GetCustomerVehicles.Policy;

public class GetCustomerVehiclesPolicyRequirement : IAuthorizationRequirement
{
	public readonly OperationAuthorizationRequirement _operationAuthorizationRequirement;

	public GetCustomerVehiclesPolicyRequirement(OperationAuthorizationRequirement operationAuthorizationRequirement)
	{
		_operationAuthorizationRequirement = operationAuthorizationRequirement;
	}
}

