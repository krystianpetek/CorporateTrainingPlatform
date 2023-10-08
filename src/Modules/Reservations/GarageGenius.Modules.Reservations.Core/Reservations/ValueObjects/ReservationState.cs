using System.Collections.ObjectModel;

namespace GarageGenius.Modules.Reservations.Core.Reservations.ValueObjects;

internal sealed class ReservationState : IEquatable<ReservationState>
{
	private const string PendingState = "Pending";
	private const string CanceledState = "Canceled";
	private const string CompletedState = "Completed";
	private const string WaitingForCustomerState = "WaitingForCustomer";
	private const string RejectedState = "Rejected";
	private const string AcceptedState = "Accepted";
	private const string WorkInProgressState = "WorkInProgress";

	internal static ReservationState Pending => new ReservationState(PendingState);
	internal static ReservationState WaitingForCustomer => new ReservationState(WaitingForCustomerState);
	internal static ReservationState Accepted => new ReservationState(AcceptedState);
	internal static ReservationState Rejected => new ReservationState(RejectedState);
	internal static ReservationState Canceled => new ReservationState(CanceledState);

	internal static ReservationState Completed => new ReservationState(CompletedState);

	internal static ReservationState WorkInProgress => new ReservationState(WorkInProgressState);

	public static IReadOnlyCollection<string> GetAvailableStates => new List<string>
	{
		PendingState,
		CanceledState,
		CompletedState,
		WaitingForCustomerState,
		RejectedState,
		AcceptedState,
		WorkInProgressState
	};

	public string Value { get; }
	internal ReservationState(string value)
	{
		Value = value;
	}

	public bool Equals(ReservationState? other)
	{
		if (other is null) return false;
		if (ReferenceEquals(this, other)) return true;
		return Value == other.Value;
	}

	public override bool Equals(object obj)
	{
		return Equals(obj as ReservationState);
	}

	public override int GetHashCode()
	{
		return Value is not null ? Value.GetHashCode() : 0;
	}

	public override string ToString()
	{
		return Value.ToString();
	}

	public static implicit operator ReservationState(string value)
	{
		return new ReservationState(value);
	}

	public static implicit operator string(ReservationState value)
	{
		return value.Value;
	}
}