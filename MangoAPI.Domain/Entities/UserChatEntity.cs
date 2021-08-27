﻿namespace MangoAPI.Domain.Entities
{
    using Enums;

    public sealed class UserChatEntity
    {
        public string UserId { get; set; }

        public string ChatId { get; set; }

        public UserRole RoleId { get; set; }

        public bool IsArchived { get; set; }

        public UserEntity User { get; set; }

        public ChatEntity Chat { get; set; }
    }
}