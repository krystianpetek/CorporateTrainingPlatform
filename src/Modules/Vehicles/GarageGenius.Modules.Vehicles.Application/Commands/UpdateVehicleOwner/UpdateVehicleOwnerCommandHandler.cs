using GarageGenius.Modules.Vehicles.Application.QueryStorage;
using GarageGenius.Modules.Vehicles.Core.Entities;
using GarageGenius.Modules.Vehicles.Core.Exceptions;
using GarageGenius.Modules.Vehicles.Core.Repositories;
using GarageGenius.Shared.Abstractions.Commands;

namespace GarageGenius.Modules.Vehicles.Application.Commands.UpdateVehicleOwner;
internal class UpdateVehicleOwnerCommandHandler : ICommandHandler<UpdateVehicleOwnerCommand>
{
	private readonly Serilog.ILogger _logger;
	private readonly IVehicleRepository _vehiclesRepository;
	private readonly IVehicleQueryStorage _vehiclesQueryStorage;

	public UpdateVehicleOwnerCommandHandler(
		Serilog.ILogger logger,
		IVehicleRepository vehiclesRepository,
		IVehicleQueryStorage vehiclesQueryStorage)
	{
		_logger = logger;
		_vehiclesRepository = vehiclesRepository;
		_vehiclesQueryStorage = vehiclesQueryStorage;
	}

	public async Task HandleCommandAsync(UpdateVehicleOwnerCommand command, CancellationToken cancellationToken = default)
	{
		//var vehicle = await _vehiclesQueryStorage.GetVehicleAsync(command.VehicleId, cancellationToken); // TODO ?
		Vehicle vehicle = await _vehiclesRepository.GetVehicleAsync(command.VehicleId, cancellationToken) ?? throw new VehicleNotFoundException(command.VehicleId);
		vehicle.ChangeOwner(command.CustomerId);

		await _vehiclesRepository.UpdateVehicleAsync(vehicle, cancellationToken);

		_logger.Information(
			messageTemplate: "Command {CommandName} handled by {ModuleName} module, changed vehicle owner with for vehicle with ID: {VehicleId} to customer with ID: {CustomerId}",
			nameof(UpdateVehicleOwnerCommand),
			nameof(Vehicles),
			vehicle.VehicleId,
			command.CustomerId);
	}
}
