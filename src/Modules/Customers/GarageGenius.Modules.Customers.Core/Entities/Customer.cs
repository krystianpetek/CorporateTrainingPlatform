using GarageGenius.Modules.Customers.Core.Types;
using GarageGenius.Modules.Customers.Core.ValueObjects;
using GarageGenius.Shared.Abstractions.Common;

namespace GarageGenius.Modules.Customers.Core.Entities;
internal sealed class Customer : AuditableEntity
{
    // TODO validation domain entity
    public CustomerId Id { get; private set; }
    public UserId? UserId { get; private set; } // TODO linking to user if customer created separately, but how ?
    public FirstName? FirstName { get; private set; }
    public LastName? LastName { get; private set; }
    public PhoneNumber? PhoneNumber { get; private set; }
    public EmailAddress EmailAddress { get; private set; }

    private Customer() { }

    public Customer(UserId userId, FirstName firstName, LastName lastName, PhoneNumber phoneNumber, EmailAddress emailAddress) : this(firstName, lastName, phoneNumber, emailAddress)
    {
        UserId = userId;
    }

    public Customer(FirstName firstName, LastName lastName, PhoneNumber phoneNumber, EmailAddress emailAddress)
    {
        Id = new CustomerId(Guid.NewGuid());
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        EmailAddress = emailAddress;        
    }

    public Customer(UserId userId, EmailAddress emailAddress)
    {
        Id = new CustomerId(Guid.NewGuid());
        UserId = userId;
        EmailAddress = emailAddress;
    }

    public void Update(FirstName firstName, LastName lastName, PhoneNumber phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
    }
}
