using GarageGenius.Shared.Infrastructure.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Notifications.Core;

internal static class Extensions
{
    public static IServiceCollection AddNotificationsCore(this IServiceCollection services)
    {
        return services;
    }
    
    public static WebApplication UseNotificationsCore(this WebApplication app)
    {
        app.UseSharedSignalR();
        return app;
    }
}