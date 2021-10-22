﻿using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MangoAPI.BusinessLogic.ApiCommands.UserChats;
using MangoAPI.BusinessLogic.BusinessExceptions;
using MangoAPI.Domain.Constants;
using NUnit.Framework;

namespace MangoAPI.Tests.ApiCommandsTests.UserChats
{
    [TestFixture]
    public class LeaveGroupCommandHandlerTests
    {
        [Test]
        public async Task LeaveChatCommandHandler_Success()
        {
            using var dbContextFixture = new DbContextFixture();
            var handler = new LeaveGroupCommandHandler(dbContextFixture.PostgresDbContext);
            var command = new LeaveGroupCommand
            {
                UserId = SeedDataConstants.RazumovskyId,
                ChatId = SeedDataConstants.ExtremeCodeFloodId
            };

            var result = await handler.Handle(command, CancellationToken.None);

            result.Response.Success.Should().BeTrue();
        }
        
        [Test]
        public async Task LeaveChatCommandHandler_ShouldThrowUserNotFound()
        {
            using var dbContextFixture = new DbContextFixture();
            var handler = new LeaveGroupCommandHandler(dbContextFixture.PostgresDbContext);
            var command = new LeaveGroupCommand
            {
                UserId = Guid.NewGuid(),
                ChatId = SeedDataConstants.ExtremeCodeFloodId,
            };

            Func<Task> result = async () => await handler.Handle(command, CancellationToken.None);

            await result.Should().ThrowAsync<BusinessException>()
                .WithMessage(ResponseMessageCodes.UserNotFound);
        }
        
        [Test]
        public async Task LeaveChatCommandHandler_ShouldThrowChatNotFound()
        {
            using var dbContextFixture = new DbContextFixture();
            var handler = new LeaveGroupCommandHandler(dbContextFixture.PostgresDbContext);
            var command = new LeaveGroupCommand
            {
                UserId = SeedDataConstants.RazumovskyId,
                ChatId = Guid.NewGuid()
            };

            Func<Task> result = async () => await handler.Handle(command, CancellationToken.None);

            await result.Should().ThrowAsync<BusinessException>()
                .WithMessage(ResponseMessageCodes.ChatNotFound);
        }
    }
}