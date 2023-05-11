using GarageGenius.Modules.Cars.Core.Exceptions;

namespace GarageGenius.Modules.Cars.Core.ValueObjects;
internal sealed class Manufacturer : IEquatable<Manufacturer>
{
    public string Value { get; }

    public Manufacturer(string manufacturer)
    {
        if (string.IsNullOrWhiteSpace(manufacturer) || manufacturer.Length is > 100 or < 3)
        {
            throw new InvalidManufacturerException(manufacturer);
        }

        Value = manufacturer;
    }

    public static implicit operator string(Manufacturer manufacturer)
    {
        return manufacturer.Value;
    }

    public static implicit operator Manufacturer(string manufacturer)
    {
        if (manufacturer is null) return null;
        return new Manufacturer(manufacturer);
    }

    public bool Equals(Manufacturer? other)
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
        return obj.GetType() == this.GetType() && Equals((Manufacturer?)obj);
    }
}
