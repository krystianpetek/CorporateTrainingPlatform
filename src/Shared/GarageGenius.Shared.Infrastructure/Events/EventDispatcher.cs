using GarageGenius.Shared.Abstractions.Events;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Shared.Infrastructure.Events;
internal class EventDispatcher : IEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public EventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task DispatchEventAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : class, IEvent
    {
        using IServiceScope? scope = _serviceProvider.CreateScope();
        Type? handlerType = typeof(IEventHandler<>).MakeGenericType(@event.GetType());
        var handlers = scope.ServiceProvider.GetServices(handlerType);
        var method = handlerType.GetMethod(nameof(IEventHandler<IEvent>.HandleEventAsync));
        if (method is null)
        {
            throw new InvalidOperationException($"Event handler for '{@event.GetType().Name}' is invalid.");
        }

        var tasks = handlers.Select(x => (Task)method.Invoke(x, new object[] { @event, cancellationToken }));
        await Task.WhenAll(tasks);
    }
}
