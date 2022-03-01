using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P512FiorelloBack.Constants;
using P512FiorelloBack.DAL;
using P512FiorelloBack.Extensions;
using P512FiorelloBack.Models;
using P512FiorelloBack.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace P512FiorelloBack.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class LayoutController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public LayoutController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            List<Layout> layouts = await _context.Layouts.ToListAsync();
            return View(layouts);
        }

        public async Task<IActionResult> Details(int id)
        {
            Layout layout = await _context.Layouts.FindAsync(id);
            if (layout == null) return NotFound();
            return View(layout);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Layout layout)
        {
            if (!ModelState.IsValid) return View();

            if (!layout.ImageFile.IsSupported("image"))
            {
                ModelState.AddModelError(nameof(Layout.ImageFile), "File type is unsupported, please select image");
                return View();
            }
            if (layout.ImageFile.IsGreatergivenMb(1))
            {
                ModelState.AddModelError(nameof(Layout.ImageFile), "File size cannot be greater than 1 mb");
                return View();
            }

            layout.Logo = FileUtils.Create(FileConstants.ImagePath, layout.ImageFile);

            await _context.Layouts.AddAsync(layout);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            Layout layout = await _context.Layouts.FindAsync(id);
            if (layout == null) return NotFound();
            return View(layout);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Layout layout)
        {
            if (!ModelState.IsValid) return View();
            if (layout.Id != id) return BadRequest();
            bool isExist = await _context.Layouts.AnyAsync(c => c.Id == id);
            if (!isExist) return NotFound();

            _context.Layouts.Update(layout);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            Layout layout = await _context.Layouts.FindAsync(id);
            if (layout == null) return NotFound();
            return View(layout);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteLayout(int id)
        {
            Layout layout = await _context.Layouts.FindAsync(id);
            _context.Layouts.Remove(layout);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
