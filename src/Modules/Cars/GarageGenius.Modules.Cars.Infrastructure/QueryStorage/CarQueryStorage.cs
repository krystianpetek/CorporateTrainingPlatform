using GarageGenius.Modules.Cars.Application.Dto;
using GarageGenius.Modules.Cars.Application.QueryStorage;
using GarageGenius.Modules.Cars.Infrastructure.Persistance.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace GarageGenius.Modules.Cars.Infrastructure.QueryStorage;
internal class CarQueryStorage : ICarQueryStorage
{
    private readonly CarsDbContext _carsDbContext;

    public CarQueryStorage(CarsDbContext carsDbContext)
    {
        _carsDbContext = carsDbContext;
    }

    public async Task<GetCarDto?> GetCarAsync(Guid carId, CancellationToken cancellationToken)
    {
        GetCarDto? car = await _carsDbContext.Cars
            .AsNoTracking()
            .AsQueryable()
            .Where(x => x.Id == carId)
            .Select(x => new GetCarDto(x.Id, x.Manufacturer, x.Model, x.Year, x.LicensePlate))
            .FirstOrDefaultAsync(cancellationToken);

        return car;
    }
    public async Task<IReadOnlyList<GetCarDto>> GetCustomerCarsAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        IReadOnlyList<GetCarDto> customerCars = await _carsDbContext.Cars
            .AsNoTracking()
            .AsQueryable()
            .Where(car => car.CustomerId == customerId)
            .Select(x => new GetCarDto(x.Id, x.Manufacturer, x.Model, x.Year, x.LicensePlate))
            .ToListAsync(cancellationToken);

        return customerCars;
    }
}
