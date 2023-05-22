using GarageGenius.Modules.Reservations.Core.Reservations.Entities;
using GarageGenius.Modules.Reservations.Core.Reservations.Types;

namespace GarageGenius.Modules.Reservations.Core.Reservations.Repositories;
internal interface IReservationRepository
{
    Task<Reservation> GetReservationAsync(ReservationId reservationId, CancellationToken cancellationToken);
    //Task<IEnumerable<Reservation>> GetReservationsAsync(CancellationToken cancellationToken);
    Task/*<Reservation>*/ AddReservationAsync(Reservation reservation, CancellationToken cancellationToken);
    Task/*<Reservation>*/ UpdateReservationAsync(Reservation reservation, CancellationToken cancellationToken);
}
