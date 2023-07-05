using GarageGenius.Modules.Customers.Core;
using GarageGenius.Shared.Abstractions.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Customers.Application;

internal static class Extensions
{
	public static IServiceCollection AddCustomersApplication(this IServiceCollection services)
	{
		services.AddCustomersCore();
		return services;
	}
}