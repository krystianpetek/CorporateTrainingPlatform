using GarageGenius.Modules.Reservations.Core.ReservationHistories.Types;
using GarageGenius.Modules.Reservations.Core.ReservationHistories.ValueObjects;
using GarageGenius.Modules.Reservations.Core.Reservations.Types;
using GarageGenius.Modules.Reservations.Core.Reservations.ValueObjects;
using GarageGenius.Shared.Abstractions.Common;

namespace GarageGenius.Modules.Reservations.Core.ReservationHistories.Entities;
internal sealed class ReservationHistory : AuditableEntity
{
    internal ReservationHistoryId ReservationHistoryId { get; private set; }
    internal ReservationId ReservationId { get; private set; }
    internal ReservationState ReservationState { get; private set; }
    internal Comment Comment { get; private set; }

    private ReservationHistory() { }

    public ReservationHistory(ReservationId reservationId, ReservationState reservationState, Comment comment)
    {
        ReservationHistoryId = Guid.NewGuid();
        ReservationId = reservationId;
        ReservationState = reservationState;
        Comment = comment;
    }
}
