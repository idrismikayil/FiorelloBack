using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace P512FiorelloBack.Models
{
    public class User: IdentityUser
    {
        public string Fullname { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }
        public bool isActive { get; set; } = true;
        public List<Comment> Comments { get; set; }

    }
}
