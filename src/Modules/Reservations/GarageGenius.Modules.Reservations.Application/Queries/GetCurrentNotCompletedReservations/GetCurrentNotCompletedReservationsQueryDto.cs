using GarageGenius.Modules.Reservations.Application.Queries.GetCustomerReservations;
using GarageGenius.Shared.Abstractions.Helpers;

namespace GarageGenius.Modules.Reservations.Application.Queries.GetCurrentNotCompletedReservations;
public sealed record class GetCurrentNotCompletedReservationsQueryDto
{
	public PaginatedList<CurrentNotCompletedReservationsDto> CurrentNotCompletedReservationsDtos { get; init; }

	public GetCurrentNotCompletedReservationsQueryDto(PaginatedList<CurrentNotCompletedReservationsDto> CurrentNotCompletedReservationsDtos)
	{
		this.CurrentNotCompletedReservationsDtos = CurrentNotCompletedReservationsDtos;
	}
}

public sealed record class CurrentNotCompletedReservationsDto(Guid ReservationId, Guid VehicleId, Guid CustomerId, string ReservationState, DateTime ReservationDate, string Comment);
