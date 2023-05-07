namespace GarageGenius.Modules.Customers.Core.Types;

internal sealed class UserId : IEquatable<UserId>
{
    public Guid Value { get; }

    public UserId(Guid value)
    {
        Value = value;
    }

    public static implicit operator UserId(Guid value)
    {
        return new UserId(value);
    }

    public static implicit operator Guid(UserId userId)
    {
        return userId.Value;
    }

    public bool Equals(UserId? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as UserId);
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
