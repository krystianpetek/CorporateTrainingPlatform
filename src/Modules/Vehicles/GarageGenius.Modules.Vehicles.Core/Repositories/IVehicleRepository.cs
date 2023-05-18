using GarageGenius.Modules.Vehicles.Core.Entities;

namespace GarageGenius.Modules.Vehicles.Core.Repositories;
internal interface IVehicleRepository
{
    Task AddVehicleAsync(Vehicle vehicle, CancellationToken cancellationToken = default);
    Task<Vehicle> UpdateVehicleAsync(Vehicle vehicle, CancellationToken cancellationToken = default);
    Task DeleteVehicleAsync(Guid vehicleId, CancellationToken cancellationToken = default);

    [Obsolete($"Moved responsibility for fetching data from IVehicleRepository to IVehicleQueryStorage")]
    Task<Vehicle?> GetVehicleAsync(Guid vehicleId, CancellationToken cancellationToken = default);
    [Obsolete($"Moved responsibility for fetching data from IVehicleRepository to IVehicleQueryStorage")]
    Task<IReadOnlyList<Vehicle>> GetCustomerVehiclesAsync(Guid customerId, CancellationToken cancellationToken = default);
}
