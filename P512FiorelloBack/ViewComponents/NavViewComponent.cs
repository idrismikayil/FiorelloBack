using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P512FiorelloBack.DAL;
using System.Threading.Tasks;

namespace P512FiorelloBack.ViewComponents
{
    public class NavViewComponent: ViewComponent
    {
        private readonly AppDbContext _context;
        public NavViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var layout = await _context.Layouts.FirstOrDefaultAsync();

            return View(layout);
        }
    }
}
