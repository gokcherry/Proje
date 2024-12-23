using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proje.Data;
using Proje.Models;
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
                    RandevuDurumu = r.Durum,
                    ToplamFiyat = r.ToplamFiyat.ToString()
                })
                .ToList();

            return View(randevular);
        }
        public async Task<IActionResult> Guncelle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevular
                .Include(r => r.Musteri)
                .Include(r => r.Calisan)
                .Include(r => r.Uzmanlik)
                .FirstOrDefaultAsync(r => r.ID == id);

            if (randevu == null)
            {
                return NotFound();
            }

            // DropDownList'ler için gerekli verileri ViewBag ile gönderiyoruz
            ViewBag.Musteriler = new SelectList(_context.Musteriler, "ID", "Ad", randevu.MusteriID);
            ViewBag.Calisanlar = new SelectList(_context.Calisanlar, "ID", "Ad", randevu.CalisanID);
            ViewBag.Uzmanliklar = new SelectList(_context.UzmanlikAlanlari, "ID", "Ad", randevu.UzmanlikID);

            return View(randevu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Guncelle(int id, [Bind("ID,MusteriID,CalisanID,UzmanlikID,RandevuTarihi,ToplamFiyat,Durum")] Randevu randevu)
        {
            if (id != randevu.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(randevu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RandevuExists(randevu.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(RandevuListele));
            }

            // DropDownList'ler için gerekli verileri ViewBag ile gönderiyoruz
            ViewBag.Musteriler = new SelectList(_context.Musteriler, "ID", "Ad", randevu.MusteriID);
            ViewBag.Calisanlar = new SelectList(_context.Calisanlar, "ID", "Ad", randevu.CalisanID);
            ViewBag.Uzmanliklar = new SelectList(_context.UzmanlikAlanlari, "ID", "Ad", randevu.UzmanlikID);

            return View(randevu);
        }

        // 3. Randevu Silme
        public async Task<IActionResult> Sil(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var randevu = _context.Randevular
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
            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }

        [HttpPost, ActionName("Sil")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var randevu = await _context.Randevular.FindAsync(id);
            _context.Randevular.Remove(randevu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RandevuExists(int id)
        {
            return _context.Randevular.Any(e => e.ID == id);
        }
    }
}
