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

public sealed record class CustomerReservationsDto(Guid ReservationId, Guid VehicleId, string ReservationState, DateTime ReservationDate, string Comment)
{
	public string VehicleName { get; set; }
	public CustomerReservationsDto(Guid ReservationId, Guid VehicleId, string VehicleName, string ReservationState, DateTime ReservationDate, string Comment) : this(ReservationId, VehicleId, ReservationState, ReservationDate, Comment)
	{
		this.ReservationId = ReservationId;
		this.VehicleId = VehicleId;
		this.VehicleName = VehicleName;
		this.ReservationState = ReservationState;
		this.ReservationDate = ReservationDate;
		this.Comment = Comment;
	}
}
