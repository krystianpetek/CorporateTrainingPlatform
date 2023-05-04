namespace GarageGenius.Modules.Customers.Core.ValueObjects;
public record CustomerId
{
    public Guid Value { get; }

    public CustomerId(Guid value)
    {
        Value = value;
    }

    public static implicit operator CustomerId(Guid value)
    {
        return new CustomerId(value);
    }

    public static implicit operator Guid(CustomerId customerId)
    {
        return customerId.Value;
    }
}
