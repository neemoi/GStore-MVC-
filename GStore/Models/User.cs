﻿using Microsoft.AspNetCore.Identity;

namespace GStore.Models
{
    public partial class User : IdentityUser
    {
        public string Password { get; set; } = null!;

        public string? Name { get; set; } = null;

        public string? Phone { get; set; } = null;

        public string? Country { get; set; } = null;

        public string? City { get; set; } = null;

        public string? Address { get; set; } = null;
    }
}