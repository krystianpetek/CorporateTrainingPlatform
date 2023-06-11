using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace GarageGenius.Modules.Vehicles.Application.Policies.AddVehicle;

public class AddVehiclePolicyRequirement : IAuthorizationRequirement
{
	public readonly OperationAuthorizationRequirement _operationAuthorizationRequirement;

	public AddVehiclePolicyRequirement(OperationAuthorizationRequirement operationAuthorizationRequirement)
	{
		_operationAuthorizationRequirement = operationAuthorizationRequirement;
	}
}

