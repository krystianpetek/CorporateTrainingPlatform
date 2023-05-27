using GarageGenius.Shared.Abstractions.Events;

namespace GarageGenius.Modules.Reservations.Application.Events;
internal class ReservationUpdatedHandler : IEventHandler<ReservationUpdated>
{
    private readonly Serilog.ILogger _logger;

    public ReservationUpdatedHandler(Serilog.ILogger logger)
    {
        _logger = logger;
    }

    public Task HandleEventAsync(ReservationUpdated @event, CancellationToken cancellationToken = default)
    {
        // TODO - maybe move to other module
        _logger.Information(
            messageTemplate: "Event {EventName} handled by {ModuleName} module, updated reservation with ID: {ReservationId}",
            nameof(ReservationUpdated),
            nameof(Reservations),
            @event.reservationId);

        return Task.CompletedTask;
    }
}
