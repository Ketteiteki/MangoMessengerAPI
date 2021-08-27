﻿namespace MangoAPI.BusinessLogic.ApiCommands.Users
{
    using System;
    using FluentValidation;

    public class VerifyEmailCommandValidator : AbstractValidator<VerifyEmailCommand>
    {
        public VerifyEmailCommandValidator()
        {
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(1, 300);

            RuleFor(x => x.UserId).Must(x => Guid.TryParse(x, out _))
                .WithMessage("Verify  email: User Id cannot be parsed.");
        }
    }
}