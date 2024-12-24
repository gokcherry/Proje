using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Proje.Data;
using Proje.Models;
using System.Drawing;

namespace Proje.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public RegisterController(ApplicationDbContext context,UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;

            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Musteri model)
        {
            if (ModelState.IsValid)
            {
                // Yeni kullanıcıyı oluşturuyoruz
                var customer = new Musteri
                {
                    Ad = model.Ad,
                    Soyad = model.Soyad,
                    Telefon = model.Telefon,
                    Email = model.Email,
                    Sifre = model.Sifre // Şifreyi burada şifreli hale getirmeliyiz
                };

                // Şifreyi hash'leyelim
                var passwordHasher = new PasswordHasher<Musteri>();
                customer.Sifre = passwordHasher.HashPassword(customer, customer.Sifre);

                // Veritabanına kaydetme işlemi
                _context.Musteriler.Add(customer);
                await _context.SaveChangesAsync();

                // Kullanıcı kaydını başarılı şekilde tamamladıktan sonra giriş işlemi
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Sifre);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home"); // Ana sayfaya yönlendir
                }

                // Hata durumunda
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);

        }
    }
}
