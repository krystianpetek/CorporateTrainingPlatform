using GarageGenius.Shared.Abstractions.Events;

namespace GarageGenius.Modules.Reservations.Application.Events.ReservationAdded;
internal class ReservationAddedEventHandler : IEventHandler<ReservationAddedEvent>
{
    private readonly Serilog.ILogger _logger;

    public ReservationAddedEventHandler(Serilog.ILogger logger)
    {
        _logger = logger;
    }

    public Task HandleEventAsync(ReservationAddedEvent @event, CancellationToken cancellationToken = default)
    {
        // TODO - maybe move to other module
        _logger.Information(
            messageTemplate: "Event {EventName} handled by {ModuleName} module, added reservation with ID: {ReservationId}",
            nameof(ReservationAddedEvent),
            nameof(Reservations),
            @event.reservationId);

        return Task.CompletedTask;
    }
}
