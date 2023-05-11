using GarageGenius.Modules.Cars.Core.Exceptions;

namespace GarageGenius.Modules.Cars.Core.ValueObjects;
internal sealed class LicensePlate : IEquatable<LicensePlate>
{
    public string Value { get; }

    public LicensePlate(string licensePlate)
    {
        if (string.IsNullOrWhiteSpace(licensePlate) || licensePlate.Length is < 5 or > 8)
        {
            throw new InvalidLicensePlateException(licensePlate);
        }

        Value = licensePlate;
    }

    public static implicit operator string(LicensePlate licensePlate)
    {
        return licensePlate.Value;
    }

    public static implicit operator LicensePlate(string licensePlate)
    {
        if (licensePlate is null) return null;
        return new LicensePlate(licensePlate);
    }

    public bool Equals(LicensePlate? other)
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
        return obj.GetType() == this.GetType() && Equals((LicensePlate?)obj);
    }
}
