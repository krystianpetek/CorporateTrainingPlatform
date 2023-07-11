using FluentValidation.TestHelper;
using GarageGenius.Modules.Customers.Application.Commands.CreateCustomer;
using Xunit;

namespace GarageGenius.Modules.Customers.UnitTests.Commands.CreateCustomerCommandTests;
public class CreateCustomerCommandValidatorTests
{
	[Theory]
	[InlineData("Krystian", "Petek", "krystianpetek2@gmail.com", "123123123")]
	public void CreateCustomerCommandValidator_Should_Return_True_When_CorrectValuesArePassed(string firstName, string lastName, string emailAddress, string phoneNumber)
	{
		// arrange
		(var validator, var command) = CreateCustomerCommandValidatorAndCommand(firstName, lastName, emailAddress, phoneNumber);

		// act
		TestValidationResult<CreateCustomerCommand> result = validator.TestValidate(command);

		// assert
		Assert.True(result.IsValid);
		result.ShouldNotHaveValidationErrorFor(phoneNumber => phoneNumber.PhoneNumber);
		result.ShouldNotHaveValidationErrorFor(emailAddress => emailAddress.EmailAddress);
		result.ShouldNotHaveValidationErrorFor(firstName => firstName.FirstName);
		result.ShouldNotHaveValidationErrorFor(lastName => lastName.LastName);
	}

	[Theory]
	[InlineData("Krystian", "Petek", "", "123123123")]
	[InlineData("Krystian", "Petek", "krystianpetek2gmail.com", "123123123")]
	public void CreateCustomerCommandValidator_Should_Return_False_When_EmailAddressValueIsWrong(string firstName, string lastName, string emailAddress, string phoneNumber)
	{
		// arrange
		(var validator, var command) = CreateCustomerCommandValidatorAndCommand(firstName, lastName, emailAddress, phoneNumber);

		// act
		TestValidationResult<CreateCustomerCommand> result = validator.TestValidate(command);

		// assert
		Assert.False(result.IsValid);
		result.ShouldHaveValidationErrorFor(emailAddress => emailAddress.EmailAddress);
		result.ShouldNotHaveValidationErrorFor(phoneNumber => phoneNumber.PhoneNumber);
		result.ShouldNotHaveValidationErrorFor(firstName => firstName.FirstName);
		result.ShouldNotHaveValidationErrorFor(lastName => lastName.LastName);
	}

	[Theory]
	[InlineData("Krystian", "Petek", "krystianpetek2@gmail.com", "")]
	[InlineData("Krystian", "Petek", "krystianpetek2@gmail.com", "null")]
	[InlineData("Krystian", "Petek", "krystianpetek2@gmail.com", "12312312")]
	[InlineData("Krystian", "Petek", "krystianpetek2@gmail.com", "1231231212231123")]
	public void CreateCustomerCommandValidator_Should_Return_False_When_PhoneNumberValueIsWrong(string firstName, string lastName, string emailAddress, string phoneNumber)
	{
		// arrange
		(var validator, var command) = CreateCustomerCommandValidatorAndCommand(firstName, lastName, emailAddress, phoneNumber);

		// act
		TestValidationResult<CreateCustomerCommand> result = validator.TestValidate(command);

		// assert
		Assert.False(result.IsValid);
		result.ShouldHaveValidationErrorFor(phoneNumber => phoneNumber.PhoneNumber);
		result.ShouldNotHaveValidationErrorFor(emailAddress => emailAddress.EmailAddress);
		result.ShouldNotHaveValidationErrorFor(firstName => firstName.FirstName);
		result.ShouldNotHaveValidationErrorFor(lastName => lastName.LastName);
	}

	[Theory]
	[InlineData("", "Petek", "krystianpetek2@gmail.com", "123123121")]
	[InlineData("K", "Petek", "krystianpetek2@gmail.com", "123123121")]
	[InlineData("KrystianKrystianKrystianKrystian", "Petek", "krystianpetek2@gmail.com", "123123121")]
	public void CreateCustomerCommandValidator_Should_Return_False_When_FirstNameValueIsWrong(string firstName, string lastName, string emailAddress, string phoneNumber)
	{
		// arrange
		(var validator, var command) = CreateCustomerCommandValidatorAndCommand(firstName, lastName, emailAddress, phoneNumber);

		// act
		TestValidationResult<CreateCustomerCommand> result = validator.TestValidate(command);

		// assert
		Assert.False(result.IsValid);
		result.ShouldHaveValidationErrorFor(firstName => firstName.FirstName);
		result.ShouldNotHaveValidationErrorFor(phoneNumber => phoneNumber.PhoneNumber);
		result.ShouldNotHaveValidationErrorFor(emailAddress => emailAddress.EmailAddress);
		result.ShouldNotHaveValidationErrorFor(lastName => lastName.LastName);
	}

	[Theory]
	[InlineData("Krystian", "", "krystianpetek2@gmail.com", "123123121")]
	[InlineData("Krystian", "P", "krystianpetek2@gmail.com", "123123121")]
	[InlineData("Krystian", "PetekPetekPetekPetekPetekPetekPetekPetekPetekPetek1", "krystianpetek2@gmail.com", "123123121")]
	public void CreateCustomerCommandValidator_Should_Return_False_When_LastNameValueIsWrong(string firstName, string lastName, string emailAddress, string phoneNumber)
	{
		// arrange
		(var validator, var command) = CreateCustomerCommandValidatorAndCommand(firstName, lastName, emailAddress, phoneNumber);

		// act
		TestValidationResult<CreateCustomerCommand> result = validator.TestValidate(command);

		// assert
		Assert.False(result.IsValid);
		result.ShouldHaveValidationErrorFor(lastName => lastName.LastName);
		result.ShouldNotHaveValidationErrorFor(firstName => firstName.FirstName);
		result.ShouldNotHaveValidationErrorFor(phoneNumber => phoneNumber.PhoneNumber);
		result.ShouldNotHaveValidationErrorFor(emailAddress => emailAddress.EmailAddress);
	}

	private (CreateCustomerCommandValidator, CreateCustomerCommand) CreateCustomerCommandValidatorAndCommand(string firstName, string lastName, string emailAddress, string phoneNumber)
	{
		CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator();
		CreateCustomerCommand command = new CreateCustomerCommand()
		{
			FirstName = firstName,
			LastName = lastName,
			EmailAddress = emailAddress,
			PhoneNumber = phoneNumber
		};

		return (validator, command);
	}
}
