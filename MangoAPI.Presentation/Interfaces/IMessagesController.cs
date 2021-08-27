﻿namespace MangoAPI.Presentation.Interfaces
{
    using System.Threading;
    using System.Threading.Tasks;
    using BusinessLogic.ApiCommands.Messages;
    using Microsoft.AspNetCore.Mvc;

    public interface IMessagesController
    {
        Task<IActionResult> GetChatMessages(string chatId, CancellationToken cancellationToken);

        Task<IActionResult> SendMessage(SendMessageRequest request, CancellationToken cancellationToken);

        Task<IActionResult> EditMessage(EditMessageRequest request, CancellationToken cancellationToken);

        Task<IActionResult> DeleteMessage(string messageId, CancellationToken cancellationToken);
    }
}