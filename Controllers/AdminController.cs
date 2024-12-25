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
        public IActionResult RandevuGuncelle(int id)
        {
            return RedirectToAction("Guncelle", "Randevu", new { id });
        }
        public IActionResult RandevuSil(int id)
        {
            return RedirectToAction("Sil", "Randevu", new { id });
        }
        public IActionResult Calisanlar()
        {
            return RedirectToAction("Listele", "Calisan");
        }
        public IActionResult CalisanEkle()
        {
            return RedirectToAction("Ekle", "Calisan");
        }
        public IActionResult CalisanDuzenle(int id)
        {
            return RedirectToAction("Guncelle", "Calisan", new { id });
        }
        public IActionResult CalisanSil(int id)
        {
            return RedirectToAction("Sil", "Calisan", new { id });
        }
        public IActionResult MusteriListele()
        {
            return RedirectToAction("Listele", "Musteri");
        }
        public IActionResult MusteriEkle()
        {
            return RedirectToAction("Ekle", "Musteri");
        }
        public IActionResult MusteriDuzenle(int id)
        {
            return RedirectToAction("Guncelle", "Musteri", new { id });
        }
        public IActionResult MusteriSil(int id)
        {
            return RedirectToAction("Sil", "Musteri", new { id });
        }
        [AllowAnonymous]
        public IActionResult YardimSayfasi()
        {
            return View();
        }
    }
}
