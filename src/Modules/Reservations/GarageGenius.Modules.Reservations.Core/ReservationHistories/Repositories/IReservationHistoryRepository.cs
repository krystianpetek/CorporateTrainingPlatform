using GarageGenius.Modules.Reservations.Core.ReservationHistories.Entities;
using GarageGenius.Modules.Reservations.Core.ReservationHistories.Types;
using GarageGenius.Modules.Reservations.Core.Reservations.Types;

namespace GarageGenius.Modules.Reservations.Core.ReservationHistories.Repositories;
internal interface IReservationHistoryRepository
{
	Task<ReservationHistory> GetReservationHistoryAsync(ReservationHistoryId reservationHistoryId, CancellationToken cancellationToken);
	Task<IEnumerable<ReservationHistory>> GetReservationHistoryAsync(ReservationId reservationId, CancellationToken cancellationToken);
	Task/*<Entities.ReservationHistory>*/ AddReservationHistoryAsync(ReservationHistory reservationHistory, CancellationToken cancellationToken);
}
