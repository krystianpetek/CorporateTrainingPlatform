using GarageGenius.Shared.Abstractions.Events;

namespace GarageGenius.Modules.Reservations.Application.Events.ReservationUpdated;
internal class ReservationCompletedEventHandler : IEventHandler<ReservationCompletedEvent>
{
    private readonly Serilog.ILogger _logger;

    public ReservationCompletedEventHandler(Serilog.ILogger logger)
    {
        _logger = logger;
    }

    public Task HandleEventAsync(ReservationCompletedEvent @event, CancellationToken cancellationToken = default)
    {
        // TODO - maybe move to other module
        _logger.Information(
            messageTemplate: "Event {EventName} handled by {ModuleName} module, updated reservation with ID: {ReservationId}",
            nameof(ReservationCompletedEvent),
            nameof(Reservations),
            @event.reservationId);

        return Task.CompletedTask;
    }
}
