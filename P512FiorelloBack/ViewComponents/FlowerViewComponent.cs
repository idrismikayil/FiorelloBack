using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P512FiorelloBack.DAL;
using System.Linq;
using System.Threading.Tasks;

namespace P512FiorelloBack.ViewComponents
{
    public class FlowerViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        public FlowerViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var flowers =await _context.Flowers.Include(f => f.FlowerCategories).ThenInclude(c => c.Category).ToListAsync();

            return View(flowers);
        }
    }

}
    
