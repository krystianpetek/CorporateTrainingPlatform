using GarageGenius.Modules.Cars.Core.Exceptions;

namespace GarageGenius.Modules.Cars.Core.ValueObjects;
internal sealed class Vin : IEquatable<Vin>
{
    public string Value { get; }

    public Vin(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            Value = string.Empty;
            return;
        }

        if (value.Length is not 17 or 10)
        {
            throw new InvalidVinException(value);
        }

        Value = value;
    }

    public static implicit operator string(Vin value)
    {
        return value.Value;
    }

    public static implicit operator Vin(string value)
    {
        if (value is null) return null;
        return new Vin(value);
    }

    public bool Equals(Vin? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    public override string ToString()
    {
        return Value;
    }

    public override int GetHashCode()
    {
        return Value is not null ? Value.GetHashCode() : 0;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == this.GetType() && Equals((Vin?)obj);
    }
}
