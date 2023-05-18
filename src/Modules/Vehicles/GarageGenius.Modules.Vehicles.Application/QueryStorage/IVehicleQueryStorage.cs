using GarageGenius.Modules.Vehicles.Application.Dto;

namespace GarageGenius.Modules.Vehicles.Application.QueryStorage;
public interface IVehicleQueryStorage
{
    Task<GetVehicleDto?> GetVehicleAsync(Guid vehicleId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<GetVehicleDto>> GetCustomerVehiclesAsync(Guid customerId, CancellationToken cancellationToken = default);
}
