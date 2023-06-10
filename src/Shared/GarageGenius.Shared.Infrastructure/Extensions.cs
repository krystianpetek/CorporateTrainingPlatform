using GarageGenius.Shared.Abstractions.Modules;
using GarageGenius.Shared.Infrastructure.Authentication;
using GarageGenius.Shared.Infrastructure.Authorization;
using GarageGenius.Shared.Infrastructure.Commands;
using GarageGenius.Shared.Infrastructure.Cors;
using GarageGenius.Shared.Infrastructure.Dispatchers;
using GarageGenius.Shared.Infrastructure.Events;
using GarageGenius.Shared.Infrastructure.HealthCheck;
using GarageGenius.Shared.Infrastructure.MessageBroker;
using GarageGenius.Shared.Infrastructure.Queries;
using GarageGenius.Shared.Infrastructure.Services;
using GarageGenius.Shared.Infrastructure.SignalR;
using GarageGenius.Shared.Infrastructure.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GarageGenius.Shared.Infrastructure;
public static class Extensions
{
	public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration, IList<Assembly> assemblies, IList<IModule> modules)
	{
		services.AddSharedAuthentication(assemblies, configuration);
		services.AddSharedAuthorization(modules);
		services.AddSharedSwagger();
		services.AddSharedHealthCheck();
		services.AddSharedCors();

		services.AddSharedEventHandlers(assemblies);
		services.AddSharedCommandHandlers(assemblies);
		services.AddSharedQueryHandlers(assemblies);
		services.AddSharedInMemoryDispatcher();
		services.AddSharedMessageBroker();
		services.AddSharedSignalR();

		services.AddSharedSystemDate();
		services.AddSharedCurrentUser();
		services.AddHostedService<DbContextWorker>();
		return services;
	}

	public static IApplicationBuilder UseSharedInfrastructure(this IApplicationBuilder app)
	{
		app.UseSharedAuthentication();
		app.UseSharedAuthorization();
		app.UseSharedSwagger();
		app.UseSharedCors();
		return app;
	}
}
