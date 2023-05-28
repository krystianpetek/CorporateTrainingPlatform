using GarageGenius.Modules.Customers.Core.ValueObjects;

namespace GarageGenius.Modules.Customers.Application.Queries.GetCustomerByUserId;
internal record GetCustomerByUserIdDto
{
	public Guid CustomerId { get; init; }
	public FirstName? FirstName { get; init; }
	public LastName? LastName { get; init; }
	public PhoneNumber? PhoneNumber { get; init; }
	public EmailAddress EmailAddress { get; init; }

	public GetCustomerByUserIdDto(Guid customerId, FirstName? firstName, LastName? lastName, PhoneNumber? phoneNumber, EmailAddress emailAddress)
	{
		CustomerId = customerId;
		FirstName = firstName;
		LastName = lastName;
		PhoneNumber = phoneNumber;
		EmailAddress = emailAddress;
	}
}
