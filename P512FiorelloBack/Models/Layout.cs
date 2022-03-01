using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P512FiorelloBack.Models
{
    public class Layout
    {
        public int Id { get; set; }
        public string Logo { get; set; }
        public string Intagram{ get; set; }
        public string Facebook { get; set; }

        [NotMapped]
        [Required]
        public IFormFile ImageFile { get; set; }
    }
}
