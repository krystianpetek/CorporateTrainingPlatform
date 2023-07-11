using GarageGenius.Modules.Customers.Core.Entities;
using GarageGenius.Modules.Customers.Core.Exceptions;
using GarageGenius.Modules.Customers.Core.Types;
using GarageGenius.Modules.Customers.Core.ValueObjects;
using Xunit;

namespace GarageGenius.Modules.Customers.UnitTests.Entities;

public class CustomerTests
{
	[Theory]
	[InlineData("krystianpetek2@gmail.com")]
	public void Create_Customer_Should_Return_CustomerWithEmail(string emailAddress)
	{
		// arrange
		Guid id = Guid.NewGuid();
		CustomerId customerIdVo = new CustomerId(id);
		UserId userIdVo = new UserId(id);
		EmailAddress emailAddressVo = new EmailAddress(emailAddress);

		// act
		Customer customer = new Customer(customerIdVo, userIdVo, emailAddressVo);

		// assert		
		Assert.Equal(customerIdVo, customer.CustomerId);
		Assert.Equal(userIdVo, customer.UserId);
		Assert.Equal(emailAddress, customer.EmailAddress);
	}

	[Theory]
	[InlineData("Krystian", "Petek", "123456789", "krystianpetek2@gmail.com")]
	public void Create_Customer_Should_Return_Correct_Customer_Values(string firstName, string lastName, string phoneNumber, string emailAddress)
	{
		// arrange
		FirstName firstNameVo = new FirstName(firstName);
		LastName lastNameVo = new LastName(lastName);
		PhoneNumber phoneNumberVo = new PhoneNumber(phoneNumber);
		EmailAddress emailAddressVo = new EmailAddress(emailAddress);

		// act
		Customer customer = new Customer(firstNameVo, lastNameVo, phoneNumberVo, emailAddressVo);

		// assert
		Assert.Equal(firstName, customer.FirstName?.Value);
		Assert.Equal(lastName, customer.LastName?.Value);
		Assert.Equal(phoneNumber, customer.PhoneNumber?.Value);
		Assert.Equal(emailAddress, customer.EmailAddress?.Value);
	}

	[Theory]
	[InlineData("krystianpetek2@")]
	[InlineData("krystianpetek2.gmail.com")]
	[InlineData("")]
	public void Create_Email_With_Wrong_Value_Should_Throw_Exception(string emailAddress)
	{
		// arrange
		Guid id = Guid.NewGuid();
		CustomerId customerIdVo = new CustomerId(id);
		UserId userIdVo = new UserId(id);

		// act
		Action action = () => { EmailAddress emailAddressVo = new EmailAddress(emailAddress); };

		// assert
		Assert.Throws<InvalidEmailException>(action);
	}

	[Theory]
	[InlineData("Krystianpetek2@gmail.com")]
	[InlineData("KRYSTIANpetek2@GMAIL.com")]
	public void Create_Email_With_Sensitive_Case_Value_Should_Return_Email_InLowerCase(string emailAddress)
	{
		// arrange
		Guid id = Guid.NewGuid();
		CustomerId customerIdVo = new CustomerId(id);
		UserId userIdVo = new UserId(id);

		// act
		EmailAddress emailAddressVo = new EmailAddress(emailAddress);

		// assert
		Assert.Equal(emailAddress.ToLowerInvariant(), emailAddressVo.Value);
	}

	[Theory]
	[InlineData("krystianpetek2@gmail.com", "Krystian", "Petek", "123456789")]
	public void UpdateCustomer_Should_Return_UpdatedCustomer(string emailAddress, string firstName, string lastName, string phoneNumber)
	{
		// arrange
		Guid id = Guid.NewGuid();
		CustomerId customerIdVo = new CustomerId(id);
		UserId userIdVo = new UserId(id);
		EmailAddress emailAddressVo = new EmailAddress(emailAddress);

		// act
		Customer customer = new Customer(customerIdVo, userIdVo, emailAddressVo);
		customer.UpdateCustomer(firstName, lastName, phoneNumber);

		// assert		
		Assert.Equal(customerIdVo, customer.CustomerId);
		Assert.Equal(userIdVo, customer.UserId);
		Assert.Equal(emailAddress, customer.EmailAddress);
		Assert.Equal(firstName, customer.FirstName?.Value);
		Assert.Equal(lastName, customer.LastName?.Value);
		Assert.Equal(phoneNumber, customer.PhoneNumber?.Value);
	}
}