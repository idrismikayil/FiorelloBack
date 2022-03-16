﻿using P512FiorelloBack.Models;

namespace P512FiorelloBack.Areas.Dashboard.ViewModels
{
    public class UserVM
    {
        public string Id { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Roles { get; set; }
        //public User User { get; set; }
        public bool isActive { get; set; } = true;
    }
}
