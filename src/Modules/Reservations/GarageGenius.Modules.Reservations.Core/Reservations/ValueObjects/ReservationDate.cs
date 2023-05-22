namespace GarageGenius.Modules.Reservations.Core.Reservations.ValueObjects;
internal sealed class ReservationDate : IEquatable<ReservationDate>
{
    public DateTime Value { get; }

    public ReservationDate(DateTime dateTime)
    {
        Value = dateTime;
    }

    public static implicit operator DateTime?(ReservationDate value)
    {
        return value?.Value;
    }

    public static implicit operator ReservationDate(DateTime value)
    {
        if (value == default) return null;
        return new ReservationDate(value);
    }

    public bool Equals(ReservationDate? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    public override string ToString()
    {
        return $"{Value}";
    }

    public override int GetHashCode()
    {
        return Value != default ? Value.GetHashCode() : 0;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((ReservationDate?)obj);
    }
}

{
}
