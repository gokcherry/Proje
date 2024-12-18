using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proje.Models;
using Proje.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Proje.Controllers
{
    public class RandevuController: Controller
    {
        private readonly ApplicationDbContext _context;

        public RandevuController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var randevular = _context.Randevular.Include(x => x.musteri).Include(x => x.calisan).Include(x => x.uzmanlik)
                .Select(x => new
                {
                    x.ID,
                    MusteriAd = x.musteri.ad,
                    MusteriSoyad = x.musteri.soyad,
                    CalisanAd = x.calisan.calisan_ad,
                    CalisanSoyad = x.calisan.calisan_soyad,
                    Uzmanlik = x.uzmanlik.ad,
                    x.randevu_tarih
                }
                ).ToList();

            return View(randevular);
        }

        public IActionResult RandevuDetay(int? id)
        {
            if(id is null)
            {
                TempData["error"] = "Lütfen randevu ID bilgisini giriniz.";
                return View("RandevuHata");
            }

            var r = _context.Randevular.Include(x => x.musteri.ad).Include(x => x.musteri.soyad)
                .Include(x => x.calisan.calisan_ad).Include(x => x.calisan.calisan_soyad).Include(x => x.uzmanlik.ad)
                .FirstOrDefault(x => x.ID == id);

            if(r==null)
            {
                TempData["error"] = "Geçerli bir randevu bulunamadı.";
                return View("RandevuError");
            }

            return View(r);
        }

        public IActionResult Create()
        {
            //ViewData["Musteriler"] = new SelectList(_context.Musteriler, "MusteriID", "MusteriAd");
            ViewData["Calisanlar"] = new SelectList(_context.Calisanlar, "CalisanAd", "CalisanSoyad");
            ViewData["UzmanlikAlanlari"] = new SelectList(_context.UzmanlikAlanlari, "UzmanlikAlani");

            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(Randevular randevu)
        {
            if(ModelState.IsValid)
            {
                var musteriad = User.Identity.Name;
                randevu.musteri_ID = musteri;
                var eskirandevu = _context.Randevular.Where(x => x.calisan_ID == randevu.calisan_ID &&
                x.randevu_tarih == randevu.randevu_tarih).FirstOrDefault();

                if(eskirandevu!= null)
                {
                    TempData["error"] = "Bu çalışan için aynı tarihte başka bir randevu mecvut.";
                    return View(randevu);
                }

                _context.Randevular.Add(randevu);
                _context.SaveChanges();

                TempData["msj"] = "Randevu başarıyla oluşturuldu.";

                return RedirectToAction("Index");     
            }

        }

    }
}
