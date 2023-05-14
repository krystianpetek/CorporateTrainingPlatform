using GarageGenius.Modules.Notifications.Core;
using GarageGenius.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Notifications.Api;
internal class NotificationsModule : IModule
{
    public const string BasePath = "notifications-module";
    public string Name { get; } = "Notifications";
    public IEnumerable<string> Policies { get; } = new string[] { "notifications" };

    public void Register(IServiceCollection services)
    {
        services.AddNotificationsCore();
        services.AddControllers();
    }

    public void Use(IApplicationBuilder app)
    {
        app.UseHealthChecks($"/health/{BasePath}");
    }
}
