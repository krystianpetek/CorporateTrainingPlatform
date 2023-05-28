namespace GarageGenius.Modules.Vehicles.Core.Types;

internal sealed class VehicleId : IEquatable<VehicleId>
{
	public Guid Value { get; }

	public VehicleId(Guid value)
	{
		Value = value;
	}

	public static implicit operator VehicleId(Guid value)
	{
		return new VehicleId(value);
	}

	public static implicit operator Guid(VehicleId value)
	{
		return value.Value;
	}

	public bool Equals(VehicleId? other)
	{
		if (other is null) return false;
		if (ReferenceEquals(this, other)) return true;
		return Value == other.Value;
	}

	public override bool Equals(object obj)
	{
		return Equals(obj as VehicleId);
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
