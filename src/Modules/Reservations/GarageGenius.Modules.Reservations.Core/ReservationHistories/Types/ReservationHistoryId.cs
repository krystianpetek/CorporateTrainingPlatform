namespace GarageGenius.Modules.Reservations.Core.ReservationHistories.Types;

internal sealed class ReservationHistoryId : IEquatable<ReservationHistoryId>
{
    public Guid Value { get; }

    public ReservationHistoryId(Guid value)
    {
        Value = value;
    }

    public static implicit operator ReservationHistoryId(Guid value)
    {
        return new ReservationHistoryId(value);
    }

    public static implicit operator Guid(ReservationHistoryId value)
    {
        return value.Value;
    }

    public bool Equals(ReservationHistoryId? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as ReservationHistoryId);
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
