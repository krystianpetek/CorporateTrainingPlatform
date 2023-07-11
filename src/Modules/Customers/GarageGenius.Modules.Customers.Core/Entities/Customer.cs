using GarageGenius.Modules.Customers.Core.Types;
using GarageGenius.Modules.Customers.Core.ValueObjects;
using GarageGenius.Shared.Abstractions.Common;

namespace GarageGenius.Modules.Customers.Core.Entities;
internal sealed class Customer : AuditableEntity
{
	// TODO validation domain entity
	internal CustomerId CustomerId { get; private set; }
	internal UserId? UserId { get; private set; } // TODO linking to user if customer created separately, but how ?
												  // TODO - remove userId? and GetCustomerByUserId
	internal FirstName? FirstName { get; private set; }
	internal LastName? LastName { get; private set; }
	internal PhoneNumber? PhoneNumber { get; private set; }
	internal EmailAddress EmailAddress { get; private set; }

	private Customer() { }

	public Customer(FirstName firstName, LastName lastName, PhoneNumber phoneNumber, EmailAddress emailAddress)
	{
		CustomerId = new CustomerId(Guid.NewGuid());
		FirstName = firstName;
		LastName = lastName;
		PhoneNumber = phoneNumber;
		EmailAddress = emailAddress;
	}

	public Customer(CustomerId customerId, UserId userId, EmailAddress emailAddress)
	{
		CustomerId = customerId;
		UserId = userId;
		EmailAddress = emailAddress;
	}

	internal void UpdateCustomer(FirstName firstName, LastName lastName, PhoneNumber phoneNumber)
	{
		FirstName = firstName;
		LastName = lastName;
		PhoneNumber = phoneNumber;
	}
}
