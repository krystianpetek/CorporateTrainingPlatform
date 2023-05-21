using GarageGenius.Modules.Vehicles.Application.Queries.GetCustomerVehiclesQuery;
using GarageGenius.Modules.Vehicles.Application.Queries.GetVehicleQuery;
using GarageGenius.Modules.Vehicles.Application.QueryStorage;
using GarageGenius.Modules.Vehicles.Core.Exceptions;
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

    public async Task<GetVehicleQueryDto?> GetVehicleAsync(Guid vehicleId, CancellationToken cancellationToken = default)
    {
        GetVehicleQueryDto? getVehicleDto = await _vehiclesDbContext.Vehicles
            .AsNoTracking()
            .AsQueryable()
            .Where(x => x.VehicleId == vehicleId)
            .Select(x => new GetVehicleQueryDto(x.VehicleId, x.Manufacturer, x.Model, x.Year, x.LicensePlate))
            .SingleOrDefaultAsync(cancellationToken) ?? throw new VehicleNotFoundException(vehicleId);

        return getVehicleDto;
    }
    public async Task<IReadOnlyList<GetCustomerVehiclesQueryDto>> GetCustomerVehiclesAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        IReadOnlyList<GetCustomerVehiclesQueryDto> customerVehicles = await _vehiclesDbContext.Vehicles
            .AsNoTracking()
            .AsQueryable()
            .Where(vehicle => vehicle.CustomerId == customerId)
            .Select(x => new GetCustomerVehiclesQueryDto(x.VehicleId, x.Manufacturer, x.Model, x.Year, x.LicensePlate))
            .ToListAsync(cancellationToken);

        return customerVehicles;
    }
}
