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
                Text = $"{u.Ad} - {u.Fiyat}₺",
                Value = u.ID.ToString()
            }).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RandevuAl(DateTime randevuTarihi, string randevuSaati, int uzmanlikId)
        {
            var userId = User.Identity?.Name;
            var kullaniciId = _context.Users.Where(u => u.UserName == userId).Select(u => u.Id).FirstOrDefault();

            if (string.IsNullOrEmpty(kullaniciId))
            {
                ModelState.AddModelError("", "Geçersiz kullanıcı kimliği.");
                ViewBag.UzmanlikAlanlari = _context.UzmanlikAlanlari.Select(u => new SelectListItem
                {
                    Text = u.Ad,
                    Value = u.ID.ToString()
                }).ToList();
                return View();
            }

            if (ModelState.IsValid)
            {
                if (!TimeSpan.TryParse(randevuSaati, out var saatDilimi))
                {
                    ModelState.AddModelError("", "Geçersiz saat formatı.");
                    return View();
                }

                var randevuZamani = randevuTarihi.Date.Add(saatDilimi);

                if (randevuZamani <= DateTime.Now)
                {
                    ModelState.AddModelError("", "Geçmiş bir tarihe randevu oluşturamazsınız. Lütfen ileri bir tarih ve saat seçin.");
                    return View();
                }

                var uzmanlik = await _context.UzmanlikAlanlari.FirstOrDefaultAsync(u => u.ID == uzmanlikId);
                if (uzmanlik == null)
                {
                    ModelState.AddModelError("", "Geçersiz uzmanlık alanı seçildi.");
                    return View();
                }

                var toplamFiyat = uzmanlik.Fiyat;

                var calisan = await _context.CalisanUzmanlik
                    .Include(cu => cu.Calisan)
                    .Where(cu => cu.UzmanlikID == uzmanlikId)
                    .Select(cu => cu.Calisan)
                    .FirstOrDefaultAsync();
                if (calisan == null)
                {
                    ModelState.AddModelError("", "Bu uzmanlık alanına atanmış bir çalışan bulunmamaktadır.");
                    return View();
                }

                var mevcutRandevular = _context.Randevular
                    .Where(r => r.CalisanID == calisan.ID && r.RandevuTarihi.Date == randevuTarihi.Date)
                    .ToList();

                if (mevcutRandevular.Any(r => r.RandevuTarihi.TimeOfDay == saatDilimi))
                {
                    ModelState.AddModelError("", "Seçilen saat dolu. Lütfen başka bir saat seçin.");
                    return View();
                }

                var yeniRandevu = new Randevu
                {
                    MusteriID = kullaniciId,
                    CalisanID = calisan.ID,
                    UzmanlikID = uzmanlikId,
                    RandevuTarihi = randevuZamani,
                    ToplamFiyat = toplamFiyat,
                    Durum = "Onay Bekliyor"
                };

                try
                {
                    _context.Randevular.Add(yeniRandevu);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Randevunuz başarıyla oluşturuldu!";
                    return RedirectToAction("RandevuListele");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Randevu oluşturulurken bir hata oluştu: {ex.InnerException?.Message ?? ex.Message}");
                    return View();
                }
            }

            ViewBag.UzmanlikAlanlari = _context.UzmanlikAlanlari.Select(u => new SelectListItem
            {
                Text = u.Ad,
                Value = u.ID.ToString()
            }).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IptalEt(int id)
        {
            var userId = User.Identity.Name;
            var randevu = await _context.Randevular.FirstOrDefaultAsync(r => r.ID == id && r.MusteriID == userId);
            if (randevu == null)
            {
                return NotFound();
            }

            _context.Randevular.Remove(randevu);
            await _context.SaveChangesAsync();
            return RedirectToAction("RandevuListele");
        }

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
