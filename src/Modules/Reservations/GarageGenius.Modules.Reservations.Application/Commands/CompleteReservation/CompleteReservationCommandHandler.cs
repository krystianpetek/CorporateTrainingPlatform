using GarageGenius.Modules.Reservations.Application.Commands.AddReservation;
using GarageGenius.Modules.Reservations.Application.Events.ReservationAdded;
using GarageGenius.Modules.Reservations.Application.Events.ReservationCompleted;
using GarageGenius.Modules.Reservations.Core.Reservations.Entities;
using GarageGenius.Modules.Reservations.Core.Reservations.Exceptions;
using GarageGenius.Modules.Reservations.Core.Reservations.Repositories;
using GarageGenius.Modules.Reservations.Core.Reservations.Services;
using GarageGenius.Shared.Abstractions.Commands;
using GarageGenius.Shared.Abstractions.MessageBroker;
using GarageGenius.Shared.Abstractions.Services;

namespace GarageGenius.Modules.Reservations.Application.Commands.CompleteReservation;
internal class CompleteReservationCommandHandler : ICommandHandler<CompleteReservationCommand>
{
	private readonly Serilog.ILogger _logger;
	private readonly IMessageBroker _messageBroker;
	private readonly IReservationRepository _reservationRepository;
	private readonly IReservationDomainService _reservationDomainService;

	public CompleteReservationCommandHandler(
		Serilog.ILogger logger,
		IMessageBroker messageBroker,
		IReservationRepository reservationRepository,
		IReservationDomainService reservationDomainService)
	{
		_logger = logger;
		_messageBroker = messageBroker;
		_reservationRepository = reservationRepository;
		_reservationDomainService = reservationDomainService;
	}

	public async Task HandleCommandAsync(CompleteReservationCommand command, CancellationToken cancellationToken = default)
	{
		Reservation reservation = await _reservationRepository.GetReservationAsync(command.ReservationId, cancellationToken) ?? throw new ReservationNotFoundException(command.ReservationId);
		await _reservationDomainService.CompleteReservation(reservation,command.ReservationResultNote!, cancellationToken);

		_logger.Information(
			messageTemplate: "Command {CommandName} handled by {ModuleName} module, reservation with ID: {ReservationId} mark as completed",
			nameof(CompleteReservationCommand),
			nameof(Reservations),
			reservation.ReservationId);

		await _messageBroker.PublishAsync(new ReservationCompletedEvent(reservation.ReservationId), cancellationToken);

		_logger.Information(
			messageTemplate: "Event {EventName} published by {ModuleName} module, reservation with ID: {ReservationId} completed",
			nameof(ReservationCompletedEvent),
			nameof(Reservations),
			reservation.ReservationId);
	}
}
