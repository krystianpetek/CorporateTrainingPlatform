using GarageGenius.Modules.Vehicles.Core.Models;
using GarageGenius.Shared.Abstractions.Queries;

namespace GarageGenius.Modules.Vehicles.Application.Queries.GetFilteredVehicle;
public record GetVehicleFilterQuery : IQuery<GetVehicleFilterQueryDto>
{
    public GetVehicleFilterParameters GetVehicleFilterParameters { get; init; }

    public GetVehicleFilterQuery(GetVehicleFilterParameters GetVehicleFilterParameters)
    {
        this.GetVehicleFilterParameters = GetVehicleFilterParameters;
    }
}