using GarageGenius.Modules.Customers.Core.Types;
using GarageGenius.Modules.Customers.Core.ValueObjects;
using GarageGenius.Shared.Abstractions.Common;

namespace GarageGenius.Modules.Customers.Core.Entities;
internal class Customer : AuditableEntity
{
    public CustomerId Id { get; private set; }
    public UserId UserId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public EmailAddress EmailAddress { get; private set; }

    private Customer() { }
    public Customer(UserId userId, string firstName, string lastName, PhoneNumber phoneNumber, EmailAddress emailAddress)
    {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        EmailAddress = emailAddress; // validation email
    }
}
