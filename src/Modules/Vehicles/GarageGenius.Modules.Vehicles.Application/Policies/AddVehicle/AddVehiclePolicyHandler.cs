using GarageGenius.Modules.Vehicles.Core.Entities;
using GarageGenius.Modules.Vehicles.Shared;
using GarageGenius.Shared.Abstractions.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Vehicles.Application.Policies.AddVehicle;

internal class AddVehiclePolicyHandler : AuthorizationHandler<AddVehiclePolicyRequirement, Vehicle>
{
	private readonly Serilog.ILogger _logger;

	public AddVehiclePolicyHandler(Serilog.ILogger logger)
	{
		_logger = logger;
	}

	protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AddVehiclePolicyRequirement requirement, Vehicle resource)
	{
		// if requirement operation is not create, fail
		if (requirement._operationAuthorizationRequirement != AuthorizationSharedConstants.CreateRequirement)
		{
			context.Fail();
			return Task.CompletedTask;
		}

		// if user is an administrator or employee, allow add new vehicle
		if (context.User.IsInRole(AuthorizationSharedConstants.AdministratorRequirement) ||
			context.User.IsInRole(AuthorizationSharedConstants.EmployeeRequirement))
		{
			context.Succeed(requirement);
			return Task.CompletedTask;
		}

		// if user is a customer, allow add new vehicle if the vehicle belongs to him
		if (context.User.Identity.Name == $"{resource.UserId}")
		{
			context.Succeed(requirement);
			return Task.CompletedTask;
		}

		context.Fail();
		_logger.Warning("User {UserId} is not authorized to add a new vehicle for customer {CustomerId}", context.User.Identity.Name, resource.CustomerId);
		return Task.CompletedTask;
	}
}

public static partial class Extensions
{
	public static void AddVehiclePolicy(this AuthorizationOptions options)
	{
		options.AddPolicy(
			   VehiclesPolicyConstants.AddVehiclePolicy,
				authorizationPolicyBuilder => authorizationPolicyBuilder.Requirements.Add(new AddVehiclePolicyRequirement(AuthorizationSharedConstants.CreateRequirement)));
	}
}