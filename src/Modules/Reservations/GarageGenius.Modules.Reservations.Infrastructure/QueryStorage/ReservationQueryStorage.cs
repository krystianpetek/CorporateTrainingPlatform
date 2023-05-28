using GarageGenius.Modules.Reservations.Application.Queries.GetReservation;
using GarageGenius.Modules.Reservations.Application.Queries.GetReservationHistory;
using GarageGenius.Modules.Reservations.Application.QueryStorage;
using GarageGenius.Modules.Reservations.Core.ReservationHistories.Entities;
using GarageGenius.Modules.Reservations.Core.Reservations.Entities;
using GarageGenius.Modules.Reservations.Infrastructure.Persistance.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace GarageGenius.Modules.Reservations.Infrastructure.QueryStorage;
internal class ReservationQueryStorage : IReservationQueryStorage
{
	private readonly ReservationsDbContext _reservationsDbContext;

	public ReservationQueryStorage(ReservationsDbContext reservationsDbContext)
	{
		_reservationsDbContext = reservationsDbContext;
	}

	public async Task<GetReservationQueryDto?> GetReservationAsync(Guid reservationId, CancellationToken cancellationToken = default)
	{
		GetReservationQueryDto? getReservationQueryDto = await _reservationsDbContext.Reservations
		.AsNoTracking()
		.AsQueryable()
		.Where<Reservation>(reservation => reservation.ReservationId == reservationId)
		.Select<Reservation, GetReservationQueryDto>(reservation => new GetReservationQueryDto(reservation.ReservationId, reservation.VehicleId, reservation.ReservationState))
		.SingleOrDefaultAsync<GetReservationQueryDto>(cancellationToken);

		return getReservationQueryDto;
	}

	public async Task<GetReservationHistoryQueryDtos> GetReservationHistoryAsync(Guid reservationId, CancellationToken cancellationToken = default)
	{
		IReadOnlyList<ReservationHistoriesDto> reservationHistoriesDto = await _reservationsDbContext.ReservationHistories
		.AsNoTracking()
		.AsQueryable()
		.Where<ReservationHistory>(reservationHistory => reservationHistory.ReservationId == reservationId)
		.Select<ReservationHistory, ReservationHistoriesDto>(reservationHistory => new ReservationHistoriesDto(reservationHistory.ReservationHistoryId, reservationHistory.ReservationState, reservationHistory.Comment))
		.ToListAsync(cancellationToken);

		GetReservationHistoryQueryDtos getReservationHistoryQueryDto = new GetReservationHistoryQueryDtos(reservationId, reservationHistoriesDto);

		return getReservationHistoryQueryDto;

	}
}
