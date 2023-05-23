using GarageGenius.Shared.Abstractions.Events;
using GarageGenius.Shared.Abstractions.MessageBroker;
using Microsoft.Extensions.Logging;

namespace GarageGenius.Shared.Infrastructure.MessageBroker;

internal sealed class InMemoryMessageBroker : IMessageBroker
{
    private readonly IEventChannel _eventChannel;
    private readonly ILogger<InMemoryMessageBroker> _logger;

    public InMemoryMessageBroker(IEventChannel eventChannel, ILogger<InMemoryMessageBroker> logger)
    {
        _eventChannel = eventChannel;
        _logger = logger;
    }

    public async Task PublishAsync(IEvent @event, CancellationToken cancellationToken = default)
    {
        var name = @event.GetType().Name;
        _logger.LogInformation("Publishing an event: {Name}...", name);
        await _eventChannel.Writer.WriteAsync(@event, cancellationToken);
    }
}