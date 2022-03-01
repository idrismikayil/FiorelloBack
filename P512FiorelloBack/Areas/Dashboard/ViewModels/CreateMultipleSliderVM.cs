using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P512FiorelloBack.Areas.Dashboard.ViewModels
{
    public class CreateMultipleSliderVM
    {
        [Required]
        [StringLength(maximumLength: 300)]
        public string Desc { get; set; }
        [Required]
        [StringLength(maximumLength: 170)]
        public string Title { get; set; }
        [StringLength(maximumLength: 300)]
        public string SignatureImage { get; set; }


        [NotMapped]
        [Required]
        public IFormFile SignatureImageFile { get; set; }

        public IFormFile[] Images { get; set; }
    }
}
