using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proje.Data;
using Proje.Models;
using System.Linq;

namespace Proje.Controllers
{
    public class RandevuController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RandevuController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var randevular = _context.Randevular
                .Include(x => x.musteri)
                .Include(x => x.calisan)
                .Include(x => x.uzmanlik)
                .Select(x => new
                {
                    x.ID,
                    MusteriAd = x.musteri.ad,
                    MusteriSoyad = x.musteri.soyad,
                    CalisanAd = x.calisan.calisan_ad,
                    CalisanSoyad = x.calisan.calisan_soyad,
                    Uzmanlik = x.uzmanlik.ad,
                    x.randevu_tarih
                })
                .ToList();

            return View(randevular);
        }

        public IActionResult RandevuDetay(int? id)
        {
            if (id == null)
            {
                TempData["error"] = "Lütfen randevu ID bilgisini giriniz.";
                return RedirectToAction("Index");
            }

            var randevu = _context.Randevular
                .Include(x => x.musteri)
                .Include(x => x.calisan)
                .Include(x => x.uzmanlik)
                .FirstOrDefault(x => x.ID == id);

            if (randevu == null)
            {
                TempData["error"] = "Geçerli bir randevu bulunamadı.";
                return RedirectToAction("Index");
            }

            return View(randevu);
        }

        public IActionResult Create()
        {
            ViewData["Calisanlar"] = new SelectList(_context.Calisanlar, "CalisanID", "CalisanAdSoyad");
            ViewData["UzmanlikAlanlari"] = new SelectList(_context.UzmanlikAlanlari, "ID", "Ad");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Randevular randevu)
        {
            if (ModelState.IsValid)
            {
                var existingRandevu = _context.Randevular
                    .FirstOrDefault(x => x.calisan_ID == randevu.calisan_ID && x.randevu_tarih == randevu.randevu_tarih);

                if (existingRandevu != null)
                {
                    TempData["error"] = "Bu çalışan için aynı tarihte başka bir randevu mevcut.";
                    return View(randevu);
                }

                _context.Randevular.Add(randevu);
                _context.SaveChanges();

                TempData["msj"] = "Randevu başarıyla oluşturuldu.";
                return RedirectToAction("Index");
            }

            TempData["error"] = "Randevu oluşturulurken bir hata oluştu.";
            return View(randevu);
        }
    }
}
