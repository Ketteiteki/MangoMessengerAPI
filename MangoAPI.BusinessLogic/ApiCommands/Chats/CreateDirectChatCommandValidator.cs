﻿namespace MangoAPI.BusinessLogic.ApiCommands.Chats
{
    using System;
    using FluentValidation;

    public class CreateDirectChatCommandValidator : AbstractValidator<CreateDirectChatCommand>
    {
        public CreateDirectChatCommandValidator()
        {
            RuleFor(x => x.UserId).Must(x => Guid.TryParse(x, out _))
                .WithMessage("Create direct chat: User Id cannot be parsed.");

            RuleFor(x => x.PartnerId).Must(x => Guid.TryParse(x, out _))
                .WithMessage("Create direct chat: Partner Id cannot be parsed.");
        }
    }
}