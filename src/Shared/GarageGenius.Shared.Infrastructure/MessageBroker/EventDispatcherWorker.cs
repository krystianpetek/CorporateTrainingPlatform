using GarageGenius.Shared.Abstractions.Events;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GarageGenius.Shared.Infrastructure.MessageBroker;

internal sealed class EventDispatcherWorker : BackgroundService
{
    private readonly IEventChannel _eventChannel;
    private readonly IEventDispatcher _eventDispatcher;
    private readonly ILogger<EventDispatcherWorker> _logger;

    public EventDispatcherWorker(IEventChannel eventChannel, IEventDispatcher eventDispatcher,
        ILogger<EventDispatcherWorker> logger)
    {
        _eventChannel = eventChannel;
        _eventDispatcher = eventDispatcher;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var @event in _eventChannel.Reader.ReadAllAsync(stoppingToken))
        {
            try
            {
                await _eventDispatcher.DispatchEventAsync(@event, stoppingToken);
                _logger.LogInformation("Executed event: {EventName} using {BackgroundService}", @event.GetType().Name, nameof(EventDispatcherWorker));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
            }
        }
    }
}