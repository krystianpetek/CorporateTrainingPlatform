using GarageGenius.Modules.Vehicles.Application.Policies.AddVehicle;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Vehicles.Application;

internal static class Extensions
{
	public static IServiceCollection AddVehiclesApplication(this IServiceCollection services)
	{
		services.AddScoped<IAuthorizationHandler, AddVehiclePolicyHandler>();
		services.AddScoped<IAuthorizationHandler, GetCustomerVehiclesPolicyHandler>();

		services.AddAuthorization(authorizationOptions =>
		{
			authorizationOptions.AddVehiclePolicy();
			authorizationOptions.GetCustomerVehiclesPolicy();
		});
		return services;
	}
}
