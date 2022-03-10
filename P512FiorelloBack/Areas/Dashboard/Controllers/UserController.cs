using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P512FiorelloBack.Areas.Dashboard.ViewModels;
using P512FiorelloBack.Constants;
using P512FiorelloBack.DAL;
using P512FiorelloBack.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P512FiorelloBack.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize(Roles = RoleConstants.Admin+", "+RoleConstants.Moderator)]
    public class UserController : Controller
    {
        private AppDbContext _dbContext;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<User> _userManager;

        public UserController(AppDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _dbContext.Users.ToListAsync();
            List<UserVM> userList = new List<UserVM>();

            foreach (var user in users)
            {
                userList.Add(new UserVM
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Fullname = user.Fullname,
                    Roles = string.Join(", ", await _userManager.GetRolesAsync(user))
                }) ;
            }

            return View(userList);
        }

        public async Task<IActionResult> GetRoles(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var roles = await _userManager.GetRolesAsync(user);

            ViewBag.Name = user.Fullname;
            ViewBag.UserId = user.Id;
            return View(roles);
        }

        public async Task<IActionResult> RemoveRole(string id, string roleName)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            await _userManager.RemoveFromRoleAsync(user, roleName);

            return RedirectToAction(nameof(GetRoles), new { user.Id });
        }

        public async Task<IActionResult> AddRole(string id)
        {
            var roles = await _dbContext.Roles.Select(r => r.Name).ToListAsync();

            AddRoleVM model = new AddRoleVM
            {
                UserId = id,
                Roles = roles
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(string id, AddRoleVM model)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            if (!ModelState.IsValid) return View(model);

            await _userManager.AddToRoleAsync(user, model.RoleName);

            return RedirectToAction(nameof(GetRoles), new { id });

        }

        public async Task<IActionResult> ChangePassword(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            ViewBag.FullName = user.Fullname;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(string id, ChangePasswordVM model)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            if (!ModelState.IsValid) return View();

            if (!await _userManager.CheckPasswordAsync(user, model.OldPassword))
            {
                ModelState.AddModelError(nameof(ChangePasswordVM.OldPassword), "Old Password is not correct");
                return View();
            }

            var idResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (!idResult.Succeeded)
            {
                foreach (var error in idResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            return RedirectToAction(nameof(Index));
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Block(string id)
        //{
        //    var user = await _userManager.FindByIdAsync(id);
        //    if (user == null) return NotFound();

        //    await _userManager.SetLockoutEnabledAsync(user, true);
            
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> UnBlock(string id)
        //{
        //    var user = await _userManager.FindByIdAsync(id);
        //    if (user == null) return NotFound();

        //    await _userManager.SetLockoutEnabledAsync(user, false);

        //    return View();
        //}
    }
}
