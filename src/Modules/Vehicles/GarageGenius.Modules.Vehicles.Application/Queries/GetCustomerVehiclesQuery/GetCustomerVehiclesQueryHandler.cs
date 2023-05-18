using GarageGenius.Modules.Vehicles.Application.Dto;
using GarageGenius.Modules.Vehicles.Application.QueryStorage;
using GarageGenius.Shared.Abstractions.Queries;

namespace GarageGenius.Modules.Vehicles.Application.Queries.GetCustomerVehiclesQuery;
internal class GetCustomerVehiclesQueryHandler : IQueryHandler<GetCustomerVehiclesQuery, IReadOnlyList<GetVehicleDto>>
{
    private readonly Serilog.ILogger _logger;
    private readonly IVehicleQueryStorage _vehicleQueryStorage;

    public GetCustomerVehiclesQueryHandler(
        Serilog.ILogger logger,
       IVehicleQueryStorage vehicleQueryStorage)
    {
        _logger = logger;
        _vehicleQueryStorage = vehicleQueryStorage;
    }

    public async Task<IReadOnlyList<GetVehicleDto>> HandleAsync(GetCustomerVehiclesQuery query, CancellationToken cancellationToken = default)
    {
        IReadOnlyList<GetVehicleDto> customerVehicles = await _vehicleQueryStorage.GetCustomerVehiclesAsync(query.CustomerId, cancellationToken);

        _logger.Information(
            messageTemplate: "Query {QueryName} handled by {ModuleName} module, retrieved vehicles for customer with ID: {CustomerId}",
            nameof(GetCustomerVehiclesQuery),
            nameof(Vehicles),
            query.CustomerId);

        return customerVehicles;
    }
}
