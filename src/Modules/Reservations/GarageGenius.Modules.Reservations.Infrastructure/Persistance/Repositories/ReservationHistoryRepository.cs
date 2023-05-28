using GarageGenius.Modules.Reservations.Core.ReservationHistories.Entities;
using GarageGenius.Modules.Reservations.Core.ReservationHistories.Repositories;
using GarageGenius.Modules.Reservations.Core.ReservationHistories.Types;
using GarageGenius.Modules.Reservations.Core.Reservations.Types;
using GarageGenius.Modules.Reservations.Infrastructure.Persistance.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace GarageGenius.Modules.Reservations.Infrastructure.Persistance.Repositories;
internal class ReservationHistoryRepository : IReservationHistoryRepository
{
	private readonly ReservationsDbContext _reservationsDbContext;

	public ReservationHistoryRepository(ReservationsDbContext reservationsDbContext)
	{
		_reservationsDbContext = reservationsDbContext;
	}

	public async Task AddReservationHistoryAsync(ReservationHistory reservationHistory, CancellationToken cancellationToken)
	{
		await _reservationsDbContext.ReservationHistories.AddAsync(reservationHistory, cancellationToken);
		await _reservationsDbContext.SaveChangesAsync(cancellationToken);
	}

	public async Task<ReservationHistory> GetReservationHistoryAsync(ReservationHistoryId reservationHistoryId, CancellationToken cancellationToken)
	{
		ReservationHistory reservationHistory = await _reservationsDbContext.ReservationHistories.FindAsync(reservationHistoryId, cancellationToken);
		return reservationHistory;
	}

	public async Task<IEnumerable<ReservationHistory>> GetReservationHistoryAsync(ReservationId reservationId, CancellationToken cancellationToken)
	{
		IEnumerable<ReservationHistory> reservationHistories = await _reservationsDbContext.ReservationHistories.Where(reservationHistory => reservationHistory.ReservationId == reservationId)
			.ToListAsync();

		return reservationHistories;
	}
}
