using GarageGenius.Modules.Customers.Core.ValueObjects;

namespace GarageGenius.Modules.Customers.Application.Queries.GetCustomerById;
internal record GetCustomerByIdDto
{
	public Guid CustomerId { get; init; }
	public FirstName? FirstName { get; init; }
	public LastName? LastName { get; init; }
	public PhoneNumber? PhoneNumber { get; init; }
	public EmailAddress EmailAddress { get; init; }

	public GetCustomerByIdDto(Guid customerId, FirstName? firstName, LastName? lastName, PhoneNumber? phoneNumber, EmailAddress emailAddress)
	{
		CustomerId = customerId;
		FirstName = firstName;
		LastName = lastName;
		PhoneNumber = phoneNumber;
		EmailAddress = emailAddress;
	}
}
