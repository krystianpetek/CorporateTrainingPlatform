using GarageGenius.Modules.Reservations.Application.Queries.GetReservation;

namespace GarageGenius.Modules.Vehicles.Application.QueryStorage;
public interface IReservationQueryStorage
{
    public Task<GetReservationQueryDto?> GetReservationAsync(Guid reservationId, CancellationToken cancellationToken = default);
    //Task<IReadOnlyList<GetCustomerVehiclesQueryDto>> GetCustomerVehiclesAsync(Guid customerId, CancellationToken cancellationToken = default);
    //Task<IReadOnlyList<SearchVehiclesQueryDto>> SearchVehicleAsync(SearchVehiclesParameters searchVehiclesParameters, CancellationToken cancellationToken = default);
}
