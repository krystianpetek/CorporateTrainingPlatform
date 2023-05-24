using GarageGenius.Modules.Reservations.Core.Reservations.Entities;

namespace GarageGenius.Modules.Reservations.Core.Reservations.Services;
internal interface IReservationDomainService
{
    Task AddReservation(Reservation reservation, CancellationToken cancellationToken = default);
}
