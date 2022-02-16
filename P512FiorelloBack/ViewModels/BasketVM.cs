using P512FiorelloBack.Models;

namespace P512FiorelloBack.ViewModels
{
    public class BasketVM
    {
        public Flower Flower { get; set; }
        public int Count { get; set; } = 1;
    }
}
