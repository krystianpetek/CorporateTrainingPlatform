namespace GarageGenius.Modules.Customers.Core.ValueObjects;
public record UserId
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
}
