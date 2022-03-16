using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace P512FiorelloBack.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [StringLength(maximumLength:300)]
        public string Image { get; set; }
        [Required]
        [StringLength(maximumLength:300)]
        public string Desc { get; set; }
        [Required]
        [StringLength(maximumLength:170)]
        public string Title { get; set; }
        [StringLength(maximumLength:300)]
        public string SignatureImage { get; set; }
        [StringLength(maximumLength:100)]
        public string LeftIcon { get; set; }
        [StringLength(maximumLength:100)]
        public string RightIcon { get; set; }
        [Required]
        public byte Order { get; set; }

        [NotMapped]
        [Required]
        public IFormFile ImageFile { get; set; }

        [NotMapped]
        [Required]
        public IFormFile SignatureImageFile { get; set; }
    }
}
