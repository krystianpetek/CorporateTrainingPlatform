using GarageGenius.Shared.Abstractions.Events;

namespace GarageGenius.Modules.Reservations.Application.Events.ReservationUpdated;
internal class ReservationUpdatedEventHandler : IEventHandler<ReservationUpdatedEvent>
{
	private readonly Serilog.ILogger _logger;

	public ReservationUpdatedEventHandler(Serilog.ILogger logger)
	{
		_logger = logger;
	}

	public Task HandleEventAsync(ReservationUpdatedEvent @event, CancellationToken cancellationToken = default)
	{
		// TODO - maybe move to other module
		_logger.Information(
			messageTemplate: "Event {EventName} handled by {ModuleName} module, updated reservation with ID: {ReservationId}",
			nameof(ReservationUpdatedEvent),
			nameof(Reservations),
			@event.reservationId);

		return Task.CompletedTask;
	}
}
