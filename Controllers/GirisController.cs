using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Proje.Controllers
{
    public class GirisController : Controller
    {
            private readonly SignInManager<IdentityUser> _signInManager;
            private readonly UserManager<IdentityUser> _userManager;

            public GirisController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
            {
                _signInManager = signInManager;
                _userManager = userManager;
            }

            [HttpGet]
            public IActionResult Login()
            {
                return View();
            }

            [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                TempData["error"] = "E-posta ve şifre gereklidir.";
                return View();
            }

            var user = await _userManager.FindByEmailAsync(email); // E-posta ile kullanıcıyı bul
            if (user == null)
            {
                TempData["error"] = "Geçersiz giriş bilgileri.";
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(user, password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user);

                if (roles.Contains("Admin"))
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["error"] = "Geçersiz giriş bilgileri.";
                return View();
            }
        }


        [HttpPost]
            public async Task<IActionResult> Logout()
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Login");
            }
        }
    
}
