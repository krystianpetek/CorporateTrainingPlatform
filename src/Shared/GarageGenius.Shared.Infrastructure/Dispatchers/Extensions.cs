using GarageGenius.Shared.Abstractions.Dispatcher;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Shared.Infrastructure.Dispatchers;
public static class Extensions
{
    public static IServiceCollection AddSharedInMemoryDispatcher(this IServiceCollection services)
    {
        services.AddSingleton<IDispatcher, InMemoryDispatcher>();
        return services;
    }

}
