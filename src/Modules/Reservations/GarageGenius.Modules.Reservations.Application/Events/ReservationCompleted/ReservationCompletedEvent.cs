using GarageGenius.Shared.Abstractions.Events;

namespace GarageGenius.Modules.Reservations.Application.Events.ReservationUpdated;
public record ReservationCompletedEvent(Guid reservationId) : IEvent;
