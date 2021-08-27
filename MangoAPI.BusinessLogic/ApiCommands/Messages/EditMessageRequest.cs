﻿namespace MangoAPI.BusinessLogic.ApiCommands.Messages
{
    using Newtonsoft.Json;

    public record EditMessageRequest
    {
        [JsonConstructor]
        public EditMessageRequest(string messageId, string modifiedText)
        {
            MessageId = messageId;
            ModifiedText = modifiedText;
        }

        public string MessageId { get; }
        public string ModifiedText { get; }
    }

    public static class EditMessageCommandMapper
    {
        public static EditMessageCommand ToCommand(this EditMessageRequest model, string userId)
        {
            return new ()
            {
                MessageId = model.MessageId,
                ModifiedText = model.ModifiedText,
                UserId = userId,
            };
        }
    }
}