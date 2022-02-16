using P512FiorelloBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P512FiorelloBack.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public List<Expert> Experts { get; set; }
        public List<Category> Categories { get; set; }
        public List<Flower> Flowers { get; set; }
    }
}
