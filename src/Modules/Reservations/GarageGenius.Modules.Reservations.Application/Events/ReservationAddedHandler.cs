using GarageGenius.Shared.Abstractions.Events;

namespace GarageGenius.Modules.Reservations.Application.Events;
internal class ReservationAddedHandler : IEventHandler<ReservationAdded>
{
    private readonly Serilog.ILogger _logger;

    public ReservationAddedHandler(Serilog.ILogger logger)
    {
        _logger = logger;
    }

    public Task HandleEventAsync(ReservationAdded @event, CancellationToken cancellationToken = default)
    {
        // TODO - maybe move to other module
        _logger.Information(
            messageTemplate: "Event {EventName} handled by {ModuleName} module, added reservation with ID: {ReservationId}",
            nameof(ReservationAdded),
            nameof(Reservations),
            @event.reservationId);

        return Task.CompletedTask;
    }
}
