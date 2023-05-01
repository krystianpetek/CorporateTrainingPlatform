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

    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : class, IEvent
    {
        using IServiceScope? serviceScope = _serviceProvider.CreateScope();
        IEnumerable<IEventHandler<IEvent>> eventHandlers = serviceScope.ServiceProvider.GetServices<IEventHandler<IEvent>>();

        IEnumerable<Task> tasks = eventHandlers.Select(eventToHandle => eventToHandle.HandleAsync(@event, cancellationToken));
        await Task.WhenAll(tasks);
    }
}
