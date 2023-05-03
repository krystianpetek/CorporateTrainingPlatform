using GarageGenius.Shared.Abstractions.Dispatcher;
using GarageGenius.Shared.Infrastructure.Commands;
using GarageGenius.Shared.Infrastructure.Date;
using GarageGenius.Shared.Infrastructure.Dispatchers;
using GarageGenius.Shared.Infrastructure.Events;
using GarageGenius.Shared.Infrastructure.Queries;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GarageGenius.Shared.Infrastructure;
public static class Extensions
{
    public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IList<Assembly> assemblies)
    {
        services.AddInMemoryDispatcher();
        services.AddEventHandlers(assemblies);
        services.AddCommandHandlers(assemblies);
        services.AddQueryHandlers(assemblies);
        services.AddSystemDate();
        services.AddHostedService<DbContextWorker>();
        return services;
    }
}
