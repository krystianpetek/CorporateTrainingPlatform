using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Vehicles.Core;

internal static class Extensions
{
    public static IServiceCollection AddVehiclesCore(this IServiceCollection services)
    {
        return services;
    }
}