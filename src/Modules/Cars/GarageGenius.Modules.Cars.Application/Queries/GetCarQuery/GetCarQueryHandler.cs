using GarageGenius.Modules.Cars.Application.Dto;
using GarageGenius.Modules.Cars.Core.Entities;
using GarageGenius.Modules.Cars.Core.Repositories;
using GarageGenius.Shared.Abstractions.Queries;

namespace GarageGenius.Modules.Cars.Application.Queries.GetCarQuery;
internal class GetCarQueryHandler : IQueryHandler<GetCarQuery, GetCarDto>
{
    private readonly Serilog.ILogger _logger;
    private readonly ICarRepository _carRepository;

    public GetCarQueryHandler(
        Serilog.ILogger logger,
        ICarRepository carRepository)
    {
        _logger = logger;
        _carRepository = carRepository;
    }

    public async Task<GetCarDto> HandleAsync(GetCarQuery query, CancellationToken cancellationToken = default)
    {
        Car? car = await _carRepository.GetCarAsync(query.CarId, cancellationToken);

        _logger.Information(
            messageTemplate: "Query {QueryName} handled by {ModuleName} module, retrieved car with ID: {CarId}",
            nameof(GetCarQuery),
            nameof(Cars),
            car.Id);
        return car.AsGetUserDto();
    }
}
