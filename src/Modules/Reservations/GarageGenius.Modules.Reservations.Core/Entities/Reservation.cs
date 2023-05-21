using GarageGenius.Modules.Reservations.Core.Types;
using GarageGenius.Modules.Reservations.Core.ValueObjects;
using GarageGenius.Modules.Users.Core.ValueObjects;
using GarageGenius.Shared.Abstractions.Common;

namespace GarageGenius.Modules.Reservations.Core.Entities;
internal sealed class Reservation : AuditableEntity
{
    internal ReservationId ReservationId { get; private set; }
    internal VehicleId VehicleId { get; private set; }
    public ReservationState ReservationState { get; private set; }
    public ProblemDescription ProblemDescription { get; private set; } //  todo
    public Date Date { get; private set; } // todo
}
