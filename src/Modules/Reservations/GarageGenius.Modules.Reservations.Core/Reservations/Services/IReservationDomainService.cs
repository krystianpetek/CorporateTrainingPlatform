using GarageGenius.Modules.Reservations.Core.ReservationHistories.ValueObjects;
using GarageGenius.Modules.Reservations.Core.Reservations.Entities;
using GarageGenius.Modules.Reservations.Core.Reservations.ValueObjects;

namespace GarageGenius.Modules.Reservations.Core.Reservations.Services;
internal interface IReservationDomainService
{
	Task AddReservation(Reservation reservation, CancellationToken cancellationToken = default);
	Task UpdateReservation(Reservation reservation, ReservationState reservationState, Comment comment, CancellationToken cancellationToken = default);
}
