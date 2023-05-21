namespace GarageGenius.Shared.Abstractions.Events;
public interface IEventHandler<in TEvent> where TEvent : class, IEvent
{
    Task HandleEventAsync(TEvent @event, CancellationToken cancellationToken = default);
}