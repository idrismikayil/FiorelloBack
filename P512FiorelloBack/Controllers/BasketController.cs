using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using P512FiorelloBack.DAL;
using P512FiorelloBack.Models;
using P512FiorelloBack.ViewModels;
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

            List<BasketVM> basket;

            var basketJson = Request.Cookies["basket"];
            if (string.IsNullOrEmpty(basketJson))
            {
                basket = new List<BasketVM>();
            }
            else 
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(basketJson);
            }

            var existFlower = basket.Find(b => b.Flower.Id == id);

            if (existFlower == null)
            {
                basket.Add(new BasketVM { Flower = flower});
            }
            else
            {
                existFlower.Count++;
            }

            Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> GetBasket(int id)
        {
            var basketJson = Request.Cookies["basket"];
            List<BasketVM> basket = JsonConvert.DeserializeObject<List<BasketVM>>(basketJson);
            List<BasketVM> newBasket = new List<BasketVM>();

            foreach (var item in basket)
            {
                Flower flower = await _context.Flowers.FindAsync(item.Flower.Id);
                if (flower == null)
                {
                    continue;
                }
                newBasket.Add(new BasketVM { Flower = flower, Count = item.Count });
            }

            Response.Cookies.Append("bakset", JsonConvert.SerializeObject(newBasket));

            return Content(JsonConvert.SerializeObject(newBasket));
        }

    }
}