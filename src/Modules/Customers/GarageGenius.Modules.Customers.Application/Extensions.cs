using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Customers.Application;

internal static class Extensions
{
	public static IServiceCollection AddCustomersApplication(this IServiceCollection services)
	{
		return services;
	}
}