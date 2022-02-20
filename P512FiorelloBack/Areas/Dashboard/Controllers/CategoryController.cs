using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P512FiorelloBack.DAL;
using P512FiorelloBack.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace P512FiorelloBack.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class CategoryController : Controller

    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Category> categories = await _context.Categories.ToListAsync();

            return View(categories);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Category category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();
            return View(category);  
            
        }

        public IActionResult Create() 
        { 
           return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create (Category category) 
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
