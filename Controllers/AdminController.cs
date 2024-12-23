using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proje.Data;

namespace Proje.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RandevuListele()
        {
            return RedirectToAction("RandevuListele", "Randevu");
        }
        public IActionResult RandevuGuncelle()
        {
            return RedirectToAction("Guncelle", "Randevu");
        }
        public IActionResult RandevuSil()
        {
            return RedirectToAction("Sil", "Randevu");
        }

        public IActionResult Calisanlar()
        {
            return RedirectToAction("Listele", "Calisan");
        }

        // Çalışan Ekleme
        public IActionResult CalisanEkle()
        {
            return RedirectToAction("Ekle", "Calisan");
        }

        // Çalışan Düzenleme
        public IActionResult CalisanDuzenle(int id)
        {
            return RedirectToAction("Guncelle", "Calisan", new { id });
        }

        // Çalışan Silme
        public IActionResult CalisanSil(int id)
        {
            return RedirectToAction("Sil", "Calisan", new { id });
        }
        [AllowAnonymous]
        public IActionResult YardimSayfasi()
        {
            return View();
        }

    }
}

