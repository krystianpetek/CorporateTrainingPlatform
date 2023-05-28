namespace GarageGenius.Modules.Reservations.Application.Queries.GetCustomerReservations;
public sealed record class GetCustomerReservationsQueryDto(Guid CustomerId, IReadOnlyList<CustomerReservationsDto> CustomerReservationsDto);

public sealed record class CustomerReservationsDto(Guid ReservationId, string ReservationState, DateTime ReservationDate, string Comment);
