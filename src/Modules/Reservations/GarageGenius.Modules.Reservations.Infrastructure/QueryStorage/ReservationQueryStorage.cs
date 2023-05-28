using GarageGenius.Modules.Reservations.Application.Queries.GetReservation;
using GarageGenius.Modules.Reservations.Application.Queries.GetReservationHistory;
using GarageGenius.Modules.Reservations.Application.QueryStorage;
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

	public async Task<IReadOnlyList<GetReservationHistoryQueryDto>> GetReservationHistoryAsync(Guid reservationId, CancellationToken cancellationToken = default)
	{
		IReadOnlyList<GetReservationHistoryQueryDto> getReservationHistoryQueryDtos = await _reservationsDbContext.Reservations
		.AsNoTracking()
		.AsQueryable()
		.Where<Reservation>(reservation => reservation.ReservationId == reservationId)
			.Select<Reservation, GetReservationHistoryQueryDto>(reservation => new GetReservationHistoryQueryDto(reservation.ReservationId, reservation.VehicleId, reservation.ReservationState, reservation.ReservationNote))
			.ToListAsync<GetReservationHistoryQueryDto>(cancellationToken);

		return getReservationHistoryQueryDtos;

	}
}
