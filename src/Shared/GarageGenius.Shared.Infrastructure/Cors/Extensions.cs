using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Shared.Infrastructure.Cors;
public static class Extensions
{
	private static readonly string corsPolicyName = "backendCorsPolicy";

	public static IServiceCollection AddSharedCors(this IServiceCollection services)
	{
		services.AddCors((CorsOptions corsOptions) =>
		{
			corsOptions.AddPolicy(corsPolicyName, (CorsPolicyBuilder corsPolicyBuilder) =>
			{
				corsPolicyBuilder
				.WithOrigins("https://localhost:6857")
				.AllowAnyMethod()
				.AllowAnyHeader()
				.AllowCredentials();
			});
		});
		return services;
	}

	public static IApplicationBuilder UseSharedCors(this IApplicationBuilder app)
	{
		app.UseCors(corsPolicyName);
		return app;
	}
}