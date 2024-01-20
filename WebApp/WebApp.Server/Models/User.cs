﻿namespace WebApp.Server.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }

        public ICollection<UserFile> UserFiles { get; set; }
    }
}