using GarageGenius.Shared.Abstractions.Events;

namespace GarageGenius.Modules.Reservations.Application.Events;
public record ReservationAddedEvent(Guid reservationId, string comment) : IEvent;
