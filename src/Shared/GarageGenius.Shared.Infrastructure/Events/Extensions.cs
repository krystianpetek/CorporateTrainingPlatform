using GarageGenius.Shared.Abstractions.Events;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GarageGenius.Shared.Infrastructure.Events;
public static class Extensions
{
    public static IServiceCollection AddEventHandlers(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        services.AddSingleton<IEventDispatcher, EventDispatcher>();

        IEnumerable<Type> types = assemblies.SelectMany(x => x.GetTypes().Where(t => t.GetInterfaces().Any(any => any.IsGenericType && any.GetGenericTypeDefinition() == typeof(IEventHandler<>))));
        foreach (var type in types)
        {
            services.AddScoped(type.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEventHandler<>)), type);
        }
        return services;
    }
}
