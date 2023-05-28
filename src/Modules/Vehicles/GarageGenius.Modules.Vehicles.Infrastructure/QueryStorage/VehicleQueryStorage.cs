using GarageGenius.Modules.Vehicles.Application.Queries.GetCustomerVehicles;
using GarageGenius.Modules.Vehicles.Application.Queries.GetVehicle;
using GarageGenius.Modules.Vehicles.Application.Queries.SearchVehicles;
using GarageGenius.Modules.Vehicles.Application.QueryStorage;
using GarageGenius.Modules.Vehicles.Core.Entities;
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
			.SingleOrDefaultAsync<GetVehicleQueryDto>(cancellationToken);

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

	public async Task<IReadOnlyList<SearchVehiclesQueryDto>> SearchVehicleAsync(SearchVehiclesParameters searchVehiclesParameters, CancellationToken cancellationToken = default)
	{
		IQueryable<Vehicle> vehicleQuery = _vehiclesDbContext.Vehicles
			.AsNoTracking()
			.AsQueryable();

		if (searchVehiclesParameters.Vin != default)
			vehicleQuery = vehicleQuery.Where<Vehicle>(vehicle => vehicle.Vin == searchVehiclesParameters.Vin);

		if (searchVehiclesParameters.LicensePlate != default)
			vehicleQuery = vehicleQuery.Where<Vehicle>(vehicle => vehicle.LicensePlate == searchVehiclesParameters.LicensePlate);

		IReadOnlyList<SearchVehiclesQueryDto> searchVehiclesQueryDto = await vehicleQuery
			.Select<Vehicle, SearchVehiclesQueryDto>(vehicle => new SearchVehiclesQueryDto(vehicle.VehicleId, vehicle.Manufacturer, vehicle.Model, vehicle.Year, vehicle.LicensePlate, vehicle.Vin))
			.ToListAsync<SearchVehiclesQueryDto>(cancellationToken);

		return searchVehiclesQueryDto;
	}
}
