using GarageGenius.Modules.Cars.Application.Dto;
using GarageGenius.Shared.Abstractions.Queries;

namespace GarageGenius.Modules.Cars.Application.Queries.GetCustomerCarsQuery;
public record GetCustomerCarsQuery : IQuery<IReadOnlyList<GetCarDto>>
{
    public Guid CustomerId { get; init; }
    public GetCustomerCarsQuery(Guid CustomerId)
    {
        this.CustomerId = CustomerId;
    }
}
