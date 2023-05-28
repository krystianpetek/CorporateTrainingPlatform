using GarageGenius.Shared.Abstractions.Queries;

namespace GarageGenius.Modules.Vehicles.Application.Queries.GetVehicle;
public record GetVehicleQuery : IQuery<GetVehicleQueryDto>
{
	public Guid VehicleId { get; init; }

	public GetVehicleQuery(Guid VehicleId)
	{
		this.VehicleId = VehicleId;
	}
}
