using GarageGenius.Shared.Abstractions.Events;

namespace GarageGenius.Modules.Reservations.Application.Events.ReservationAdded;
public record ReservationAddedEvent(Guid reservationId, string comment) : IEvent;
