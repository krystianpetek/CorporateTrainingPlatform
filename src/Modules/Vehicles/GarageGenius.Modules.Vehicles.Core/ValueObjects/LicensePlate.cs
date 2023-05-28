using GarageGenius.Modules.Vehicles.Core.Exceptions;

namespace GarageGenius.Modules.Vehicles.Core.ValueObjects;
internal sealed class LicensePlate : IEquatable<LicensePlate>
{
	public string Value { get; }

	public LicensePlate(string value)
	{
		if (string.IsNullOrWhiteSpace(value) || value.Length is < 5 or > 8)
			throw new InvalidLicensePlateException(value);

		Value = value;
	}

	public static implicit operator string(LicensePlate value)
	{
		return value?.Value;
	}

	public static implicit operator LicensePlate(string value)
	{
		if (value is null) return null;
		return new LicensePlate(value);
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
		return obj.GetType() == GetType() && Equals((LicensePlate?)obj);
	}
}
