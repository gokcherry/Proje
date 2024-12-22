using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proje.Models;
using Proje.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
namespace Proje.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            var randevular = _context.Randevular
                .Include(r => r.Musteri)
                .Include(r => r.Calisan)
                .Include(r => r.Uzmanlik)
                .Select(r => new
                {
                    r.ID,
                    MusteriAdi = r.Musteri.Ad,
                    MusteriSoyadi = r.Musteri.Soyad,
                    PersonelAdiSoyadi = $"{r.Calisan.Ad} {r.Calisan.Soyad}",
                    Islem = r.Uzmanlik.Ad,
                    RandevuTarihi = r.RandevuTarihi.ToString("yyyy-MM-dd"),
                    RandevuSaati = r.RandevuTarihi.ToString("HH:mm"),
                    RandevuDurumu = r.Durum
                })
                .ToList();

            return View(randevular);
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



