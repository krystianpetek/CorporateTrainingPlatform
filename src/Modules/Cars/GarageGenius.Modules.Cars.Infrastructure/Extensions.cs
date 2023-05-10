using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GarageGenius.Modules.Cars.Api")]
namespace GarageGenius.Modules.Cars.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddCarsInfrastructure(this IServiceCollection services)
    {

        return services;
    }
}