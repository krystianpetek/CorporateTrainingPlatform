using GarageGenius.Modules.Vehicles.Application.QueryStorage;
using GarageGenius.Modules.Vehicles.Core.Repositories;
using GarageGenius.Modules.Vehicles.Infrastructure.Persistance.DbContexts;
using GarageGenius.Modules.Vehicles.Infrastructure.Persistance.Repositories;
using GarageGenius.Modules.Vehicles.Infrastructure.QueryStorage;
using GarageGenius.Shared.Infrastructure.Persistance.MsSqlServer;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Vehicles.Infrastructure;

internal static class Extensions
{
	public static IServiceCollection AddVehiclesInfrastructure(this IServiceCollection services)
	{
		services.AddMsSqlServerDbContext<VehiclesDbContext>();
		services.AddScoped<IVehicleRepository, VehicleRepository>();
		services.AddScoped<IVehicleQueryStorage, VehicleQueryStorage>();
		return services;
	}
}