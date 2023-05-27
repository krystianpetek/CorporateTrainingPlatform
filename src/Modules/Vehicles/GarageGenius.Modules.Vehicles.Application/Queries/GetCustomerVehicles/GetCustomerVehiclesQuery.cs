using GarageGenius.Shared.Abstractions.Queries;

namespace GarageGenius.Modules.Vehicles.Application.Queries.GetCustomerVehicles;
public record GetCustomerVehiclesQuery : IQuery<IReadOnlyList<GetCustomerVehiclesQueryDto>>
{
    public Guid CustomerId { get; init; }
    public GetCustomerVehiclesQuery(Guid CustomerId)
    {
        this.CustomerId = CustomerId;
    }
}
