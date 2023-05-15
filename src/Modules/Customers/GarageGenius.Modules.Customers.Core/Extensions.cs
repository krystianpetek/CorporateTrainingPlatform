using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Customers.Core;

internal static class Extensions
{
    public static IServiceCollection AddCustomersCore(this IServiceCollection services)
    {
        return services;
    }
}