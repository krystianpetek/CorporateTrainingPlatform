namespace GarageGenius.Modules.Users.Core.ValueObjects;

internal sealed class UserState : IEquatable<UserState>
{
    private const string ActiveState = "Active";
    private const string UnactiveState = "Unactive";

    public string Value { get; }
    internal UserState(string value)
    {
        Value = value;
    }

    internal static UserState Active => new UserState(ActiveState);

    internal static UserState Unactive => new UserState(UnactiveState);

    public bool Equals(UserState? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as UserState);
    }

    public override int GetHashCode()
    {
        return Value is not null ? Value.GetHashCode() : 0;
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static implicit operator UserState(string value)
    {
        return new UserState(value);
    }

    public static implicit operator string(UserState value)
    {
        return value.Value;
    }
}