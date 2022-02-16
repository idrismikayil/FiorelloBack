using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace P512FiorelloBack.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [Required]
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
        [Required]
        [StringLength(maximumLength:100)]
        public string LeftIcon { get; set; }
        [Required]
        [StringLength(maximumLength:100)]
        public string RightIcon { get; set; }
        [Required]
        public byte Order { get; set; }
    }
}
