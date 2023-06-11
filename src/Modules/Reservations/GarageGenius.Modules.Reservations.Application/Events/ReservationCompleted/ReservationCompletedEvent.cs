using GarageGenius.Shared.Abstractions.Events;

namespace GarageGenius.Modules.Reservations.Application.Events.ReservationCompleted;
public record ReservationCompletedEvent(Guid reservationId) : IEvent;
