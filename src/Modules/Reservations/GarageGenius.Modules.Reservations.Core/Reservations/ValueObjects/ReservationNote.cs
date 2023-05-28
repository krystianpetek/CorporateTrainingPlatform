namespace GarageGenius.Modules.Reservations.Core.Reservations.ValueObjects;
internal class ReservationNote : IEquatable<ReservationNote>
{
	public string Value { get; }
	internal ReservationNote(string value)
	{
		Value = value;
	}

	public bool Equals(ReservationNote? other)
	{
		if (other is null) return false;
		if (ReferenceEquals(this, other)) return true;
		return Value == other.Value;
	}

	public override bool Equals(object obj)
	{
		return Equals(obj as ReservationNote);
	}

	public override int GetHashCode()
	{
		return Value is not null ? Value.GetHashCode() : 0;
	}

	public override string ToString()
	{
		return Value.ToString();
	}

	public static implicit operator ReservationNote(string value)
	{
		return new ReservationNote(value);
	}

	public static implicit operator string(ReservationNote value)
	{
		return value.Value;
	}
}
