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
					string read = policy + "-r";
					authorization.AddPolicy(read, claims => claims.RequireClaim("permissions", read));

					string write = policy + "-w";
					authorization.AddPolicy(write, claims => claims.RequireClaim("permissions", write));

					string readWrite = policy + "-rw";
					authorization.AddPolicy(readWrite, claims => claims.RequireClaim("permissions", readWrite));
				}
			}
		});
		// TODO - change this to use the policies from storage?
		return services;
	}

	public static IApplicationBuilder UseSharedAuthorization(this IApplicationBuilder app)
	{
		app.UseAuthorization();
		return app;
	}
}
