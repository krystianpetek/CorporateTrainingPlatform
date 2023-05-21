using GarageGenius.Shared.Infrastructure.SignalR.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Shared.Infrastructure.SignalR;
public static class Extensions
{
    public static IServiceCollection AddSharedSignalR(this IServiceCollection services)
    {
        services.AddSignalR(hubOptions =>
        {
            hubOptions.EnableDetailedErrors = true;
            hubOptions.KeepAliveInterval = TimeSpan.FromSeconds(15);
        }).AddJsonProtocol(jsonHubProtocolOptions =>
        {
            jsonHubProtocolOptions.PayloadSerializerOptions.WriteIndented = true;
        });
        return services;
    }

    public static IApplicationBuilder UseSignalRNotificationsHub(this WebApplication app)
    {
        app.MapHub<NotificationsHub>("/notifications");
        return app;
    }
}
