using GarageGenius.Modules.Cars.Application.Dto;
using GarageGenius.Modules.Cars.Application.QueryStorage;
using GarageGenius.Shared.Abstractions.Queries;

namespace GarageGenius.Modules.Cars.Application.Queries.GetCustomerCarsQuery;
internal class GetCustomerCarsQueryHandler : IQueryHandler<GetCustomerCarsQuery, IReadOnlyList<GetCarDto>>
{
    private readonly Serilog.ILogger _logger;
    private readonly ICarQueryStorage _carQueryStorage;

    public GetCustomerCarsQueryHandler(
        Serilog.ILogger logger,
       ICarQueryStorage carQueryStorage)
    {
        _logger = logger;
        _carQueryStorage = carQueryStorage;
    }

    public async Task<IReadOnlyList<GetCarDto>> HandleAsync(GetCustomerCarsQuery query, CancellationToken cancellationToken = default)
    {
        IReadOnlyList<GetCarDto> customerCars = await _carQueryStorage.GetCustomerCarsAsync(query.CustomerId, cancellationToken);

        _logger.Information(
            messageTemplate: "Query {QueryName} handled by {ModuleName} module, retrieved cars for customer with ID: {CustomerId}",
            nameof(GetCustomerCarsQuery),
            nameof(Cars),
            query.CustomerId);

        return customerCars;
    }
}
