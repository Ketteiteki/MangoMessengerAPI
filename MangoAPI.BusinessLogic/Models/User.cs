using System;
using System.ComponentModel;

namespace MangoAPI.BusinessLogic.Models
{
    public record User
    {
        [DefaultValue("8e08f435-8bd9-4eae-8c0b-e981f9424f19")]
        public Guid UserId { get; set; }

        [DefaultValue("Ivan")]
        public string FirstName { get; init; }

        [DefaultValue("Ivanov")]
        public string LastName { get; init; }

        [DefaultValue("Ivan Ivanov")]
        public string DisplayName { get; init; }

        [DefaultValue("48743615532")]
        public string PhoneNumber { get; init; }

        [DefaultValue("1983-05-25T00:00:00")]
        public string BirthdayDate { get; init; }

        [DefaultValue("ivan.ivanov@wp.pl")]
        public string Email { get; init; }

        [DefaultValue("ivan.ivanov.com")]
        public string Website { get; init; }

        [DefaultValue("ivan.ivanov")]
        public string Username { get; init; }

        [DefaultValue("User of the Mango messenger")]
        public string Bio { get; init; }

        [DefaultValue("Kyiv, Ukraine")]
        public string Address { get; init; }

        [DefaultValue("ivan.ivanov")]
        public string Facebook { get; init; }

        [DefaultValue("ivan.ivanov")]
        public string Twitter { get; init; }

        [DefaultValue("ivan.ivanov")]
        public string Instagram { get; init; }

        [DefaultValue("ivan.ivanov")]
        public string LinkedIn { get; init; }

        [DefaultValue(365842)]
        public int PublicKey { get; init; }

        [DefaultValue("Uploads/ivan-ivanov.jpg")]
        public string PictureUrl { get; init; }
    }
}