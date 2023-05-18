using GarageGenius.Modules.Cars.Core.Entities;
using GarageGenius.Modules.Cars.Core.Repositories;
using GarageGenius.Shared.Abstractions.Commands;

namespace GarageGenius.Modules.Cars.Application.Commands.AddCar;
internal class AddCarCommandHandler : ICommandHandler<AddCarCommand>
{
    private readonly Serilog.ILogger _logger;
    private readonly ICarRepository _carsRepository;

    public AddCarCommandHandler(
        Serilog.ILogger logger,
        ICarRepository carsRepository)
    {
        _logger = logger;
        _carsRepository = carsRepository;
    }

    public async Task HandleAsync(AddCarCommand command, CancellationToken cancellationToken = default)
    {
        Car car = new Car(command.CustomerId, command.Manufacturer, command.Model, command.LicensePlate, command.Year, command.Vin);
        await _carsRepository.AddCarAsync(car, cancellationToken);

        _logger.Information(
            messageTemplate: "Command {CommandName} handled by {ModuleName} module, added new car with ID: {CarId} to customer with ID: {CustomerId}",
            nameof(AddCarCommand),
            nameof(Cars),
            car.Id,
            command.CustomerId);
    }
}
