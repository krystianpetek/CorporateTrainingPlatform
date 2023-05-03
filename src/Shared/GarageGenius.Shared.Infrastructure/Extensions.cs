using GarageGenius.Shared.Abstractions.Authorization;
using GarageGenius.Shared.Infrastructure.Commands;
using GarageGenius.Shared.Infrastructure.Date;
using GarageGenius.Shared.Infrastructure.Dispatchers;
using GarageGenius.Shared.Infrastructure.Events;
using GarageGenius.Shared.Infrastructure.Middleware.ErrorHandling;
using GarageGenius.Shared.Infrastructure.Queries;
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
        services.AddErrorHandling();
        services.AddHostedService<DbContextWorker>();
        return services;
    }

    public static IApplicationBuilder UseSharedInfrastructure(this IApplicationBuilder app)
    {
        app.UseErrorHandling();
        return app;
    }
}
