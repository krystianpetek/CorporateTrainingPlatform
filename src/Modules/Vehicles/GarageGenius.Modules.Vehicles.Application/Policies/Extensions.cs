using GarageGenius.Modules.Vehicles.Application.Policies.AddVehicle;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Vehicles.Application.Policies;
internal static class Extensions
{
	public static void AddVehiclesModulePolicies(this IServiceCollection services)
	{
		services.AddScoped<IAuthorizationHandler, AddVehiclePolicyHandler>();

		services.AddAuthorization(authorizationOptions =>
		{
			authorizationOptions.AddVehiclePolicy();
		});
	}
}
