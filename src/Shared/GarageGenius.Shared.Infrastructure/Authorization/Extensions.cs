using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using GarageGenius.Shared.Abstractions.Modules;
using GarageGenius.Shared.Abstractions.Authorization.AdministratorRequirement;
using GarageGenius.Shared.Abstractions.Authorization.ManagerRequirement;
using GarageGenius.Shared.Abstractions.Authorization.CustomerRequirement;
using GarageGenius.Shared.Abstractions.Authorization.EmployeeRequirement;

namespace GarageGenius.Shared.Infrastructure.Authorization;
public static class Extensions
{
	public static IServiceCollection AddSharedAuthorization(this IServiceCollection services, IList<IModule> modules)
	{
		services.AddAuthorization(authorizationOptions =>
		{
			foreach (var module in modules)
			{
				// register modules policies, all available in system
				foreach (string policy in module.Policies)
				{
					string read = policy + "-r";
					authorizationOptions.AddPolicy(
						read,
						authorizationPolicyBuilder => authorizationPolicyBuilder.RequireClaim("permissions", read));

					string write = policy + "-w";
					authorizationOptions.AddPolicy(
						write,
						authorizationPolicyBuilder => authorizationPolicyBuilder.RequireClaim("permissions", write));

					string readWrite = policy + "-rw";
					authorizationOptions.AddPolicy(
						readWrite,
						authorizationPolicyBuilder => authorizationPolicyBuilder.RequireClaim("permissions", readWrite));
				}
			}

			authorizationOptions.AddAdministratorRolePolicy();
			authorizationOptions.AddManagerRolePolicy();
			authorizationOptions.AddEmployeeRolePolicy();
			authorizationOptions.AddCustomerRolePolicy();

			// authorization.FallbackPolicy = new AuthorizationPolicyBuilder()
			// .RequireAuthenticatedUser()
			// .Build();
			// TODO - fallback policy block swagger ? what to do here?
		});

		services.AddSingleton<IAuthorizationHandler, AdministratorRequirementHandler>();
		services.AddSingleton<IAuthorizationHandler, ManagerRequirementHandler>();
		services.AddSingleton<IAuthorizationHandler, EmployeeRequirementHandler>();
		services.AddSingleton<IAuthorizationHandler, CustomerRequirementHandler>();
		// TODO - change this to use the policies from storage?
		return services;
	}

	public static IApplicationBuilder UseSharedAuthorization(this IApplicationBuilder app)
	{
		app.UseAuthorization();
		return app;
	}
}