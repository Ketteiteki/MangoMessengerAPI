﻿namespace MangoAPI.Domain.Entities
{
    using System;

    public class UserInformationEntity
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime? BirthDay { get; set; }

        public string? Website { get; set; }

        public string? Address { get; set; }

        public string? Facebook { get; set; }

        public string? Twitter { get; set; }

        public string? Instagram { get; set; }

        public string? LinkedIn { get; set; }

        public string? ProfilePicture { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual UserEntity User { get; set; }
    }
}