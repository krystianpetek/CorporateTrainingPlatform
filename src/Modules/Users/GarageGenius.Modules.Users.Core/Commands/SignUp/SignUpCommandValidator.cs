﻿using FluentValidation;

namespace GarageGenius.Modules.Users.Core.Commands.SignUp;
internal class SignUpCommandValidator : AbstractValidator<SignUpCommand>
{
    public SignUpCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress();
    }
    
    // TODO another rules
}
