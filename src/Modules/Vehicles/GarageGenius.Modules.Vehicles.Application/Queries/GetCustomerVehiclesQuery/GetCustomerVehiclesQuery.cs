using GarageGenius.Modules.Vehicles.Application.Dto;
using GarageGenius.Shared.Abstractions.Queries;

namespace GarageGenius.Modules.Vehicles.Application.Queries.GetCustomerVehiclesQuery;
public record GetCustomerVehiclesQuery : IQuery<IReadOnlyList<GetVehicleDto>>
{
    public Guid CustomerId { get; init; }
    public GetCustomerVehiclesQuery(Guid CustomerId)
    {
        this.CustomerId = CustomerId;
    }
}
