using GarageGenius.Modules.Vehicles.Application.Dto;
using GarageGenius.Shared.Abstractions.Queries;

namespace GarageGenius.Modules.Vehicles.Application.Queries.GetVehicleQuery;
public record GetVehicleQuery : IQuery<GetVehicleDto>
{
    public Guid VehicleId { get; init; }

    public GetVehicleQuery(Guid VehicleId)
    {
        this.VehicleId = VehicleId;
    }
}
