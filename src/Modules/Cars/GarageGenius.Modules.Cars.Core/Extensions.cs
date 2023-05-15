using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Cars.Core;

internal static class Extensions
{
    public static IServiceCollection AddCarsCore(this IServiceCollection services)
    {
        return services;
    }
}