using GarageGenius.Modules.Vehicles.Application.Commands.AddVehicle.Policy;
using GarageGenius.Modules.Vehicles.Core.Types;
using GarageGenius.Modules.Vehicles.Shared;
using GarageGenius.Shared.Abstractions.Authentication.JsonWebToken.Models;
using GarageGenius.Shared.Abstractions.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace GarageGenius.Modules.Vehicles.Application.Policies.AddVehicle;

internal class AddVehiclePolicyHandler : AuthorizationHandler<AddVehiclePolicyRequirement, CustomerId>
{
	private readonly Serilog.ILogger _logger;

	public AddVehiclePolicyHandler(Serilog.ILogger logger)
	{
		_logger = logger;
	}

	protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AddVehiclePolicyRequirement requirement, CustomerId resource)
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
		string customerId = context.User.Claims.Where(x => x.Type.Equals(JwtClaimTypes.CustomerId)).FirstOrDefault().Value;
		if (customerId == $"{resource}")
		{
			context.Succeed(requirement);
			return Task.CompletedTask;
		}

		context.Fail();
		_logger.Warning("User {UserId} is not authorized to add a new vehicle for customer {CustomerId}", context.User.Identity.Name, resource);
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