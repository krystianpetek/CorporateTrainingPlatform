using GarageGenius.Modules.Reservations.Core.ReservationHistories.Entities;
using GarageGenius.Modules.Reservations.Core.ReservationHistories.Repositories;
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

        ReservationHistory reservationHistory = new ReservationHistory(reservation.ReservationId, ReservationState.Pending, reservation.ReservationNote.Value);
        await _reservationHistoryRepository.AddReservationHistoryAsync(reservationHistory, cancellationToken);
        _logger.Information(
            messageTemplate: "Reservation history with ID: {ReservationHistoryId} added",
            reservationHistory.ReservationHistoryId);
    }

    public Task AddReservationHistory(ReservationHistory reservationHistory, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
