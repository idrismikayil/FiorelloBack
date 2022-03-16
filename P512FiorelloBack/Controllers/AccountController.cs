using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using P512FiorelloBack.Constants;
using P512FiorelloBack.Models;
using P512FiorelloBack.Services;
using P512FiorelloBack.ViewModels;
using System.Threading.Tasks;

namespace P512FiorelloBack.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMailService _mailService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IMailService mailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mailService = mailService;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid) return View();
            var user = await _userManager.FindByNameAsync(model.Login);
            if (user == null) user = await _userManager.FindByEmailAsync(model.Login);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid credentials");
                return View();
            }

            if (!user.isActive)
            {
                ModelState.AddModelError("", "You are blocked");
                return View();
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Invalid credentials");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if(!ModelState.IsValid) return View();
            var user = await _userManager.FindByNameAsync(model.Username);
            if(user != null)
            {
                ModelState.AddModelError("Username", "There is already user with this username");
                return View();
            }

            User newUser = new User
            {
                Fullname = model.Fullname,
                Age = model.Age,
                Email = model.Email,
                UserName = model.Username,
                Position = model.Position

            };

            IdentityResult idtResult = await _userManager.CreateAsync(newUser, model.Password);

            if (!idtResult.Succeeded)
            {
                foreach (var error in idtResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            await _userManager.AddToRoleAsync(newUser, RoleConstants.User);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            var link = Url.Action(nameof(ConfirmEmail), "Account", new { newUser.UserName, token }, Request.Scheme);

            await _mailService.SendEmailAsync(new MailRequest
            {
                ToEmail = newUser.Email,
                Subject = "Complete registration",
                Body = link
            });

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ConfirmEmail(string username, string token)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return BadRequest();

            var identityResult = await _userManager.ConfirmEmailAsync(user, token);
            if (!identityResult.Succeeded)
            {
                return BadRequest();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
