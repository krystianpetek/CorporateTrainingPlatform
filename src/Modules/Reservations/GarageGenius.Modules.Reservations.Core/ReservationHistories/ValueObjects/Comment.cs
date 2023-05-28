namespace GarageGenius.Modules.Reservations.Core.ReservationHistories.ValueObjects;
internal class Comment : IEquatable<Comment>
{
	public string Value { get; }
	internal Comment(string value)
	{
		Value = value;
	}

	public bool Equals(Comment? other)
	{
		if (other is null) return false;
		if (ReferenceEquals(this, other)) return true;
		return Value == other.Value;
	}

	public override bool Equals(object obj)
	{
		return Equals(obj as Comment);
	}

	public override int GetHashCode()
	{
		return Value is not null ? Value.GetHashCode() : 0;
	}

	public override string ToString()
	{
		return Value.ToString();
	}

	public static implicit operator Comment(string value)
	{
		return new Comment(value);
	}

	public static implicit operator string(Comment value)
	{
		return value.Value;
	}
}

