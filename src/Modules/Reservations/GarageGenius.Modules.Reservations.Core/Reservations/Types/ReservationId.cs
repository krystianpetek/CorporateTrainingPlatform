namespace GarageGenius.Modules.Reservations.Core.Reservations.Types;

internal sealed class ReservationId : IEquatable<ReservationId>
{
	public Guid Value { get; }

	public ReservationId(Guid value)
	{
		Value = value;
	}

	public static implicit operator ReservationId(Guid value)
	{
		return new ReservationId(value);
	}

	public static implicit operator Guid(ReservationId value)
	{
		return value.Value;
	}

	public bool Equals(ReservationId? other)
	{
		if (other is null) return false;
		if (ReferenceEquals(this, other)) return true;
		return Value == other.Value;
	}

	public override bool Equals(object obj)
	{
		return Equals(obj as ReservationId);
	}

	public override int GetHashCode()
	{
		return Value.GetHashCode();
	}

	public override string ToString()
	{
		return Value.ToString();
	}
}
