using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GarageGenius.Modules.Notifications.Api")]
namespace GarageGenius.Modules.Notifications.Core;

internal static class Extensions
{
    public static IServiceCollection AddNotificationsCore(this IServiceCollection services)
    {
        return services;
    }
}