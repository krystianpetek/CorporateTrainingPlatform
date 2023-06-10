using GarageGenius.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Shared.Infrastructure.Authorization;
public static class Extensions
{
	public static IServiceCollection AddSharedAuthorization(this IServiceCollection services, IList<IModule> modules)
	{
		services.AddAuthorization(authorization =>
		{
			foreach (var module in modules)
			{
				foreach (string policy in module.Policies)
				{
					authorization.AddPolicy(policy, claims => claims.RequireClaim("permissions", policy));
				}
			}
		});
		// TODO - change this to use the module policies?
		return services;
	}

	public static IApplicationBuilder UseSharedAuthorization(this IApplicationBuilder app)
	{
		app.UseAuthorization();
		return app;
	}
}
