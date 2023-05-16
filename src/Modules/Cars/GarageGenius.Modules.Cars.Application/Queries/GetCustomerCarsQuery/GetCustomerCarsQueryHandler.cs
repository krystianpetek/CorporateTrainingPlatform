using GarageGenius.Modules.Cars.Application.Dto;
using GarageGenius.Modules.Cars.Core.Entities;
using GarageGenius.Modules.Cars.Core.Repositories;
using GarageGenius.Shared.Abstractions.Queries;

namespace GarageGenius.Modules.Cars.Application.Queries.GetCarQuery;
internal class GetCustomerCarsQueryHandler : IQueryHandler<GetCustomerCarsQuery, IReadOnlyList<GetCarDto>>
{
    private readonly Serilog.ILogger _logger;
    private readonly ICarRepository _carRepository;

    public GetCustomerCarsQueryHandler(
        Serilog.ILogger logger,
        ICarRepository carRepository)
    {
        _logger = logger;
        _carRepository = carRepository;
    }

    public async Task<IReadOnlyList<GetCarDto>> HandleAsync(GetCustomerCarsQuery query, CancellationToken cancellationToken = default)
    {
        IReadOnlyList<Car> customerCars = await _carRepository.GetCustomerCarsAsync(query.CustomerId, cancellationToken);

        List<GetCarDto> customerCarsDto = new List<GetCarDto>();
        foreach(Car car in customerCars)
        {
            customerCarsDto.Add(car.AsGetUserDto());
        }

        _logger.Information(
            messageTemplate: "Query {QueryName} handled by {ModuleName} module, retrieved cars for customer with ID: {CustomerId}",
            nameof(GetCustomerCarsQuery),
            nameof(Cars),
            query.CustomerId);

        return customerCarsDto;
    }
}
