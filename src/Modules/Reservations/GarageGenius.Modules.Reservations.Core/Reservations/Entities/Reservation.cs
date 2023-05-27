using GarageGenius.Modules.Reservations.Core.ReservationHistories.Entities;
using GarageGenius.Modules.Reservations.Core.Reservations.Exceptions;
using GarageGenius.Modules.Reservations.Core.Reservations.Types;
using GarageGenius.Modules.Reservations.Core.Reservations.ValueObjects;
using GarageGenius.Shared.Abstractions.Common;

namespace GarageGenius.Modules.Reservations.Core.Reservations.Entities;
internal sealed class Reservation : AuditableEntity
{
    private readonly IList<ReservationHistory> _reservationHistories = new List<ReservationHistory>();

    internal ReservationId ReservationId { get; private set; }
    internal VehicleId VehicleId { get; private set; }
    public ReservationState ReservationState { get; private set; }
    public ReservationDate ReservationDate { get; private set; }
    public ReservationNote ReservationNote { get; private set; }
    public bool ReservationDeleted { get; private set; }

    public IEnumerable<ReservationHistory> ReservationHistories
    {
        get => _reservationHistories;
        private set => new List<ReservationHistory>(value);
    }

    private Reservation() { }

    public Reservation(VehicleId vehicleId, ReservationNote reservationNote, ReservationDate reservationDate)
    {
        ReservationId = Guid.NewGuid();
        VehicleId = vehicleId;
        ReservationNote = reservationNote;
        ReservationDate = reservationDate;
        ReservationState = ReservationState.Pending;
        ReservationDeleted = true;
    }

    internal void ChangeStateAccepted() { ReservationState = ReservationState.Accepted; }
    internal void ChangeStateRejected() { ReservationState = ReservationState.Rejected; }
    internal void ChangeStateCanceled() { ReservationState = ReservationState.Canceled; }
    internal void ChangeStateCompleted() { ReservationState = ReservationState.Completed; }
    internal void ChangeStateWaitingForCustomer() { ReservationState = ReservationState.WaitingForCustomer; }

    internal void ReservationDeactivate() { ReservationDeleted = true; }

    internal void ChangeState(string reservationState)
    {
        if (this.ReservationState == ReservationState.Completed ||
            this.ReservationState == ReservationState.Canceled)
        {
            throw new UnableChangeReservationStateException(this.ReservationId);
        }

        switch (reservationState)
        {
            case "Accepted":
                ChangeStateAccepted();
                break;
            case "Rejected":
                ChangeStateRejected();
                break;
            case "Canceled":
                ChangeStateCanceled();
                break;
            case "Completed":
                ChangeStateCompleted();
                break;
            case "WaitingForCustomer":
                ChangeStateWaitingForCustomer();
                break;
            default:
                throw new InvalidReservationStateException(reservationState);
        }
    }
}
