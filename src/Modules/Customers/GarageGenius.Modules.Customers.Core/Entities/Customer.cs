using GarageGenius.Modules.Customers.Core.ValueObjects;

namespace GarageGenius.Modules.Customers.Core.Entities;
internal class Customer
{
    public CustomerId Id { get; set; }
    public UserId CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public PhoneNumber PhoneNumber { get; set; }
    public EmailAddress EmailAddress { get; set; }
}
