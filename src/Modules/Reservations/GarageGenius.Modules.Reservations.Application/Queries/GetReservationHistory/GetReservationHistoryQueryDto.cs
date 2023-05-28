namespace GarageGenius.Modules.Reservations.Application.Queries.GetReservationHistory;
public sealed record class GetReservationHistoryQueryDto(Guid ReservationId, Guid VehicleId, string ReservationState, string Comment);
