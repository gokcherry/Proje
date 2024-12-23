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
            public IActionResult Giris()
            {
                return View();
            }

            [HttpPost]
        public async Task<IActionResult> Giris(string email, string password)
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

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                // Şifre sıfırlama token'ı oluşturma
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                // Şifre sıfırlama linkini e-posta ile gönderin (e-posta gönderme kodunu buraya ekleyin)
                TempData["info"] = "Şifre sıfırlama bağlantısı e-posta adresinize gönderildi.";
            }
            else
            {
                TempData["error"] = "Bu e-posta adresine ait bir hesap bulunamadı.";
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
            public async Task<IActionResult> Cıkıs()
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Login");
            }
        }
    
}
