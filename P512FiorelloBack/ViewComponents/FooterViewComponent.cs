using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P512FiorelloBack.DAL;
using System.Linq;
using System.Threading.Tasks;

namespace P512FiorelloBack.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
    private readonly AppDbContext _context;
    public FooterViewComponent(AppDbContext context)
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
