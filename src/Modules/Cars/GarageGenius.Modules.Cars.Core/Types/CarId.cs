namespace GarageGenius.Modules.Cars.Core.Types;

internal sealed class CarId : IEquatable<CarId>
{
    public Guid Value { get; }

    public CarId(Guid value)
    {
        Value = value;
    }

    public static implicit operator CarId(Guid value)
    {
        return new CarId(value);
    }

    public static implicit operator Guid(CarId userId)
    {
        return userId.Value;
    }

    public bool Equals(CarId? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as CarId);
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
