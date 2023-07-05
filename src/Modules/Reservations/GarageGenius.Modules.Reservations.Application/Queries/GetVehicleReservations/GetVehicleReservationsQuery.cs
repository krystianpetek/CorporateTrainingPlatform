using GarageGenius.Shared.Abstractions.Queries.Query;
using System.Text.Json.Serialization;

namespace GarageGenius.Modules.Reservations.Application.Queries.GetVehicleReservations;
public sealed record class GetVehicleReservationsQuery : IQuery<GetVehicleReservationsQueryDto>
{
	[JsonIgnore]
	public Guid VehicleId { get; set; }

	public GetVehicleReservationsQuery() { }

	public GetVehicleReservationsQuery(Guid vehicleId)
	{
		VehicleId = vehicleId;
	}
}
