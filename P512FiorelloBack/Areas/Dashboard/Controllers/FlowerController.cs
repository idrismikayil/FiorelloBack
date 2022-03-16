using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P512FiorelloBack.Areas.Dashboard.ViewModels;
using P512FiorelloBack.Constants;
using P512FiorelloBack.DAL;
using P512FiorelloBack.Extensions;
using P512FiorelloBack.Models;
using P512FiorelloBack.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace P512FiorelloBack.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize(Roles = RoleConstants.Admin)]
    public class FlowerController : Controller
    {
        private readonly AppDbContext _context;

        public FlowerController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var flowers = await _context.Flowers.Select(f=> new FlowerListVM
            {
                Id = f.Id,
                Name = f.Name,
                Price = f.Price,
                MainImage = f.MainImage,
            }).ToListAsync();
            return View(flowers);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var flower = await _context.Flowers.Include(f => f.FlowerImages)
                   .Include(f => f.Comments)
                   .ThenInclude(c => c.User)
                   .Include(f => f.FlowerCategories)
                   .ThenInclude(fc => fc.Category).AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);

            if (flower == null) return NotFound();

            return View(flower);
        }

        public async Task<IActionResult> Create()
        {
            FlowerPostVM model = new FlowerPostVM
            {
                Campaigns = await _context.Campaigns.ToListAsync(),
                Categories = await _context.Categories.ToListAsync()
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FlowerPostVM model)
        {
            model.Campaigns = await _context.Campaigns.ToListAsync();
            model.Categories = await _context.Categories.ToListAsync();

            if (!ModelState.IsValid) return View(model);

            var campaign = await _context.Campaigns.FindAsync(model.CampaignId);
            if (campaign == null)
            {
                ModelState.AddModelError(nameof(FlowerPostVM.CampaignId), "Choose a valid campaign");
                return View(model);
            }

            List<FlowerCategory> categories = new List<FlowerCategory>();
            foreach (var categoryId in model.CategoryIds)
            {
                var category = await _context.Categories.FindAsync(categoryId);
                if (category == null)
                {
                    ModelState.AddModelError(nameof(FlowerPostVM.CategoryIds), "Choose a valid category");
                    return View(model);
                }
                categories.Add(new FlowerCategory { CategoryId = categoryId });
            }

            if (!model.MainImage.IsOkay())
            {
                ModelState.AddModelError(nameof(FlowerPostVM.MainImage), "There is a problem in your file");
                return View(model);
            }

            List<FlowerImage> images = new List<FlowerImage>();
            foreach (var image in model.Images)
            {
                if (!image.IsOkay())
                {
                    ModelState.AddModelError(nameof(FlowerPostVM.Images), $"There is a problem in your {image.FileName} file");
                    return View(model);
                }
                images.Add(new FlowerImage
                {
                    Name = FileUtils.Create(FileConstants.ImagePath, image)
                });
            }

            Flower flower = new Flower
            {
                Name = model.Name,
                Price = model.Price,
                Dimension = model.Dimension,
                DiscountPrice = model.Price - (model.Price * campaign.DiscountPercent / 100),
                Weight = model.Weight,
                SKUCode = model.SKUCode,
                Desc = model.Desc,
                CampaignId = model.CampaignId,
                MainImage = FileUtils.Create(FileConstants.ImagePath, model.MainImage),
                FlowerImages = images,
                FlowerCategories = categories
            };

            await _context.Flowers.AddAsync(flower);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            var flower = await _context.Flowers.Include(f => f.FlowerImages).Include(f => f.FlowerCategories).ThenInclude(fc => fc.Category).FirstOrDefaultAsync(f => f.Id == id);
            if (flower == null) return NotFound();
            return View(flower);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]

        public async Task<IActionResult> DeleteFlower(int id)
        {
            var flower = await _context.Flowers.Include(f => f.FlowerImages).Include(f => f.FlowerCategories).ThenInclude(fc => fc.Category).FirstOrDefaultAsync(f => f.Id == id);
            if (flower == null) return NotFound();

            foreach (var images in flower.FlowerImages)
            {
                FileUtils.Delete(Path.Combine(FileConstants.ImagePath, images.Name));
            }
            FileUtils.Delete(Path.Combine(FileConstants.ImagePath, flower.MainImage));

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var flower = await _context.Flowers.Include(f => f.FlowerImages)
                   .Include(f => f.Comments)
                   .ThenInclude(c => c.User)
                   .Include(f => f.FlowerCategories)
                   .ThenInclude(fc => fc.Category).FirstOrDefaultAsync(f => f.Id == id);
            if (flower == null) return NotFound();

            //model.Categories = await _context.Categories.ToListAsync();
            //model.Campaigns = await _context.Campaigns.ToListAsync();

            //if (!ModelState.IsValid) return View(model);

            FlowerPostVM model = new FlowerPostVM
            {
                CategoryIds = flower.FlowerCategories.Select(c => c.Id).ToList(),
                Name = flower.Name,
                Desc = flower.Desc,
                Price = flower.Price,
                SKUCode = flower.SKUCode,
                Weight = flower.Weight,
                Dimension = flower.Dimension,
                CampaignId = flower.CampaignId,
                Categories = await _context.Categories.ToListAsync(),
                Campaigns = await _context.Campaigns.ToListAsync()
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, FlowerPostVM model)
        {
            var flower = await _context.Flowers.Include(f => f.FlowerImages)
                 .Include(f => f.Comments)
                 .ThenInclude(c => c.User)
                 .Include(f => f.FlowerCategories)
                 .ThenInclude(fc => fc.Category).FirstOrDefaultAsync(f => f.Id == id);
            if (flower == null) return NotFound();

            model.Categories = await _context.Categories.ToListAsync();
            model.Campaigns = await _context.Campaigns.ToListAsync();

            if (!ModelState.IsValid) return View(model);


            var campaign = await _context.Campaigns.FindAsync(model.CampaignId);
            if (campaign == null)
            {
                ModelState.AddModelError(nameof(FlowerPostVM.CampaignId), "Choose a valid campaign");
                return View(model);
            }

            List<FlowerCategory> categories = new List<FlowerCategory>();
            foreach (var categoryId in model.CategoryIds)
            {
                var category = await _context.Categories.FindAsync(categoryId);
                if (category == null)
                {
                    ModelState.AddModelError(nameof(FlowerPostVM.CategoryIds), "Choose a valid category");
                    return View(model);
                }
                categories.Add(new FlowerCategory { CategoryId = categoryId });
            }

            if (model.MainImage != null)
            {
                if (!model.MainImage.IsOkay())
                {
                    ModelState.AddModelError(nameof(FlowerPostVM.MainImage), "There is a problem in your file");
                    return View(model);
                }

                FileUtils.Delete(Path.Combine(FileConstants.ImagePath, flower.MainImage));
            }

            List<FlowerImage> images = null;

            if (model.Images != null)
            {
                images = new List<FlowerImage>();
                foreach (var image in model.Images)
                {
                    if (!model.MainImage.IsOkay())
                    {
                        ModelState.AddModelError(nameof(FlowerPostVM.Images), "There is a problem in your file");
                        return View(model);
                    }
                    images.Add(new FlowerImage { FlowerId = flower.Id, Name = FileUtils.Create(FileConstants.ImagePath, image) });
                }

                foreach (var image in flower.FlowerImages)
                {
                    FileUtils.Delete2(image.Name);
                }
            }

            flower.Name = model.Name;
            flower.Desc = model.Desc;
            flower.Weight = model.Weight;
            flower.Price = model.Price;
            flower.SKUCode = model.SKUCode;
            flower.DiscountPrice = model.Price - (model.Price * campaign.DiscountPercent / 100);
            flower.Dimension = flower.Dimension;
            flower.FlowerCategories = categories;
            flower.FlowerImages = images.Count > 0 ? images : flower.FlowerImages;
            flower.MainImage = model.MainImage != null ? FileUtils.Create(FileConstants.ImagePath, model.MainImage) : flower.MainImage;


            _context.Flowers.Update(flower);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
