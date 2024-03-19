using GarageGenius.Modules.Notifications.Core.Services;
using GarageGenius.Shared.Infrastructure.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Notifications.Core;

internal static class Extensions
{
	public static IServiceCollection AddNotificationsCore(this IServiceCollection services)
	{
		services.AddScoped<IEmailSenderService, EmailSenderService>();
		services.AddScoped<IPdfGeneratorService, PdfGeneratorService>();
		return services;
	}

	public static WebApplication UseNotificationsCore(this WebApplication app)
	{
		app.UseSignalRNotificationsHub();
		return app;
	}
}