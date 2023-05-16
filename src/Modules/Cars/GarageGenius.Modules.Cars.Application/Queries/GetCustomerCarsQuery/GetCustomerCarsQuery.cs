using GarageGenius.Modules.Cars.Application.Dto;
using GarageGenius.Shared.Abstractions.Queries;

namespace GarageGenius.Modules.Cars.Application.Queries.GetCarQuery;
public record GetCustomerCarsQuery : IQuery<IReadOnlyList<GetCarDto>>
{
    public Guid CustomerId { get; init; }
}
