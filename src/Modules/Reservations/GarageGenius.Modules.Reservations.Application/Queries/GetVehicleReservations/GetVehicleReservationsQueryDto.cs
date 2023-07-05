namespace GarageGenius.Modules.Reservations.Application.Queries.GetVehicleReservations;
public sealed record class GetVehicleReservationsQueryDto
{
	public Guid VehicleId { get; init; }
	public List<VehicleReservationsDto> VehicleReservationsDto { get; init; }

	public GetVehicleReservationsQueryDto(Guid VehicleId, List<VehicleReservationsDto> VehicleReservationsDto)
	{
		this.VehicleId = VehicleId;
		this.VehicleReservationsDto = VehicleReservationsDto;
	}
}

public sealed record class VehicleReservationsDto(Guid ReservationId, string ReservationState, DateTime ReservationDate, string Comment);