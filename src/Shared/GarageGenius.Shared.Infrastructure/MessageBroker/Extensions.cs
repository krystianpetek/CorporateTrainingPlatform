using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Shared.Infrastructure.MessageBroker;

internal static class Extensions
{
    private const string SectionName = "messaging";

    public static IServiceCollection AddMessageBroker(this IServiceCollection services)
    {
        services.AddTransient<IMessageBroker, InMemoryMessageBroker>();
        services.AddTransient<IAsyncEventDispatcher, AsyncEventDispatcher>();
        services.AddSingleton<IEventChannel, EventChannel>();
        services.AddHostedService<EventDispatcherWorker>();

        return services;
    }
}