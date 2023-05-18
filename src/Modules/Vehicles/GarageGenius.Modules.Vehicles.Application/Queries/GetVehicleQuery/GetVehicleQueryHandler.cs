using GarageGenius.Modules.Vehicles.Application.Dto;
using GarageGenius.Modules.Vehicles.Application.QueryStorage;
using GarageGenius.Shared.Abstractions.Queries;

namespace GarageGenius.Modules.Vehicles.Application.Queries.GetVehicleQuery;
internal class GetVehicleQueryHandler : IQueryHandler<GetVehicleQuery, GetVehicleDto>
{
    private readonly Serilog.ILogger _logger;
    private readonly IVehicleQueryStorage _vehicleQueryStorage;

    public GetVehicleQueryHandler(
        Serilog.ILogger logger,
       IVehicleQueryStorage vehicleQueryStorage)
    {
        _logger = logger;
        _vehicleQueryStorage = vehicleQueryStorage;
    }

    public async Task<GetVehicleDto> HandleAsync(GetVehicleQuery query, CancellationToken cancellationToken = default)
    {
        GetVehicleDto? getVehicleDto = await _vehicleQueryStorage.GetVehicleAsync(query.VehicleId, cancellationToken);

        _logger.Information(
            messageTemplate: "Query {QueryName} handled by {ModuleName} module, retrieved vehicle with ID: {VehicleId}",
            nameof(GetVehicleQuery),
            nameof(Vehicles),
            getVehicleDto?.Id);

        return getVehicleDto;
    }
}
