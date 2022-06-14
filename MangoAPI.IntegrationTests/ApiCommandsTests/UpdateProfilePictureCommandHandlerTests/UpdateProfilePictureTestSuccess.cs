﻿using System.Threading;
using System.Threading.Tasks;
using MangoAPI.BusinessLogic.ApiCommands.Users;
using MangoAPI.IntegrationTests.Helpers;
using Xunit;

namespace MangoAPI.IntegrationTests.ApiCommandsTests.UpdateProfilePictureCommandHandlerTests;

public class UpdateProfilePictureTestSuccess : IntegrationTestBase
{
    private readonly Assert<UpdateProfilePictureResponse> _assert = new();

    [Fact]
    public async Task UpdateProfilePictureTest_Success()
    {
        var userResult = await MangoModule.RequestAsync(CommandHelper.RegisterPetroCommand(), CancellationToken.None);
        var userId = userResult.Response.UserId;
        var file = MangoFilesHelper.GetTestImage();
        var command = new UpdateProfilePictureCommand(UserId: userId, ContentType: "image/jpeg", PictureFile: file);

        var result = await MangoModule.RequestAsync(command, CancellationToken.None);

        _assert.Pass(result);
        await BlobService.DeleteBlobAsync(result.Response.FileName);
    }
}