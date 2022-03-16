using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P512FiorelloBack.DAL;
using P512FiorelloBack.Models;
using P512FiorelloBack.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P512FiorelloBack.Controllers
{
    public class FlowerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;

        public FlowerController(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Detail(int id,int categoryId)
        {
            Flower flower = _context.Flowers.Include(f => f.FlowerImages)
                .Include(f => f.Comments)
                .ThenInclude(c => c.User)
                .Include(f => f.FlowerCategories)
                .ThenInclude(fc => fc.Category).FirstOrDefault(f => f.Id == id);

            if (flower == null) return NotFound();
            ViewBag.Related = _context.Flowers.Where(f => f.FlowerCategories
                .FirstOrDefault(fc => fc.CategoryId == categoryId).
                CategoryId == categoryId && f.Id != flower.Id)
                .Include(f => f.FlowerImages).Include(f => f.FlowerCategories)
                 .ThenInclude(fc => fc.Category).ToList();


            FlowerDetailVM model = new FlowerDetailVM
            {
                Flower = flower
            };

            return View(model);
        }

        public async Task<IActionResult> Search (string searchStr)
        {
            if (string.IsNullOrWhiteSpace(searchStr))
            {
                return PartialView("_SearchPartialView", new List<Flower>());
            }

            var flower = await _context.Flowers.Where(f => f.Name.ToUpper().Contains(searchStr.ToUpper())).ToListAsync();
            return PartialView("_SearchPartialView", flower);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> AddComment(int id, FlowerDetailVM model)
        {
            var flower = await _context.Flowers.Include(f => f.FlowerImages).Include(f => f.Comments).ThenInclude(c => c.User)
                .Include(f => f.FlowerCategories).ThenInclude(fc => fc.Category).FirstOrDefaultAsync(f => f.Id == id);
            if (flower == null) return NotFound();

            if (!ModelState.IsValid)
            {
                model.Flower = flower;
                return View(model);
            }

            var comment = new Comment
            {
                Description = model.Comment.Description,
                UserId = _userManager.GetUserId(User),
                FlowerId = id
            };

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Detail), new { id });
        }
    }
}
