using GarageGenius.Shared.Abstractions.Events;

namespace GarageGenius.Modules.Reservations.Shared.Events;

public record ReservationAddedEvent(Guid UserId, Guid ReservationId, Guid VehicleId) : IEvent;