using GarageGenius.Modules.Vehicles.Application.QueryStorage;
using GarageGenius.Modules.Vehicles.Core.Exceptions;
using GarageGenius.Shared.Abstractions.Queries.Query;

namespace GarageGenius.Modules.Vehicles.Application.Queries.GetVehicle;
internal class GetVehicleQueryHandler : IQueryHandler<GetVehicleQuery, GetVehicleQueryDto>
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

	public async Task<GetVehicleQueryDto> HandleQueryAsync(GetVehicleQuery query, CancellationToken cancellationToken = default)
	{
		GetVehicleQueryDto? getVehicleDto = await _vehicleQueryStorage.GetVehicleAsync(query.VehicleId, cancellationToken) ?? throw new VehicleNotFoundException(query.VehicleId);

		_logger.Information(
			messageTemplate: "Query {QueryName} handled by {ModuleName} module, retrieved vehicle with ID: {VehicleId}",
			nameof(GetVehicleQuery),
			nameof(Vehicles),
			getVehicleDto?.Id);

		return getVehicleDto!;
	}
}
