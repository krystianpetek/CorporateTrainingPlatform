using GarageGenius.Modules.Cars.Application.Dto;
using GarageGenius.Modules.Cars.Application.QueryStorage;
using GarageGenius.Shared.Abstractions.Queries;

namespace GarageGenius.Modules.Cars.Application.Queries.GetCarQuery;
internal class GetCarQueryHandler : IQueryHandler<GetCarQuery, GetCarDto>
{
    private readonly Serilog.ILogger _logger;
    private readonly ICarQueryStorage _carQueryStorage;

    public GetCarQueryHandler(
        Serilog.ILogger logger,
       ICarQueryStorage carQueryStorage)
    {
        _logger = logger;
        _carQueryStorage = carQueryStorage;
    }

    public async Task<GetCarDto> HandleAsync(GetCarQuery query, CancellationToken cancellationToken = default)
    {
        GetCarDto? car = await _carQueryStorage.GetCarAsync(query.CarId, cancellationToken);

        _logger.Information(
            messageTemplate: "Query {QueryName} handled by {ModuleName} module, retrieved car with ID: {CarId}",
            nameof(GetCarQuery),
            nameof(Cars),
            car?.Id);

        return car;
    }
}
