using GarageGenius.Shared.Infrastructure.Authentication;
using GarageGenius.Shared.Infrastructure.Commands;
using GarageGenius.Shared.Infrastructure.Dispatchers;
using GarageGenius.Shared.Infrastructure.Events;
using GarageGenius.Shared.Infrastructure.MessageBroker;
using GarageGenius.Shared.Infrastructure.Queries;
using GarageGenius.Shared.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GarageGenius.Shared.Infrastructure;
public static class Extensions
{
    public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration, IList<Assembly> assemblies)
    {
        services.AddSharedAuthentication(assemblies, configuration);
        services.AddSharedEventHandlers(assemblies);
        services.AddSharedCommandHandlers(assemblies);
        services.AddSharedQueryHandlers(assemblies);
        services.AddSharedInMemoryDispatcher();
        services.AddSharedSystemDate();
        services.AddSharedMessageBroker();
        services.AddHostedService<DbContextWorker>();
        return services;
    }

    public static IApplicationBuilder UseSharedInfrastructure(this IApplicationBuilder app)
    {
        return app;
    }
}
