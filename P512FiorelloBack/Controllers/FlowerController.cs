using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P512FiorelloBack.DAL;
using P512FiorelloBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P512FiorelloBack.Controllers
{
    public class FlowerController : Controller
    {
        private readonly AppDbContext _context;

        public FlowerController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Detail(int id,int categoryId)
        {
            Flower flower = await _context.Flowers.Include(f => f.FlowerImages).Include(f => f.FlowerCategories).ThenInclude(fc => fc.Category).FirstOrDefaultAsync(f => f.Id == id);
            if (flower == null) return NotFound();
            //ViewBag.Related = _context.FlowerCategories.Where(fc => fc.CategoryId == categoryId && fc.FlowerId!=flower.Id).Include(fc=>fc.Flower).ToList();
            ViewBag.Related = await _context.Flowers.Where(f => f.FlowerCategories.FirstOrDefault(fc => fc.CategoryId == categoryId).CategoryId == categoryId && f.Id != flower.Id).Include(f=>f.FlowerImages).Include(f=>f.FlowerCategories).ThenInclude(fc=>fc.Category).ToListAsync();
            return View(flower);
        }

        public async Task<IActionResult> Search (string searchStr)
        {
            var flower = await _context.Flowers.Where(f => f.Name.ToUpper().Contains(searchStr.ToUpper())).ToListAsync();
            return PartialView("_SearchPartialView", flower);
        }
    }
}
