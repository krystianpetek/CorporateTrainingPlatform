using GarageGenius.Modules.Cars.Core.Exceptions;

namespace GarageGenius.Modules.Cars.Core.ValueObjects;
internal sealed class Year : IEquatable<Year>
{
    private int _currentYear = DateTime.UtcNow.Year;
    public int Value { get; }

    public Year(int year)
    {
        if (year is < 1900 || year > _currentYear)
        {
            throw new InvalidProductionYearException(year);
        }

        Value = year;
    }

    public static implicit operator int(Year year)
    {
        return year.Value;
    }

    public static implicit operator Year(int year)
    {
        if (year == default) return null;
        return new Year(year);
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
