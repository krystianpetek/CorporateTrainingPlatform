using GarageGenius.Modules.Reservations.Core.Reservations.Entities;
using GarageGenius.Modules.Reservations.Core.Reservations.Exceptions;
using GarageGenius.Modules.Reservations.Core.Reservations.Repositories;
using GarageGenius.Modules.Reservations.Core.Reservations.Types;
using GarageGenius.Modules.Reservations.Core.Reservations.ValueObjects;
using GarageGenius.Modules.Reservations.Infrastructure.Persistance.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace GarageGenius.Modules.Reservations.Infrastructure.Persistance.Repositories;
internal class ReservationRepository : IReservationRepository
{
	private readonly ReservationsDbContext _reservationsDbContext;

	public ReservationRepository(ReservationsDbContext reservationsDbContext)
	{
		_reservationsDbContext = reservationsDbContext;
	}

	public async Task AddReservationAsync(Reservation reservation, CancellationToken cancellationToken)
	{
		await _reservationsDbContext.Reservations.AddAsync(reservation, cancellationToken);
		await _reservationsDbContext.SaveChangesAsync(cancellationToken);
	}

	public async Task<Reservation> GetReservationAsync(ReservationId reservationId, CancellationToken cancellationToken)
	{
		Reservation? reservation = await _reservationsDbContext.Reservations
			.Where(reservation => !reservation.ReservationDeleted)
			.SingleOrDefaultAsync(reservation => reservation.ReservationId == reservationId, cancellationToken);
		return reservation;
		// TODO move to QueryStorage ?
	}

	public Task UpdateReservationAsync(Reservation reservation, CancellationToken cancellationToken)
	{
		_reservationsDbContext.Reservations.Update(reservation);
		return _reservationsDbContext.SaveChangesAsync(cancellationToken);
	}

	public async Task DeleteReservationAsync(ReservationId reservationId, CancellationToken cancellationToken)
	{
		// get reservation
		Reservation reservation = await _reservationsDbContext.Reservations.SingleOrDefaultAsync(reservation => reservation.ReservationId == reservationId)
			?? throw new ReservationNotFoundException(reservationId);

		// check if reservation is not canceled or pending
		if (reservation.ReservationState != ReservationState.Canceled || reservation.ReservationState != ReservationState.Pending)
			throw new UnableRemoveReservationException(reservationId);

		// remove reservation
		reservation.ReservationDeactivate();
		_reservationsDbContext.Reservations.Update(reservation);

		await _reservationsDbContext.SaveChangesAsync(cancellationToken);

		// TODO this should be in a domain service / aggregate ?
	}
}
