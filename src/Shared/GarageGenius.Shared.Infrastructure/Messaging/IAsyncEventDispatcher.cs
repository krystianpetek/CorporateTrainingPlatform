using GarageGenius.Shared.Abstractions.Events;

namespace GarageGenius.Shared.Infrastructure.Messaging;

internal interface IAsyncEventDispatcher
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : class, IEvent;
}