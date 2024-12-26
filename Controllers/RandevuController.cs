using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proje.Data;
using Proje.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Proje.Controllers
{
    public class RandevuController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RandevuController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> RandevuListele()
        {
            var userId = User.Identity.Name;
            var randevular = await _context.Randevular
                .Include(r => r.Uzmanlik)
                .Include(r => r.Calisan)
                .Where(r => r.MusteriID == userId)
                .ToListAsync();
            return View(randevular);
        }

        [HttpGet]
        public IActionResult RandevuAl()
        {
            ViewBag.UzmanlikAlanlari = _context.UzmanlikAlanlari.Select(u => new SelectListItem
            {
                Text = u.Ad,
                Value = u.ID.ToString()

            }).ToList();
            return View();
        }

            [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RandevuAl(Randevu randevu, int[] UzmanlikAlanlari)
        {

            if (ModelState.IsValid)
            {
                var uzmanlik = await _context.UzmanlikAlanlari
                .FirstOrDefaultAsync(u => u.ID == randevu.UzmanlikID);
                if (uzmanlik == null)
                {
                    ModelState.AddModelError("", "Geçersiz uzmanlık alanı seçildi.");
                    return View(randevu);
                }
                var calisan = await _context.CalisanUzmanlik
                    .FirstOrDefaultAsync(c => c.CalisanID == randevu.UzmanlikID);
                if (calisan == null)
                {
                    ModelState.AddModelError("", "Bu uzmanlık alanına atanmış bir çalışan bulunmamaktadır.");
                    return View(randevu);
                }

                randevu.CalisanID = calisan.ID;

                // Çakışma kontrolü
                var mevcutRandevular = _context.Randevular
                    .Where(r => r.CalisanID == randevu.CalisanID &&
                                r.RandevuTarihi.Date == randevu.RandevuTarihi.Date)
                    .ToList();

                var yeniRandevuBaslangic = randevu.RandevuTarihi;
                var yeniRandevuBitis = randevu.RandevuTarihi.AddMinutes(uzmanlik.Sure);

                if (mevcutRandevular.Any(r =>
                    (yeniRandevuBaslangic >= r.RandevuTarihi && yeniRandevuBaslangic < r.RandevuTarihi.AddMinutes(uzmanlik.Sure)) ||
                    (yeniRandevuBitis > r.RandevuTarihi && yeniRandevuBitis <= r.RandevuTarihi.AddMinutes(uzmanlik.Sure))))
                {
                    ModelState.AddModelError("", "Seçilen saat dolu. Lütfen başka bir saat seçin.");
                    return View(randevu);
                }

                randevu.MusteriID = User.Identity.Name;
                randevu.Durum = "Onay Bekliyor";

                _context.Randevular.Add(randevu);
                await _context.SaveChangesAsync();

                return RedirectToAction("RandevuAl");
            }
            ViewBag.UzmanlikAlanlari = _context.UzmanlikAlanlari.Select(u => new SelectListItem
            {
                Text = u.Ad,
                Value = u.ID.ToString()

            }).ToList();
            return View(randevu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IptalEt(int id)
        {
            var randevu = await _context.Randevular.FindAsync(id);
            if (randevu == null || randevu.MusteriID != User.Identity.Name)
            {
                return NotFound();
            }

            _context.Randevular.Remove(randevu);
            await _context.SaveChangesAsync();
            return RedirectToAction("RandevuAl");

        }

        // Admin için Randevu Yönetimi
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminRandevular()
        {
            var randevular = await _context.Randevular
                .Include(r => r.Uzmanlik)
                .Include(r => r.Calisan)
                .Include(r => r.Musteri)
                .ToListAsync();
            return View(randevular);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Onayla(int id)
        {
            var randevu = await _context.Randevular.FindAsync(id);
            if (randevu == null)
            {
                return NotFound();
            }

            randevu.Durum = "Onaylandı";
            await _context.SaveChangesAsync();

            return RedirectToAction("AdminRandevular");
        }

    }
}
