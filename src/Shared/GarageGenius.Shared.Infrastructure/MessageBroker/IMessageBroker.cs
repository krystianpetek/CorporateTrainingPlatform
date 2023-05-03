using System.Threading;
using System.Threading.Tasks;
using GarageGenius.Shared.Abstractions.Events;

namespace GarageGenius.Shared.Infrastructure.MessageBroker;

public interface IMessageBroker
{
    Task PublishAsync(IEvent @event, CancellationToken cancellationToken = default);
}