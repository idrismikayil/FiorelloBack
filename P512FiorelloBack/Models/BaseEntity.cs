using System;

namespace P512FiorelloBack.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime DeletedAt { get; set; }
        public bool isActive { get; set; }
    }
}
