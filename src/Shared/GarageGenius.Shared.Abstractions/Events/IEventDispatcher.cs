namespace GarageGenius.Shared.Abstractions.Events;
public interface IEventDispatcher
{
    Task DispatchEventAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : class, IEvent;
}
