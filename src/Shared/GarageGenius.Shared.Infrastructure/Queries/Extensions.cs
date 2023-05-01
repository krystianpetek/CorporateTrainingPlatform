using GarageGenius.Shared.Abstractions.Queries;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GarageGenius.Shared.Infrastructure.Queries;
internal static class Extensions
{
    public static IServiceCollection AddQueryHandlers(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        services.AddSingleton<IQueryDispatcher, QueryDispatcher>();

        IEnumerable<Type> types = assemblies.SelectMany(x => x.GetTypes().Where(t => t.GetInterfaces().Any(any => any.IsGenericType && any.GetGenericTypeDefinition() == typeof(IQueryHandler<,>))));
        foreach (var type in types)
        {
            services.AddScoped(type.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)), type);
        }
        return services;
    }
}
