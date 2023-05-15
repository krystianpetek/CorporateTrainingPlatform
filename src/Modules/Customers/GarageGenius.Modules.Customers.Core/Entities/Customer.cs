using GarageGenius.Modules.Customers.Core.Types;
using GarageGenius.Modules.Customers.Core.ValueObjects;
using GarageGenius.Shared.Abstractions.Common;

namespace GarageGenius.Modules.Customers.Core.Entities;
internal sealed class Customer : AuditableEntity
{
    // TODO validation domain entity
    public CustomerId Id { get; private set; }
    public UserId? UserId { get; private set; }
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public PhoneNumber? PhoneNumber { get; private set; }
    public EmailAddress EmailAddress { get; private set; }

    private Customer() { }

    public Customer(UserId userId, string firstName, string lastName, PhoneNumber phoneNumber, EmailAddress emailAddress) : this(firstName, lastName, phoneNumber, emailAddress)
    {
        UserId = userId;
    }

    public Customer(string firstName, string lastName, PhoneNumber phoneNumber, EmailAddress emailAddress)
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

    //public void Update(string firstName, string lastName, PhoneNumber phoneNumber, EmailAddress emailAddress)
    //{
    //    FirstName = firstName;
    //    LastName = lastName;
    //    PhoneNumber = phoneNumber;
    //    EmailAddress = emailAddress;
    //}
}
