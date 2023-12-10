using FluentValidation;
using GarageGenius.Modules.Users.Application.Commands.DeactivateUser;
using GarageGenius.Modules.Users.Core.Entities;

namespace GarageGenius.Modules.Users.Application.Commands.CreateUser;
internal class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Role)
            .NotEmpty()
            .Must(x =>
                x == Roles.Administrator ||
                x == Roles.Customer ||
                x == Roles.Employee ||
                x == Roles.Manager);
    }
}