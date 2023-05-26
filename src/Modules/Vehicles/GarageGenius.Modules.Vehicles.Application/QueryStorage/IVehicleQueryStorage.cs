using GarageGenius.Modules.Vehicles.Application.Queries.GetCustomerVehiclesQuery;
using GarageGenius.Modules.Vehicles.Application.Queries.GetVehicleQuery;
using GarageGenius.Modules.Vehicles.Application.Queries.SearchVehiclesQuery;
using GarageGenius.Modules.Vehicles.Core.Models;

namespace GarageGenius.Modules.Vehicles.Application.QueryStorage;
public interface IVehicleQueryStorage
{
    Task<GetVehicleQueryDto?> GetVehicleAsync(Guid vehicleId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<GetCustomerVehiclesQueryDto>> GetCustomerVehiclesAsync(Guid customerId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<SearchVehiclesQueryDto>> SearchVehicleAsync(SearchVehiclesParameters searchVehiclesParameters, CancellationToken cancellationToken = default);
}
