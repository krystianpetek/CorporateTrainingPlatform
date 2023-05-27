using GarageGenius.Shared.Abstractions.Events;

namespace GarageGenius.Modules.Reservations.Application.Events;
public record ReservationUpdated(Guid reservationId, string comment) : IEvent;
