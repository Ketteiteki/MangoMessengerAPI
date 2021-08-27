﻿namespace MangoAPI.Presentation.Interfaces
{
    using System.Threading;
    using System.Threading.Tasks;
    using BusinessLogic.ApiCommands.UserChats;
    using Microsoft.AspNetCore.Mvc;

    public interface IUserChatsController
    {
        Task<IActionResult> JoinChatAsync(string chatId, CancellationToken cancellationToken);
        Task<IActionResult> LeaveGroup(string chatId, CancellationToken cancellationToken);
        Task<IActionResult> ArchiveChat(ArchiveChatRequest request, CancellationToken cancellationToken);
    }
}