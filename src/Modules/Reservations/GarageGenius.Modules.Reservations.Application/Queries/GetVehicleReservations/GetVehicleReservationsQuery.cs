using GarageGenius.Shared.Abstractions.Queries.Query;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GarageGenius.Modules.Reservations.Application.Queries.GetCustomerReservations;
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
