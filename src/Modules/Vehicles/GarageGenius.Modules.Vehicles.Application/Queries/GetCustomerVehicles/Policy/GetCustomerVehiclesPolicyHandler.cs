using GarageGenius.Modules.Vehicles.Application.Queries.GetCustomerVehicles.Policy;
using GarageGenius.Modules.Vehicles.Shared;
using GarageGenius.Shared.Abstractions.Authentication.JsonWebToken.Models;
using GarageGenius.Shared.Abstractions.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace GarageGenius.Modules.Vehicles.Application.Policies.AddVehicle;

internal class GetCustomerVehiclesPolicyHandler : AuthorizationHandler<GetCustomerVehiclesPolicyRequirement, Guid>
{
	private readonly Serilog.ILogger _logger;

	public GetCustomerVehiclesPolicyHandler(Serilog.ILogger logger)
	{
		_logger = logger;
	}

	protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, GetCustomerVehiclesPolicyRequirement requirement, Guid resource)
	{
		// if requirement operation is not create, fail
		if (requirement._operationAuthorizationRequirement != AuthorizationSharedConstants.ReadRequirement)
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

		// if user is a customer, allow get customer vehicles if the vehicle belongs to him
		string customerId = context.User.Claims.Where(x => x.Type.Equals(JwtClaimTypes.CustomerId)).FirstOrDefault().Value;
		if (customerId == $"{resource}")
		{
			context.Succeed(requirement);
			return Task.CompletedTask;
		}

		context.Fail();
		_logger.Warning("User {UserId} is not authorized to fetch a user {CustomerId} vehicles", context.User.Identity.Name, resource);
		return Task.CompletedTask;
	}
}

public static partial class Extensions
{
	public static void GetCustomerVehiclesPolicy(this AuthorizationOptions options)
	{
		options.AddPolicy(
			   VehiclesPolicyConstants.GetCustomerVehiclesPolicy,
			   				authorizationPolicyBuilder => authorizationPolicyBuilder.Requirements.Add(new GetCustomerVehiclesPolicyRequirement(AuthorizationSharedConstants.ReadRequirement)));
	}
}