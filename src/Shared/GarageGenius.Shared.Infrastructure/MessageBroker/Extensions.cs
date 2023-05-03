using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Shared.Infrastructure.MessageBroker;

internal static class Extensions
{
    public static IServiceCollection AddMessageBroker(this IServiceCollection services)
    {
        services.AddTransient<IMessageBroker, InMemoryMessageBroker>();
        services.AddSingleton<IEventChannel, EventChannel>();
        services.AddHostedService<EventDispatcherWorker>();

        return services;
    }
}