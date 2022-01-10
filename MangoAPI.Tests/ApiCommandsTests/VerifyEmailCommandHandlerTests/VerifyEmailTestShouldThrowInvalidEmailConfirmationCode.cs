﻿using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MangoAPI.BusinessLogic.ApiCommands.Users;
using MangoAPI.BusinessLogic.Responses;
using MangoAPI.Domain.Constants;
using MangoAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MangoAPI.Tests.ApiCommandsTests.VerifyEmailCommandHandlerTests
{
    public class VerifyEmailTestShouldThrowInvalidEmailConfirmationCode : ITestable<VerifyEmailCommand, ResponseBase>
    {
        private readonly MangoDbFixture _mangoDbFixture = new();
        private readonly Assert<ResponseBase> _assert = new();

        [Fact]
        public async Task VerifyEmailTest_Success()
        {
            Seed();
            const string expectedMessage = ResponseMessageCodes.InvalidEmailConfirmationCode;
            string expectedDetails = ResponseMessageCodes.ErrorDictionary[expectedMessage];
            var handler = CreateHandler();
            var command = new VerifyEmailCommand
            {
                Email = _user.Email,
                EmailCode = Guid.NewGuid()
            };

            var result = await handler.Handle(command, CancellationToken.None);
            
            _assert.Fail(result, expectedMessage, expectedDetails);
        }

        public bool Seed()
        {
            _mangoDbFixture.Context.Users.Add(_user);

            _mangoDbFixture.Context.SaveChanges();

            _mangoDbFixture.Context.Entry(_user).State = EntityState.Detached;
            
            return true;
        }

        public IRequestHandler<VerifyEmailCommand, Result<ResponseBase>> CreateHandler()
        {
            var context = _mangoDbFixture.Context;
            var responseFactory = new ResponseFactory<ResponseBase>();
            var handler = new VerifyEmailCommandHandler(context, responseFactory);
            return handler;
        }

        private readonly UserEntity _user = new()
        {
            DisplayName = "razumovsky r",
            Bio = "11011 y.o Dotnet Developer from $\"{cityName}\"",
            Id = SeedDataConstants.RazumovskyId,
            UserName = "razumovsky_r",
            Email = "kolosovp95@gmail.com",
            NormalizedEmail = "KOLOSOVP94@GMAIL.COM",
            EmailCode = Guid.NewGuid(),
            EmailConfirmed = false,
            PhoneNumberConfirmed = false,
            Image = "razumovsky_picture.jpg"
        };
    }
}