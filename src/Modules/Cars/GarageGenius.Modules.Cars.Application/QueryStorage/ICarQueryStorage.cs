using GarageGenius.Modules.Cars.Application.Dto;

namespace GarageGenius.Modules.Cars.Application.QueryStorage;
public interface ICarQueryStorage
{
    Task<GetCarDto?> GetCarAsync(Guid carId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<GetCarDto>> GetCustomerCarsAsync(Guid customerId, CancellationToken cancellationToken = default);
}
