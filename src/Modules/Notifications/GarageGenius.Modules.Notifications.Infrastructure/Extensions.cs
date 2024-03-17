using GarageGenius.Modules.Notifications.Core.Repositories;
using GarageGenius.Modules.Notifications.Infrastructure.Persistence.DbContexts;
using GarageGenius.Modules.Notifications.Infrastructure.Persistence.Repositories;
using GarageGenius.Shared.Infrastructure.Persistance.MsSqlServer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Notifications.Infrastructure;

internal static class Extensions
{
	public static IServiceCollection AddNotificationsInfrastructure(this IServiceCollection services, IWebHostEnvironment webHostEnvironment)
	{
		services.AddMsSqlServerDbContext<NotificationsDbContext>(webHostEnvironment);
		//services.AddPostgreSqlServerDbContext<NotificationsDbContext>(webHostEnvironment);
		services.AddScoped<INotificationRepository, NotificationRepository>();

		return services;
	}
}