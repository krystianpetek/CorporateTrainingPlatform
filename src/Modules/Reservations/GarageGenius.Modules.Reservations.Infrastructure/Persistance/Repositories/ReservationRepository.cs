using GarageGenius.Modules.Reservations.Core.Reservations.Entities;
using GarageGenius.Modules.Reservations.Core.Reservations.Repositories;
using GarageGenius.Modules.Reservations.Core.Reservations.Types;
using GarageGenius.Modules.Reservations.Infrastructure.Persistance.DbContexts;

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
        Reservation reservation = await _reservationsDbContext.Reservations.FindAsync(reservationId, cancellationToken);
        return reservation;
        // TODO move to QueryStorage ?
    }

    public Task UpdateReservationAsync(Reservation reservation, CancellationToken cancellationToken)
    {
        _reservationsDbContext.Reservations.Update(reservation);
        return _reservationsDbContext.SaveChangesAsync(cancellationToken);
    }
}
