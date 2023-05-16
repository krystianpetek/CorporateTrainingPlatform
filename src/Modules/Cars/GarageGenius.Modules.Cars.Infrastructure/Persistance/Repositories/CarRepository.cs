using GarageGenius.Modules.Cars.Core.Entities;
using GarageGenius.Modules.Cars.Core.Repositories;
using GarageGenius.Modules.Cars.Infrastructure.Persistance.DbContexts;

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

    public Task<Car> GetCarAsync(Guid carId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Car>> GetCustomerCarsAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Car> UpdateCarAsync(Car car, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
