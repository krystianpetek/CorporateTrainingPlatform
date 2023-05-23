using GarageGenius.Shared.Abstractions.Events;

namespace GarageGenius.Modules.Reservations.Application.Events;
public record ReservationAdded(Guid reservationId, string comment) : IEvent;
