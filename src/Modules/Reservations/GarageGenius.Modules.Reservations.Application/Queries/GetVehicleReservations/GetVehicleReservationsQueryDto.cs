namespace GarageGenius.Modules.Reservations.Application.Queries.GetCustomerReservations;
public sealed record class GetVehicleReservationsQueryDto
{
	public Guid VehicleId { get; init; }
	public List<VehicleReservationsDto> CustomerReservationsDto { get; init; }

	public GetVehicleReservationsQueryDto(Guid VehicleId, List<VehicleReservationsDto> CustomerReservationsDto)
	{
		this.VehicleId = VehicleId;
		this.CustomerReservationsDto = CustomerReservationsDto;
	}
}

public sealed record class VehicleReservationsDto(Guid ReservationId, string ReservationState, DateTime ReservationDate, string Comment);