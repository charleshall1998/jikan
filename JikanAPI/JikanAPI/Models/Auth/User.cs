﻿using System.Collections.Generic;

namespace JikanAPI.Models.Auth
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
