using GarageGenius.Shared.Abstractions.Helpers;

namespace GarageGenius.Modules.Reservations.Application.Queries.GetCustomerReservations;
public sealed record class GetCustomerReservationsQueryDto
{
	public Guid CustomerId { get; init; }
	public PaginatedList<CustomerReservationsDto> CustomerReservationsDto { get; init; }

	public GetCustomerReservationsQueryDto(Guid CustomerId, PaginatedList<CustomerReservationsDto> CustomerReservationsDto)
	{
		this.CustomerId = CustomerId;
		this.CustomerReservationsDto = CustomerReservationsDto;
	}
}

public sealed record class CustomerReservationsDto(Guid ReservationId, Guid VehicleId, string ReservationState, DateTime ReservationDate, string Comment);
