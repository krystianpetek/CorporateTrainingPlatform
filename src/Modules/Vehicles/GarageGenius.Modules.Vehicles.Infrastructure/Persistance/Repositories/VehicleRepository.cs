using GarageGenius.Modules.Vehicles.Core.Entities;
using GarageGenius.Modules.Vehicles.Core.Repositories;
using GarageGenius.Modules.Vehicles.Infrastructure.Persistance.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace GarageGenius.Modules.Vehicles.Infrastructure.Persistance.Repositories;
internal class VehicleRepository : IVehicleRepository
{
    private readonly VehiclesDbContext _vehiclesDbContext;

    public VehicleRepository(VehiclesDbContext vehiclesDbContext)
    {
        _vehiclesDbContext = vehiclesDbContext;
    }

    public async Task AddVehicleAsync(Vehicle vehicle, CancellationToken cancellationToken = default)
    {
        await _vehiclesDbContext.AddAsync(vehicle, cancellationToken);
        await _vehiclesDbContext.SaveChangesAsync(cancellationToken);
    }

    public Task DeleteVehicleAsync(Guid vehicleId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }


    public Task<Vehicle> UpdateVehicleAsync(Vehicle vehicle, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    [Obsolete($"Moved responsibility for fetching data from IVehicleRepository to IVehicleQueryStorage")]
    public async Task<Vehicle?> GetVehicleAsync(Guid vehicleId, CancellationToken cancellationToken = default)
    {
        Vehicle? vehicle = await _vehiclesDbContext.Vehicles.FirstOrDefaultAsync(vehicle => vehicle.Id == vehicleId, cancellationToken);
        return vehicle;
    }

    [Obsolete($"Moved responsibility for fetching data from IVehicleRepository to IVehicleQueryStorage")]
    public async Task<IReadOnlyList<Vehicle>> GetCustomerVehiclesAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        IReadOnlyList<Vehicle> customerVehicles = await _vehiclesDbContext.Vehicles.Where(vehicle => vehicle.CustomerId == customerId).ToListAsync(cancellationToken);
        return customerVehicles;
    }
}
