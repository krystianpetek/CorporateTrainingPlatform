using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace GarageGenius.Modules.Vehicles.Application.Commands.AddVehicle.Policy;

public class AddVehiclePolicyRequirement : IAuthorizationRequirement
{
    public readonly OperationAuthorizationRequirement _operationAuthorizationRequirement;

    public AddVehiclePolicyRequirement(OperationAuthorizationRequirement operationAuthorizationRequirement)
    {
        _operationAuthorizationRequirement = operationAuthorizationRequirement;
    }
}

