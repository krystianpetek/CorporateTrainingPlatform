using GarageGenius.Modules.Vehicles.Application.QueryStorage;
using GarageGenius.Modules.Vehicles.Core.Exceptions;
using GarageGenius.Shared.Abstractions.Queries;

namespace GarageGenius.Modules.Vehicles.Application.Queries.GetFilteredVehicle;
internal class GetVehicleFilterQueryHandler : IQueryHandler<GetVehicleFilterQuery, GetVehicleFilterQueryDto>
{
    private readonly Serilog.ILogger _logger;
    private readonly IVehicleQueryStorage _vehicleQueryStorage;

    public GetVehicleFilterQueryHandler(
        Serilog.ILogger logger,
        IVehicleQueryStorage vehicleQueryStorage)
    {
        _logger = logger;
        _vehicleQueryStorage = vehicleQueryStorage;
    }

    public async Task<GetVehicleFilterQueryDto> HandleQueryAsync(GetVehicleFilterQuery query, CancellationToken cancellationToken = default)
    {
        GetVehicleFilterQueryDto? getVehicleFilterQueryDto = await _vehicleQueryStorage.SearchVehicleAsync(query.GetVehicleFilterParameters, cancellationToken) ?? throw new VehicleNotFoundException();
        
        _logger.Information(
            messageTemplate: "Query {QueryName} handled by {ModuleName} module, retrieved vehicle with ID: {VehicleId}",
            nameof(GetVehicleQuery),
            nameof(Vehicles),
            getVehicleFilterQueryDto?.Id);

        return getVehicleFilterQueryDto!;
    }
}
