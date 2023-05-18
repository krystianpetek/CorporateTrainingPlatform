using GarageGenius.Modules.Vehicles.Application.Queries.GetVehicleQuery;
using GarageGenius.Shared.Abstractions.Queries;

namespace GarageGenius.Modules.Vehicles.Application.Queries.GetCustomerVehiclesQuery;
public record GetCustomerVehiclesQuery : IQuery<IReadOnlyList<GetCustomerVehiclesQueryDto>>
{
    public Guid CustomerId { get; init; }
    public GetCustomerVehiclesQuery(Guid CustomerId)
    {
        this.CustomerId = CustomerId;
    }
}
