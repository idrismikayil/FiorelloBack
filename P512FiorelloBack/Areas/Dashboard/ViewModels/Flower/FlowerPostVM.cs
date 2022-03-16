using Microsoft.AspNetCore.Http;
using P512FiorelloBack.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace P512FiorelloBack.Areas.Dashboard.ViewModels
{
    public class FlowerPostVM
    {
        [Required]
        [StringLength(maximumLength: 50)]
        public string Name { get; set; }
        public double Price { get; set; }
        [Required]
        [StringLength(maximumLength: 500)]
        public string Desc { get; set; }
        [Required]
        [StringLength(maximumLength: 18)]
        public string SKUCode { get; set; }
        [StringLength(maximumLength: 10)]
        public string Weight { get; set; }
        [StringLength(maximumLength: 30)]
        public string Dimension { get; set; }
        public IFormFile MainImage { get; set; }
        public IFormFile[] Images { get; set; }
        public int? CampaignId { get; set; }
        public List<int> CategoryIds { get; set; }

        //get
        public List<Category> Categories { get; set; }
        public List<Campaign> Campaigns { get; set; }
    }
}
