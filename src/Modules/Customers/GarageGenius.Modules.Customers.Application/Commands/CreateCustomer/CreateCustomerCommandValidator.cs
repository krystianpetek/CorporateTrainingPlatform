using FluentValidation;

namespace GarageGenius.Modules.Customers.Application.Commands.CreateCustomer;
internal class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
	public CreateCustomerCommandValidator()
	{
		RuleFor(x => x.EmailAddress).EmailAddress().NotEmpty().NotNull();
	}
}
