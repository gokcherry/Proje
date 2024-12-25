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
    [Authorize(Roles = "Admin")]
    public class RandevuController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RandevuController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Listele()
        {
            var randevular = await _context.Randevular
                .Include(r => r.Musteri)
                .Include(r => r.Calisan)
                .Include(r => r.Uzmanlik)
                .ToListAsync();

            return View(randevular);
        }
        public IActionResult Ekle()
        {
            ViewBag.Musteriler = new SelectList(_context.Musteri.ToList(), "Id", "Ad");
            ViewBag.Calisanlar = new SelectList(_context.Calisan.ToList(), "Id", "Ad");
            ViewBag.UzmanlikAlanlari = new SelectList(_context.UzmanlikAlanlari.ToList(), "Id", "Ad");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ekle(Randevu randevu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(randevu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Listele));
            }
            ViewData["Musteriler"] = new SelectList(_context.Musteri, "ID", "Ad");
            ViewData["Calisanlar"] = new SelectList(_context.Calisan, "ID", "Ad");
            ViewData["UzmanlikAlanlari"] = new SelectList(_context.UzmanlikAlanlari, "ID", "Ad");

            return View(randevu);
        }
        public async Task<IActionResult> Guncelle(int? id)
        {
            if (id == null)
                return NotFound("Randevu ID gerekli.");

            var randevu = await _context.Randevular
                .Include(r => r.Musteri)
                .Include(r => r.Calisan)
                .Include(r => r.Uzmanlik)
                .FirstOrDefaultAsync(r => r.ID == id);

            if (randevu == null)
                return NotFound("Randevu bulunamadı.");

            ViewData["Musteriler"] = new SelectList(_context.Musteri, "ID", "Ad", randevu.MusteriID);
            ViewData["Calisanlar"] = new SelectList(_context.Calisan, "ID", "Ad", randevu.CalisanID);
            ViewData["UzmanlikAlanlari"] = new SelectList(_context.UzmanlikAlanlari, "ID", "Ad", randevu.UzmanlikID);

            return View(randevu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Guncelle(int id, Randevu randevu)
        {
            if (id != randevu.ID)
                return NotFound("Randevu ID eşleşmiyor.");

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
                        return NotFound("Randevu bulunamadı.");

                    throw;
                }
                return RedirectToAction(nameof(Listele));
            }

            ViewData["Musteriler"] = new SelectList(_context.Musteri, "ID", "Ad", randevu.MusteriID);
            ViewData["Calisanlar"] = new SelectList(_context.Calisan, "ID", "Ad", randevu.CalisanID);
            ViewData["UzmanlikAlanlari"] = new SelectList(_context.UzmanlikAlanlari, "ID", "Ad", randevu.UzmanlikID);

            return View(randevu);
        }

        public async Task<IActionResult> Sil(int? id)
        {
            if (id == null)
                return NotFound("Randevu ID gerekli.");

            var randevu = await _context.Randevular
                .Include(r => r.Musteri)
                .FirstOrDefaultAsync(r => r.ID == id);

            if (randevu == null)
                return NotFound("Randevu bulunamadı.");

            return View(randevu);
        }

        [HttpPost, ActionName("Sil")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SilOnayla(int id)
        {
            var randevu = await _context.Randevular.FindAsync(id);

            if (randevu == null)
                return NotFound("Randevu bulunamadı.");

            _context.Randevular.Remove(randevu);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Listele));
        }
        private bool RandevuExists(int id)
        {
            return _context.Randevular.Any(e => e.ID == id);
        }
    }
}
