// using GarageGenius.Modules.Reservations.Application.Events.ReservationAdded;
using GarageGenius.Modules.Reservations.Core.Reservations.Entities;
using GarageGenius.Modules.Reservations.Core.Reservations.Services;
using GarageGenius.Modules.Reservations.Shared.Events;
using GarageGenius.Shared.Abstractions.Commands;
using GarageGenius.Shared.Abstractions.MessageBroker;
using GarageGenius.Shared.Abstractions.Services;

namespace GarageGenius.Modules.Reservations.Application.Commands.AddReservation;
internal class AddReservationCommandHandler : ICommandHandler<AddReservationCommand>
{
	private readonly Serilog.ILogger _logger;
	private readonly IMessageBroker _messageBroker;
	private readonly IReservationDomainService _reservationDomainService;
	private readonly ISystemDateService _systemDateService;

	public AddReservationCommandHandler(
		Serilog.ILogger logger,
		IMessageBroker messageBroker,
		IReservationDomainService reservationDomainService,
		ISystemDateService systemDateService)
	{
		_logger = logger;
		_messageBroker = messageBroker;
		_reservationDomainService = reservationDomainService;
		_systemDateService = systemDateService;
	}

	public async Task HandleCommandAsync(AddReservationCommand command, CancellationToken cancellationToken = default)
	{
		Reservation reservation = new Reservation(command.VehicleId, command.CustomerId, command.Comment, command.ReservationDate);
		await _reservationDomainService.AddReservation(reservation, cancellationToken);

		_logger.Information(
			messageTemplate: "Command {CommandName} handled by {ModuleName} module, added new reservation with ID: {ReservationId}",
			nameof(AddReservationCommand),
			nameof(Reservations),
			reservation.ReservationId);

		// await _messageBroker.PublishAsync(new ReservationAddedEvent(reservation.ReservationId, command.Comment), cancellationToken);
		await _messageBroker.PublishAsync(new ReservationAddedEvent(reservation.CustomerId, reservation.ReservationId, reservation.VehicleId), cancellationToken);

		_logger.Information(
			messageTemplate: "Event {EventName} published by {ModuleName} module, reservation with ID: {ReservationId} added",
			nameof(ReservationAddedEvent),
			nameof(Reservations),
			reservation.ReservationId);
	}
}
