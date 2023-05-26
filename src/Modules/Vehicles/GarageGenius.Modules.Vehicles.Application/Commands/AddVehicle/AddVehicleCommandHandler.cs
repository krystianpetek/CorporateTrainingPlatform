using GarageGenius.Modules.Vehicles.Application.Queries.GetFilteredVehicle;
using GarageGenius.Modules.Vehicles.Application.QueryStorage;
using GarageGenius.Modules.Vehicles.Core.Entities;
using GarageGenius.Modules.Vehicles.Core.Exceptions;
using GarageGenius.Modules.Vehicles.Core.Repositories;
using GarageGenius.Shared.Abstractions.Commands;

namespace GarageGenius.Modules.Vehicles.Application.Commands.AddVehicle;
internal class AddVehicleCommandHandler : ICommandHandler<AddVehicleCommand>
{
    private readonly Serilog.ILogger _logger;
    private readonly IVehicleRepository _vehiclesRepository;
    private readonly IVehicleQueryStorage _vehicleQueryStorage;

    public AddVehicleCommandHandler(
        Serilog.ILogger logger,
        IVehicleRepository vehiclesRepository,
        IVehicleQueryStorage vehicleQueryStorage)
    {
        _logger = logger;
        _vehiclesRepository = vehiclesRepository;
        _vehicleQueryStorage = vehicleQueryStorage;
    }

    public async Task HandleCommandAsync(AddVehicleCommand command, CancellationToken cancellationToken = default)
    {
        GetVehicleFilterQueryDto vehicleQuery = await _vehicleQueryStorage.SearchVehicleAsync(new Core.Models.GetVehicleFilterParameters(command.Vin, null), cancellationToken);
        if (vehicleQuery != null) throw new VehicleAlreadyExistsException(vehicleQuery.Id);

        Vehicle vehicle = new Vehicle(command.CustomerId, command.Manufacturer, command.Model, command.LicensePlate, command.Vin, command.Year);
        await _vehiclesRepository.AddVehicleAsync(vehicle, cancellationToken);

        _logger.Information(
            messageTemplate: "Command {CommandName} handled by {ModuleName} module, added new vehicle with ID: {VehicleId} to customer with ID: {CustomerId}",
            nameof(AddVehicleCommand),
            nameof(Vehicles),
            vehicle.VehicleId,
            command.CustomerId);
    }
}
