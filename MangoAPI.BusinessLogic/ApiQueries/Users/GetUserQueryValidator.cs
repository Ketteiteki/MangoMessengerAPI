﻿namespace MangoAPI.BusinessLogic.ApiQueries.Users
{
    using System;
    using FluentValidation;

    public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
    {
        public GetUserQueryValidator()
        {
            RuleFor(x => x.UserId).Must(x => Guid.TryParse(x, out _))
                .WithMessage("GetUserQuery: User Id cannot be parsed.");
        }
    }
}