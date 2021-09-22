﻿using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MangoAPI.BusinessLogic.ApiCommands.Communities;
using MangoAPI.BusinessLogic.BusinessExceptions;
using MangoAPI.Domain.Constants;
using MangoAPI.Domain.Enums;
using NUnit.Framework;

namespace MangoAPI.Tests.ApiCommandsTests.Communities
{
    [TestFixture]
    public class CreateChannelCommandHandlerTest
    {
        [Test]
        public async Task CreateGroupCommandHandlerTest_Success()
        {
            using var dbContextFixture = new DbContextFixture();
            var handler = new CreateChannelCommandHandler(dbContextFixture.PostgresDbContext);
            var command = new CreateChannelCommand
            {
                UserId = SeedDataConstants.PetroId,
                CommunityType = CommunityType.PublicChannel,
                ChannelTitle = "Extreme Code",
            };

            var result = await handler.Handle(command, CancellationToken.None);

            result.Success.Should().BeTrue();
        }

        [Test]
        public async Task CreateGroupCommandHandlerTest_ShouldThrowUserNotFound()
        {
            using var dbContextFixture = new DbContextFixture();
            var handler = new CreateChannelCommandHandler(dbContextFixture.PostgresDbContext);
            var command = new CreateChannelCommand
            {
                UserId = Guid.NewGuid(),
                CommunityType = CommunityType.PublicChannel,
                ChannelTitle = "Extreme Code",
            };

            Func<Task> result = async () => await handler.Handle(command, CancellationToken.None);

            await result.Should().ThrowAsync<BusinessException>()
                .WithMessage(ResponseMessageCodes.UserNotFound);
        }
    }
}