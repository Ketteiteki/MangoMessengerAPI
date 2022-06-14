﻿using System.Threading;
using System.Threading.Tasks;
using MangoAPI.BusinessLogic.ApiCommands.Communities;
using MangoAPI.IntegrationTests.Helpers;
using Xunit;

namespace MangoAPI.IntegrationTests.ApiCommandsTests.CreateChannelCommandHandlerTests;

public class CreateChannelTestSuccess : IntegrationTestBase
{
    private readonly Assert<CreateCommunityResponse> _assert = new();

    [Fact]
    public async Task CreateChannelTest_Success()
    {
        var user = 
            await MangoModule.RequestAsync(CommandHelper.RegisterPetroCommand(), CancellationToken.None);

        var result = 
            await MangoModule.RequestAsync(CommandHelper.CreateExtremeCodeMainChatCommand(user.Response.UserId), 
                                           CancellationToken.None);

        _assert.Pass(result);
    }

}