using GarageGenius.Modules.Vehicles.Application.Dto;
using GarageGenius.Modules.Vehicles.Application.QueryStorage;
using GarageGenius.Modules.Vehicles.Infrastructure.Persistance.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace GarageGenius.Modules.Vehicles.Infrastructure.QueryStorage;
internal class VehicleQueryStorage : IVehicleQueryStorage
{
    private readonly VehiclesDbContext _vehiclesDbContext;

    public VehicleQueryStorage(VehiclesDbContext vehiclesDbContext)
    {
        _vehiclesDbContext = vehiclesDbContext;
    }

    public async Task<GetVehicleDto?> GetVehicleAsync(Guid vehicleId, CancellationToken cancellationToken)
    {
        GetVehicleDto? getVehicleDto = await _vehiclesDbContext.Vehicles
            .AsNoTracking()
            .AsQueryable()
            .Where(x => x.Id == vehicleId)
            .Select(x => new GetVehicleDto(x.Id, x.Manufacturer, x.Model, x.Year, x.LicensePlate))
            .FirstOrDefaultAsync(cancellationToken);

        return getVehicleDto;
    }
    public async Task<IReadOnlyList<GetVehicleDto>> GetCustomerVehiclesAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        IReadOnlyList<GetVehicleDto> customerVehicles = await _vehiclesDbContext.Vehicles
            .AsNoTracking()
            .AsQueryable()
            .Where(vehicle => vehicle.CustomerId == customerId)
            .Select(x => new GetVehicleDto(x.Id, x.Manufacturer, x.Model, x.Year, x.LicensePlate))
            .ToListAsync(cancellationToken);

        return customerVehicles;
    }
}
