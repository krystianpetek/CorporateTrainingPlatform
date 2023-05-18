using GarageGenius.Modules.Cars.Application.QueryStorage;
using GarageGenius.Modules.Cars.Core.Repositories;
using GarageGenius.Modules.Cars.Infrastructure.Persistance.DbContexts;
using GarageGenius.Modules.Cars.Infrastructure.Persistance.Repositories;
using GarageGenius.Modules.Cars.Infrastructure.QueryStorage;
using GarageGenius.Shared.Infrastructure.Persistance.MsSqlServer;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GarageGenius.Modules.Cars.Api")]
namespace GarageGenius.Modules.Cars.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddCarsInfrastructure(this IServiceCollection services)
    {
        services.AddMsSqlServerDbContext<CarsDbContext>();
        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<ICarQueryStorage, CarQueryStorage>();
        return services;
    }
}