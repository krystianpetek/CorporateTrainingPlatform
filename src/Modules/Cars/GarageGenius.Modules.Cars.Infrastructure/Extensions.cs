using GarageGenius.Modules.Cars.Infrastructure.Persistance.DbContexts;
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
        return services;
    }
}