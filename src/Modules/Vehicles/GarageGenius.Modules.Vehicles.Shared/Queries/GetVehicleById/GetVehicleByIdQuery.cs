using GarageGenius.Shared.Abstractions.Queries.Query;

namespace GarageGenius.Modules.Vehicles.Shared.Queries.GetVehicleById;

public record GetVehicleByIdQuery(Guid VehicleId) : IQuery<GetVehicleByIdQueryDto>;
