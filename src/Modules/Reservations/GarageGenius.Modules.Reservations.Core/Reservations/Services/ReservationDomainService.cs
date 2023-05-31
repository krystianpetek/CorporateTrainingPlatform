using GarageGenius.Modules.Reservations.Core.ReservationHistories.Entities;
using GarageGenius.Modules.Reservations.Core.ReservationHistories.Repositories;
using GarageGenius.Modules.Reservations.Core.ReservationHistories.ValueObjects;
using GarageGenius.Modules.Reservations.Core.Reservations.Entities;
using GarageGenius.Modules.Reservations.Core.Reservations.Repositories;
using GarageGenius.Modules.Reservations.Core.Reservations.ValueObjects;

namespace GarageGenius.Modules.Reservations.Core.Reservations.Services;
internal class ReservationDomainService : IReservationDomainService
{
	private readonly Serilog.ILogger _logger;
	private readonly IReservationRepository _reservationRepository;
	private readonly IReservationHistoryRepository _reservationHistoryRepository;

	public ReservationDomainService(
		Serilog.ILogger logger,
		IReservationRepository reservationRepository,
		IReservationHistoryRepository reservationHistoryRepository
		)
	{
		_logger = logger;
		_reservationRepository = reservationRepository;
		_reservationHistoryRepository = reservationHistoryRepository;
	}

	public async Task AddReservation(Reservation reservation, CancellationToken cancellationToken = default)
	{
		await _reservationRepository.AddReservationAsync(reservation, cancellationToken);
		_logger.Information(
			messageTemplate: "Reservation with ID: {ReservationId} added",
			reservation.ReservationId);

		await AddReservationHistory(reservation.ReservationId, ReservationState.Pending, reservation.ReservationNote.Value, cancellationToken);
	}

	public async Task UpdateReservation(Reservation reservation, ReservationState reservationState, Comment comment, CancellationToken cancellationToken = default)
	{
		reservation.ChangeState(reservationState);
		await _reservationRepository.UpdateReservationAsync(reservation, cancellationToken);

		_logger.Information(
			messageTemplate: "Reservation with ID: {ReservationId} updated",
			reservation.ReservationId);

		await AddReservationHistory(reservation.ReservationId, reservationState, comment, cancellationToken);
	}

	public async Task CompleteReservation(Reservation reservation, string ReservationResultNote, CancellationToken cancellationToken = default)
	{
		reservation.ChangeState(ReservationState.Completed);
		await _reservationRepository.UpdateReservationAsync(reservation, cancellationToken);

		_logger.Information(
			messageTemplate: "Reservation with ID: {ReservationId} completed",
			reservation.ReservationId);

		await AddReservationHistory(reservation.ReservationId, ReservationState.Completed, ReservationResultNote, cancellationToken);
		// TODO change comment
	}

	private async Task AddReservationHistory(Guid reservationId, string reservationState, string reservationComment, CancellationToken cancellationToken = default)
	{
		ReservationHistory reservationHistory = new ReservationHistory(reservationId, reservationState, reservationComment);
		await _reservationHistoryRepository.AddReservationHistoryAsync(reservationHistory, cancellationToken);

		_logger.Information(
			messageTemplate: "Reservation history with ID: {ReservationHistoryId} added for reservation {ReservationId}",
			reservationHistory.ReservationHistoryId,
			reservationId);
	}
}
