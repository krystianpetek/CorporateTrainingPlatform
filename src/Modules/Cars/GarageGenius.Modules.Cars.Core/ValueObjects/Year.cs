using GarageGenius.Modules.Cars.Core.Exceptions;

namespace GarageGenius.Modules.Cars.Core.ValueObjects;
internal sealed class Year : IEquatable<Year>
{
    private readonly int _currentYear = DateTime.UtcNow.Year;
    public int? Value { get; }

    public Year(int? value)
    {
        if(value == default(int) || value == null)
        {
            Value = null;
            return;
        }
        if (value is < 1900 || value > _currentYear)
        {
            throw new InvalidProductionYearException(value);
        }

        Value = value;
    }

    public static implicit operator int?(Year value)
    {
        return value.Value;
    }

    public static implicit operator Year(int value)
    {
        if (value == default) return null;
        return new Year(value);
    }

    public bool Equals(Year? other)
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
        return obj.GetType() == this.GetType() && Equals((Year?)obj);
    }
}
