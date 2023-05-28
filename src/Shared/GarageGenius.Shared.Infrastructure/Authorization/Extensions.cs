using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Shared.Infrastructure.Authorization;
public static class Extensions
{
	public static IServiceCollection AddSharedAuthorization(this IServiceCollection services)
	{
		services.AddAuthorization();
		return services;
	}

	public static IApplicationBuilder UseSharedAuthorization(this IApplicationBuilder app)
	{
		app.UseAuthorization();
		return app;
	}
}
