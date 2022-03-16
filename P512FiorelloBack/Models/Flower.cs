using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace P512FiorelloBack.Models
{
    public class Flower
    {
        public int Id { get; set; }
        public string MainImage { get; set; }
        [Required]
        [StringLength(maximumLength:50)]
        public string Name { get; set; }
        public double Price { get; set; }
        public double? DiscountPrice { get; set; }
        [Required]
        [StringLength(maximumLength:500)]
        public string Desc { get; set; }
        [Required]
        [StringLength(maximumLength:18)]
        public string SKUCode { get; set; }
        [StringLength(maximumLength: 10)]
        public string Weight { get; set; }
        [StringLength(maximumLength: 30)]
        public string Dimension { get; set; }
        public int? CampaignId { get; set; }
        public Campaign Campaign { get; set; }
        public List<FlowerImage> FlowerImages { get; set; }
        public List<FlowerCategory> FlowerCategories{ get; set; }
        public List<Comment> Comments { get; set; }

    }
}
