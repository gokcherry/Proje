using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Proje.Data;
using Proje.Models;
using Proje.ViewModels;
using System.Threading.Tasks;

namespace Proje.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<Kullanicilar> _userManager;
        private readonly SignInManager<Kullanicilar> _signInManager;

        public RegisterController(UserManager<Kullanicilar> userManager, SignInManager<Kullanicilar> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            { 
                var user = new Kullanicilar
                {
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.Telefon,
                    Ad = model.Ad,
                    Soyad = model.Soyad
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");

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
