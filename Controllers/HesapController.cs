using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Proje.Models;
using Proje.ViewModels;
using System.Threading.Tasks;

namespace Proje.Controllers
{
    public class HesapController : Controller
    {
        private readonly SignInManager<Kullanicilar> _signInManager;
        private readonly UserManager<Kullanicilar> _userManager;

        public HesapController(SignInManager<Kullanicilar> signInManager, UserManager<Kullanicilar> userManager)
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
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, result.IsLockedOut
                    ? "Hesabınız kilitlendi. Lütfen daha sonra tekrar deneyin."
                    : "Geçersiz giriş denemesi.");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Hesap");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "E-posta adresi sistemde bulunamadı.");
                    return View(model);
                }
                var removePasswordResult = await _userManager.RemovePasswordAsync(user);
                if (removePasswordResult.Succeeded)
                {
                    var addPasswordResult = await _userManager.AddPasswordAsync(user, model.Password);
                    if (addPasswordResult.Succeeded)
                    {
                        TempData["SuccessMessage"] = "Şifreniz başarıyla sıfırlandı. Yeni şifrenizle giriş yapabilirsiniz.";
                        return RedirectToAction("Login", "Hesap");
                    }
                    foreach (var error in addPasswordResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                else
                {
                    foreach (var error in removePasswordResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Kullanicilar
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Telefon = model.Telefon,
                    Ad = model.Ad,
                    Soyad = model.Soyad
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // Add user to "User" role
                    var roleResult = await _userManager.AddToRoleAsync(user, "User");
                    if (!roleResult.Succeeded)
                    {
                        foreach (var error in roleResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, $"Role Error: {error.Description}");
                        }
                        return View(model);
                    }

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

    }
}
