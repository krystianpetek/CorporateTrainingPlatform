using GarageGenius.Modules.Vehicles.Application.Queries.GetVehicle;
using GarageGenius.Modules.Vehicles.Application.QueryStorage;
using GarageGenius.Modules.Vehicles.Core.Exceptions;
using GarageGenius.Modules.Vehicles.Shared.Queries.GetVehicleById;
using GarageGenius.Shared.Abstractions.Queries.Query;

namespace GarageGenius.Modules.Vehicles.Application.Queries.GetVehicleById;
internal class GetVehicleByIdQueryHandler : IQueryHandler<GetVehicleByIdQuery, GetVehicleByIdQueryDto>
{
	private readonly Serilog.ILogger _logger;
	private readonly IVehicleQueryStorage _vehicleQueryStorage;

	public GetVehicleByIdQueryHandler(
		Serilog.ILogger logger,
	   IVehicleQueryStorage vehicleQueryStorage)
	{
		_logger = logger;
		_vehicleQueryStorage = vehicleQueryStorage;
	}

	public async Task<GetVehicleByIdQueryDto> HandleQueryAsync(GetVehicleByIdQuery query, CancellationToken cancellationToken = default)
	{
		GetVehicleQueryDto? getVehicleDto = await _vehicleQueryStorage.GetVehicleAsync(query.VehicleId, cancellationToken) ?? throw new VehicleNotFoundException(query.VehicleId);
		GetVehicleByIdQueryDto? getVehicleByIdDto = new GetVehicleByIdQueryDto
		{
			Id = getVehicleDto.Id,
			Manufacturer = getVehicleDto.Manufacturer,
			Model = getVehicleDto.Model,
			Year = getVehicleDto.Year,
			LicensePlate = getVehicleDto.LicensePlate,
			Vin = getVehicleDto.Vin,
		};

		_logger.Information(
			messageTemplate: "Query {QueryName} handled by {ModuleName} module, retrieved vehicle with ID: {VehicleId}",
			nameof(GetVehicleByIdQueryDto),
			nameof(Vehicles),
			getVehicleByIdDto?.Id);

		return getVehicleByIdDto!;
	}
}