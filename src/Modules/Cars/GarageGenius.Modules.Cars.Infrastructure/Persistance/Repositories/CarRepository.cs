using GarageGenius.Modules.Cars.Core.Entities;
using GarageGenius.Modules.Cars.Core.Repositories;
using GarageGenius.Modules.Cars.Infrastructure.Persistance.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace GarageGenius.Modules.Cars.Infrastructure.Persistance.Repositories;
internal class CarRepository : ICarRepository
{
    private readonly CarsDbContext _carsDbContext;

    public CarRepository(CarsDbContext carsDbContext)
    {
        _carsDbContext = carsDbContext;
    }

    public async Task AddCarAsync(Car car, CancellationToken cancellationToken = default)
    {
        await _carsDbContext.AddAsync(car, cancellationToken);
        await _carsDbContext.SaveChangesAsync(cancellationToken);
    }

    public Task DeleteCarAsync(Guid carId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }


    public Task<Car> UpdateCarAsync(Car car, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    [Obsolete($"Moved responsibility for fetching data from ICarRepository to ICarQueryStorage")]
    public async Task<Car?> GetCarAsync(Guid carId, CancellationToken cancellationToken = default)
    {
        Car? car = await _carsDbContext.Cars.FirstOrDefaultAsync(car => car.Id == carId, cancellationToken);
        return car;
    }

    [Obsolete($"Moved responsibility for fetching data from ICarRepository to ICarQueryStorage")]
    public async Task<IReadOnlyList<Car>> GetCustomerCarsAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        IReadOnlyList<Car> customerCars = await _carsDbContext.Cars.Where(car => car.CustomerId == customerId).ToListAsync(cancellationToken);
        return customerCars;
    }
}
