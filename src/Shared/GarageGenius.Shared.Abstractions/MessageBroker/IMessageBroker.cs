using GarageGenius.Shared.Abstractions.Events;

namespace GarageGenius.Shared.Abstractions.MessageBroker;

public interface IMessageBroker
{
    Task PublishAsync(IEvent @event, CancellationToken cancellationToken = default);
}