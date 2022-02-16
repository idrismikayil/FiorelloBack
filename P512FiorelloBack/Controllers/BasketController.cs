using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using P512FiorelloBack.DAL;
using P512FiorelloBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P512FiorelloBack.Controllers
{
    public class BasketController : Controller
    {
        private readonly AppDbContext _context;

        public BasketController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> AddToBasket(int id)
        {
            Flower flower = await _context.Flowers.FindAsync(id);
            if (flower == null) return RedirectToAction("Index", "Home");

            List<Flower> basket;

            var basketJson = Request.Cookies["basket"];
            if (string.IsNullOrEmpty(basketJson))
            {
                basket = new List<Flower>();
            }
            else 
            {
                basket = JsonConvert.DeserializeObject<List<Flower>>(basketJson);
            }

            basket.Add(flower);

            Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));
            return RedirectToAction("Index", "Home");
        }

        public IActionResult GetBasket(int id)
        {
            var basketJson = Request.Cookies["basket"];
            return Content(basketJson);
        }

    }
}
