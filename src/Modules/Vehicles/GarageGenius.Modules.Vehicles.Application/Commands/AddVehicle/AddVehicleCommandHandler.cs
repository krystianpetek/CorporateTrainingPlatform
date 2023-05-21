using GarageGenius.Modules.Vehicles.Core.Entities;
using GarageGenius.Modules.Vehicles.Core.Repositories;
using GarageGenius.Shared.Abstractions.Commands;

namespace GarageGenius.Modules.Vehicles.Application.Commands.AddVehicle;
internal class AddVehicleCommandHandler : ICommandHandler<AddVehicleCommand>
{
    private readonly Serilog.ILogger _logger;
    private readonly IVehicleRepository _vehiclesRepository;

    public AddVehicleCommandHandler(
        Serilog.ILogger logger,
        IVehicleRepository vehiclesRepository)
    {
        _logger = logger;
        _vehiclesRepository = vehiclesRepository;
    }

    public async Task HandleCommandAsync(AddVehicleCommand command, CancellationToken cancellationToken = default)
    {
        Vehicle vehicle = new Vehicle(command.CustomerId, command.Manufacturer, command.Model, command.LicensePlate, command.Year, command.Vin);
        await _vehiclesRepository.AddVehicleAsync(vehicle, cancellationToken);

        _logger.Information(
            messageTemplate: "Command {CommandName} handled by {ModuleName} module, added new vehicle with ID: {VehicleId} to customer with ID: {CustomerId}",
            nameof(AddVehicleCommand),
            nameof(Vehicles),
            vehicle.Id,
            command.CustomerId);
    }
}
