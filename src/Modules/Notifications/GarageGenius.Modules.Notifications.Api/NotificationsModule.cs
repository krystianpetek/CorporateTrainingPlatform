using GarageGenius.Modules.Notifications.Application;
using GarageGenius.Modules.Notifications.Core;
using GarageGenius.Modules.Notifications.Infrastructure;
using GarageGenius.Shared.Abstractions.Modules;
using GarageGenius.Shared.Infrastructure.HealthCheck;
using Microsoft.AspNetCore.Builder;

namespace GarageGenius.Modules.Notifications.Api;
internal class NotificationsModule : IModule
{
	public const string BasePath = "notifications-module";
	public string Name { get; } = "Notifications";
	public IEnumerable<string> Policies { get; } = new string[] { "notifications" };

	public void Register(WebApplicationBuilder webApplicationBuilder)
	{
		webApplicationBuilder.Services
			.AddNotificationsApplication()
			.AddNotificationsInfrastructure(webApplicationBuilder.Environment);
	}

	public void Use(WebApplication app)
	{
		app.UseNotificationsCore();
		app.MapHealthCheck(Name);
	}
}
