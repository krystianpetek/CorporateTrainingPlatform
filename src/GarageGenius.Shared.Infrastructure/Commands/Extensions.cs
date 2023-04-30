using GarageGenius.Shared.Abstractions.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GarageGenius.Shared.Infrastructure.Commands;
public static class Extensions
{
    public static IServiceCollection AddCommandHandlers(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        services.AddSingleton<ICommandDispatcher, CommandDispatcher>();


        IEnumerable<Type> types = assemblies.SelectMany(x => x.GetTypes().Where(t => t.GetInterfaces().Any(any => any.IsGenericType && any.GetGenericTypeDefinition() == typeof(ICommandHandler<>))));
        foreach (var type in types)
        {
            services.AddScoped(type.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<>)), type);
        }
        return services;
    }
}
