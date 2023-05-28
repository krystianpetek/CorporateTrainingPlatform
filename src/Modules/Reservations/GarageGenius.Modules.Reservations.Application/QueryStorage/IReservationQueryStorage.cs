using GarageGenius.Modules.Reservations.Application.Queries.GetCustomerReservations;
using GarageGenius.Modules.Reservations.Application.Queries.GetReservation;
using GarageGenius.Modules.Reservations.Application.Queries.GetReservationHistory;

namespace GarageGenius.Modules.Reservations.Application.QueryStorage;
public interface IReservationQueryStorage
{
	public Task<GetReservationQueryDto?> GetReservationAsync(Guid reservationId, CancellationToken cancellationToken = default);
	public Task<GetReservationHistoryQueryDtos> GetReservationHistoryAsync(Guid reservationId, CancellationToken cancellationToken = default);
	public Task<GetCustomerReservationsQueryDto> GetCustomerReservationsAsync(Guid customerId, CancellationToken cancellationToken = default);
}
