using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Vehicles.Application;

internal static class Extensions
{
	public static IServiceCollection AddVehiclesApplication(this IServiceCollection services)
	{
		return services;
	}
}