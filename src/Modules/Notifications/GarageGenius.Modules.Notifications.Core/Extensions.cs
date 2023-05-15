using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Notifications.Core;

internal static class Extensions
{
    public static IServiceCollection AddNotificationsCore(this IServiceCollection services)
    {
        return services;
    }
}