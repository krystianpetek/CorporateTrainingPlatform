using System.Threading;
using System.Threading.Tasks;
using GarageGenius.Shared.Abstractions.Events;

namespace GarageGenius.Shared.Infrastructure.MessageBroker;

internal sealed class AsyncEventDispatcher : IAsyncEventDispatcher
{
    private readonly IEventChannel _channel;

    public AsyncEventDispatcher(IEventChannel channel)
    {
        _channel = channel;
    }

    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : class, IEvent
    {
        await _channel.Writer.WriteAsync(@event, cancellationToken);
    }
}