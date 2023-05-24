using GarageGenius.Modules.Vehicles.Application.Queries.GetCustomerVehiclesQuery;
using GarageGenius.Modules.Vehicles.Application.Queries.GetFilteredVehicle;
using GarageGenius.Modules.Vehicles.Application.Queries.GetVehicleQuery;
using GarageGenius.Modules.Vehicles.Application.QueryStorage;
using GarageGenius.Modules.Vehicles.Core.Entities;
using GarageGenius.Modules.Vehicles.Core.Exceptions;
using GarageGenius.Modules.Vehicles.Core.Models;
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
        GetVehicleQueryDto? getVehicleQueryDto = await _vehiclesDbContext.Vehicles
            .AsNoTracking()
            .AsQueryable()
            .Where<Vehicle>(vehicle => vehicle.VehicleId == vehicleId)
            .Select<Vehicle, GetVehicleQueryDto>(vehicle => new GetVehicleQueryDto(vehicle.VehicleId, vehicle.Manufacturer, vehicle.Model, vehicle.Year, vehicle.LicensePlate, vehicle.Vin))
            .SingleOrDefaultAsync<GetVehicleQueryDto>(cancellationToken) ?? throw new VehicleNotFoundException(vehicleId);

        return getVehicleQueryDto;
    }
    public async Task<IReadOnlyList<GetCustomerVehiclesQueryDto>> GetCustomerVehiclesAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        IReadOnlyList<GetCustomerVehiclesQueryDto> getCustomerVehiclesQueryDto = await _vehiclesDbContext.Vehicles
            .AsNoTracking()
            .AsQueryable()
            .Where<Vehicle>(vehicle => vehicle.CustomerId == customerId)
            .Select<Vehicle, GetCustomerVehiclesQueryDto>(vehicle => new GetCustomerVehiclesQueryDto(vehicle.VehicleId, vehicle.Manufacturer, vehicle.Model, vehicle.Year, vehicle.LicensePlate, vehicle.Vin))
            .ToListAsync<GetCustomerVehiclesQueryDto>(cancellationToken);

        return getCustomerVehiclesQueryDto;
    }

    public async Task<GetVehicleFilterQueryDto> GetFilteredVehicleAsync(GetVehicleFilterParameters getVehicleFilterParameters, CancellationToken cancellationToken = default)
    {
        IQueryable<Vehicle> vehicleQuery = _vehiclesDbContext.Vehicles
        .AsNoTracking()
        .AsQueryable();

        if (getVehicleFilterParameters.Vin != default)
            vehicleQuery = vehicleQuery.Where<Vehicle>(vehicle => vehicle.Vin == getVehicleFilterParameters.Vin);

        if (getVehicleFilterParameters.LicensePlate != default)
            vehicleQuery = vehicleQuery.Where<Vehicle>(vehicle => vehicle.LicensePlate == getVehicleFilterParameters.LicensePlate);

        GetVehicleFilterQueryDto getVehicleFilterQueryDto = await vehicleQuery
            .Select<Vehicle, GetVehicleFilterQueryDto>(vehicle => new GetVehicleFilterQueryDto(vehicle.VehicleId, vehicle.Manufacturer, vehicle.Model, vehicle.Year, vehicle.LicensePlate, vehicle.Vin))
           .SingleOrDefaultAsync<GetVehicleFilterQueryDto>(cancellationToken) ?? throw new VehicleNotFoundException(getVehicleFilterParameters.Vin);

        return getVehicleFilterQueryDto;
    }
}
