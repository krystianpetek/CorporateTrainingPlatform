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
	internal CustomerId CustomerId { get; private set; }
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

	public Reservation(VehicleId vehicleId, CustomerId customerId, ReservationNote reservationNote, ReservationDate reservationDate)
	{
		ReservationId = Guid.NewGuid();
		VehicleId = vehicleId;
		CustomerId = customerId;
		ReservationNote = reservationNote;
		ReservationDate = reservationDate;
		ReservationState = ReservationState.Pending;
		ReservationDeleted = false;
	}

	private void ChangeStateAccepted() { ReservationState = ReservationState.Accepted; }
	private void ChangeStateRejected() { ReservationState = ReservationState.Rejected; }
	private void ChangeStateCanceled() { ReservationState = ReservationState.Canceled; }
	private void ChangeStateCompleted() { ReservationState = ReservationState.Completed; }
	private void ChangeStateWaitingForCustomer() { ReservationState = ReservationState.WaitingForCustomer; }
	private void ChangeStateWorkInProgress() { ReservationState = ReservationState.WorkInProgress; }
	private void ChangeStatePending() { ReservationState = ReservationState.Pending; }

	internal void ReservationDeactivate() { ReservationDeleted = true; }

	internal void ChangeState(string reservationState)
	{
		if (ReservationState.Equals(ReservationState.Completed) ||
			ReservationState.Equals(ReservationState.Canceled))
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
			case "WorkInProgress":
				ChangeStateWorkInProgress();
				break;
			case "Pending":
				ChangeStatePending();
				break;
			default:
				throw new InvalidReservationStateException(reservationState);
		}
	}

	internal void ChangeClientReservationNote(string reservationNote)
	{
		ReservationNote = reservationNote;
	}

	internal void ChangeReservationDate(ReservationDate reservationDate)
	{
		ReservationDate = reservationDate;
	}
}
