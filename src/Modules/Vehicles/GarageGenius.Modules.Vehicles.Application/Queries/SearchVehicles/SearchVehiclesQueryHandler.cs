using GarageGenius.Modules.Vehicles.Application.QueryStorage;
using GarageGenius.Modules.Vehicles.Core.Exceptions;
using GarageGenius.Shared.Abstractions.Queries;

namespace GarageGenius.Modules.Vehicles.Application.Queries.SearchVehicles;
internal class SearchVehiclesQueryHandler : IQueryHandler<SearchVehiclesQuery, IReadOnlyList<SearchVehiclesQueryDto>>
{
    private readonly Serilog.ILogger _logger;
    private readonly IVehicleQueryStorage _vehicleQueryStorage;

    public SearchVehiclesQueryHandler(
        Serilog.ILogger logger,
        IVehicleQueryStorage vehicleQueryStorage)
    {
        _logger = logger;
        _vehicleQueryStorage = vehicleQueryStorage;
    }

    public async Task<IReadOnlyList<SearchVehiclesQueryDto>> HandleQueryAsync(SearchVehiclesQuery query, CancellationToken cancellationToken = default)
    {
        if (query.SearchVehiclesParameters.Vin is null && query.SearchVehiclesParameters.LicensePlate is null) throw new OneFilterParameterIsRequired();

        IReadOnlyList<SearchVehiclesQueryDto> searchVehiclesQueryDto = await _vehicleQueryStorage.SearchVehicleAsync(query.SearchVehiclesParameters, cancellationToken) ?? throw new VehicleNotFoundException();

        _logger.Information(
            messageTemplate: "Query {QueryName} handled by {ModuleName} module, retrieved vehicles with filters: {Vin} and {LicencePlate}",
            nameof(GetVehicleQuery),
            nameof(Vehicles),
            query.SearchVehiclesParameters.Vin,
            query.SearchVehiclesParameters.LicensePlate);

        return searchVehiclesQueryDto!;
    }
}
