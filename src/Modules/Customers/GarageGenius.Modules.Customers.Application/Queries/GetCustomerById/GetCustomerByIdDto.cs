using GarageGenius.Modules.Customers.Core.ValueObjects;

namespace GarageGenius.Modules.Customers.Application.Queries.GetCustomerById;
internal record GetCustomerByIdDto
{
    public Guid Id { get; init; }
    public FirstName? FirstName { get; init; }
    public LastName? LastName { get; init; }
    public PhoneNumber? PhoneNumber { get; init; }
    public EmailAddress EmailAddress { get; init; }

    public GetCustomerByIdDto(Guid id, FirstName? firstName, LastName? lastName, PhoneNumber? phoneNumber, EmailAddress emailAddress)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        EmailAddress = emailAddress;
    }
}
