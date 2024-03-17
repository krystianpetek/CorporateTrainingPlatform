using FluentValidation;
using GarageGenius.Modules.Notifications.Core;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Notifications.Application;

internal static class Extensions
{
	public static IServiceCollection AddNotificationsApplication(this IServiceCollection services)
	{
		services.AddNotificationsCore();

		return services;
	}
}