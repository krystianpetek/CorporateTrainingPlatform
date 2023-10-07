namespace GarageGenius.Modules.Reservations.Application.Queries.GetReservation;
public sealed record class GetReservationQueryDto(Guid ReservationId, Guid VehicleId, string ReservationState, DateTime ReservationDate, string comment);
