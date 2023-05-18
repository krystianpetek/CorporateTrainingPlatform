using GarageGenius.Modules.Vehicles.Core.Exceptions;

namespace GarageGenius.Modules.Vehicles.Core.ValueObjects;
internal sealed class Year : IEquatable<Year>
{
    private readonly int _currentYear = DateTime.UtcNow.Year;
    public int Value { get; }

    public Year(int year)
    {
        if (year is < 1900 || year > _currentYear)
            throw new InvalidProductionYearException(year);

        Value = year;
    }

    public static implicit operator int?(Year value)
    {
        return value?.Value;
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
        return obj.GetType() == GetType() && Equals((Year?)obj);
    }
}
