namespace GarageGenius.Modules.Vehicles.Core.Types;

internal sealed class CustomerId : IEquatable<CustomerId>
{
    public Guid Value { get; }

    public CustomerId(Guid value)
    {
        Value = value;
    }

    public static implicit operator CustomerId(Guid value)
    {
        return new CustomerId(value);
    }

    public static implicit operator Guid(CustomerId value)
    {
        return value.Value;
    }

    public bool Equals(CustomerId? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as CustomerId);
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
