using GarageGenius.Modules.Vehicles.Core.Exceptions;

namespace GarageGenius.Modules.Vehicles.Core.ValueObjects;
internal sealed class Model : IEquatable<Model>
{
	public string Value { get; }

	public Model(string value)
	{
		if (string.IsNullOrWhiteSpace(value) || value.Length is > 100 or < 3)
			throw new InvalidModelException(value);

		Value = value;
	}

	public static implicit operator string(Model value)
	{
		return value.Value;
	}

	public static implicit operator Model(string value)
	{
		if (value is null) return null;
		return new Model(value);
	}

	public bool Equals(Model? other)
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
		return obj.GetType() == GetType() && Equals((Model?)obj);
	}
}
