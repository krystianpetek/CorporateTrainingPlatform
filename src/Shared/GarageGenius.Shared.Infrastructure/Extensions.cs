using GarageGenius.Shared.Abstractions.Authorization;
using GarageGenius.Shared.Infrastructure.Authorization;
using GarageGenius.Shared.Infrastructure.Commands;
using GarageGenius.Shared.Infrastructure.Dispatchers;
using GarageGenius.Shared.Infrastructure.Events;
using GarageGenius.Shared.Infrastructure.MessageBroker;
using GarageGenius.Shared.Infrastructure.Queries;
using GarageGenius.Shared.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GarageGenius.Shared.Infrastructure;
public static class Extensions
{
    public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IList<Assembly> assemblies)
    {

        services.AddEventHandlers(assemblies);
        services.AddCommandHandlers(assemblies);
        services.AddQueryHandlers(assemblies);
        services.AddInMemoryDispatcher();
        services.AddSystemDate();
        services.AddPasswordManager();
        services.AddMessageBroker();
        services.AddHostedService<DbContextWorker>();
        services.AddSingleton<IJwtSettings, JwtSettings>();
        services.AddTransient<IJwtTokenService, JwtTokenService>();
        //services.Configure<JwtSettings>(jwtSettings => configuration.GetRequiredSection("JwtSecret").Bind(jwtSettings));

        return services;
    }

    public static IApplicationBuilder UseSharedInfrastructure(this IApplicationBuilder app)
    {
        return app;
    }
}
