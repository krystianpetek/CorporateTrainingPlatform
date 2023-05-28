using GarageGenius.Modules.Vehicles.Core.Exceptions;

namespace GarageGenius.Modules.Vehicles.Core.ValueObjects;
internal sealed class Manufacturer : IEquatable<Manufacturer>
{
	public string Value { get; }

	public Manufacturer(string value)
	{
		if (string.IsNullOrWhiteSpace(value) || value.Length is > 100 or < 3)
			throw new InvalidManufacturerException(value);

		Value = value;
	}

	public static implicit operator string(Manufacturer value)
	{
		return value.Value;
	}

	public static implicit operator Manufacturer(string value)
	{
		if (value is null) return null;
		return new Manufacturer(value);
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
		return obj.GetType() == GetType() && Equals((Manufacturer?)obj);
	}
}
