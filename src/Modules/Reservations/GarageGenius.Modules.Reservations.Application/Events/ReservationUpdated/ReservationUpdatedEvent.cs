using GarageGenius.Shared.Abstractions.Events;

namespace GarageGenius.Modules.Reservations.Application.Events.ReservationUpdated;
public record ReservationUpdatedEvent(Guid reservationId, string comment) : IEvent;
