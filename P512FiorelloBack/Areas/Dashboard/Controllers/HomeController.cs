using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P512FiorelloBack.Constants;

namespace P512FiorelloBack.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize(Roles = RoleConstants.Admin)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
