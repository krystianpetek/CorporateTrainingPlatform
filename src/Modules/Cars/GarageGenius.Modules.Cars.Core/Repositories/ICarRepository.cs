using GarageGenius.Modules.Cars.Core.Entities;

namespace GarageGenius.Modules.Cars.Core.Repositories;
internal interface ICarRepository
{
    Task AddCarAsync(Car car, CancellationToken cancellationToken = default);
    Task<Car> GetCarAsync(Guid carId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Car>> GetCustomerCarsAsync(Guid customerId, CancellationToken cancellationToken = default);
    Task<Car> UpdateCarAsync(Car car, CancellationToken cancellationToken = default);
    Task DeleteCarAsync(Guid carId, CancellationToken cancellationToken = default);
}
