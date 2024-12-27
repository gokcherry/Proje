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
    public class CalisanController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CalisanController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Hata(string mesaj)
        {
            ViewData["HataMesaji"] = mesaj;
            return View();
        }

        public async Task<IActionResult> Listele()
        {
            var calisanlar = await _context.Calisan
                .Include(c => c.UzmanlikAlanlari)
                    .ThenInclude(cu => cu.Uzmanlik)
                .ToListAsync();

            return View(calisanlar);
        }
        public async Task<IActionResult> Detay(int? id)
        {
            if (id == null)
                return BadRequest("Geçersiz çalışan ID.");

            var calisan = await _context.Calisan
                .Include(c => c.UzmanlikAlanlari)
                    .ThenInclude(cu => cu.Uzmanlik)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (calisan == null)
                return NotFound("Çalışan bulunamadı.");

            return View(calisan);
        }
        public IActionResult Ekle()
        {
            ViewData["UzmanlikAlanlari"] = new MultiSelectList(_context.UzmanlikAlanlari, "ID", "Ad");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ekle([Bind("Ad,Soyad,Email,Telefon")] Calisanlar calisan, int[] UzmanlikAlanlari)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(calisan);
                    await _context.SaveChangesAsync();

                    foreach (var uzmanlikId in UzmanlikAlanlari)
                    {
                        var calisanUzmanlik = new CalisanUzmanlik
                        {
                            CalisanID = calisan.ID,
                            UzmanlikID = uzmanlikId
                        };
                        _context.Add(calisanUzmanlik);
                    }

                    await _context.SaveChangesAsync();
                    TempData["msj"] = "Çalışan başarıyla eklendi.";
                    return RedirectToAction(nameof(Listele));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Ekleme sırasında bir hata oluştu: {ex.Message}");
                }
            }
            ViewData["UzmanlikAlanlari"] = new MultiSelectList(_context.UzmanlikAlanlari, "ID", "Ad");
            return View(calisan);
        }
        public async Task<IActionResult> Guncelle(int? id)
        {
            if (id == null)
                return BadRequest("Geçersiz çalışan ID.");

            var calisan = await _context.Calisan
                .Include(c => c.UzmanlikAlanlari)
                .FirstOrDefaultAsync(c => c.ID == id);

            if (calisan == null)
                return NotFound("Çalışan bulunamadı.");

            ViewData["UzmanlikAlanlari"] = new MultiSelectList(_context.UzmanlikAlanlari, "ID", "Ad");
            return View(calisan);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Guncelle(int id, [Bind("ID,Ad,Soyad,Email,Telefon")] Calisanlar calisan, int[] UzmanlikAlanlari)
        {
            if (id != calisan.ID)
                return BadRequest("ID uyuşmazlığı.");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calisan);
                    await _context.SaveChangesAsync();

                    var mevcutUzmanliklar = _context.CalisanUzmanlik.Where(cu => cu.CalisanID == calisan.ID);
                    _context.CalisanUzmanlik.RemoveRange(mevcutUzmanliklar);
                    await _context.SaveChangesAsync();

                    foreach (var uzmanlikId in UzmanlikAlanlari)
                    {
                        var calisanUzmanlik = new CalisanUzmanlik
                        {
                            CalisanID = calisan.ID,
                            UzmanlikID = uzmanlikId
                        };
                        _context.Add(calisanUzmanlik);
                    }

                    await _context.SaveChangesAsync();
                    TempData["msj"] = "Çalışan başarıyla güncellendi.";
                    return RedirectToAction(nameof(Listele));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalisanVarMi(calisan.ID))
                        return NotFound("Çalışan bulunamadı.");
                    throw;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Güncelleme sırasında bir hata oluştu: {ex.Message}");
                }
            }
            ViewData["UzmanlikAlanlari"] = new MultiSelectList(_context.UzmanlikAlanlari, "ID", "Ad");
            return View(calisan);
        }
        [HttpGet]
        public IActionResult Sil(int? id)
        {
            if (id == null)
            {
                return BadRequest("Geçersiz ID.");
            }

            var calisan = _context.Calisan
                .Include(c => c.UzmanlikAlanlari)
                .ThenInclude(ua => ua.Uzmanlik)
                .FirstOrDefault(c => c.ID == id);

            if (calisan == null)
            {
                return NotFound("Çalışan bulunamadı.");
            }

            return View(calisan);
        }
        [HttpPost]
        public IActionResult Sil(int id)
        {
            var calisan = _context.Calisan
                .Include(c => c.UzmanlikAlanlari)
                .FirstOrDefault(c => c.ID == id);

            if (calisan == null)
            {
                return NotFound("Çalışan bulunamadı.");
            }
            if (calisan.Randevular != null && calisan.Randevular.Any())
            {
                TempData["Hata"] = "Çalışanın aktif randevuları var. Önce randevuları kaldırın.";
                return RedirectToAction("Sil", new { id });
            }

            // Çalışanın uzmanlık alanlarını sil
            var uzmanliklar = _context.CalisanUzmanlik.Where(cu => cu.CalisanID == calisan.ID);
            _context.CalisanUzmanlik.RemoveRange(uzmanliklar);

            // Çalışanı sil
            _context.Calisan.Remove(calisan);
            _context.SaveChanges();

            TempData["msj"] = $"{calisan.Ad} {calisan.Soyad} isimli çalışan başarıyla silindi.";
            return RedirectToAction("Listele");
        }
        private bool CalisanVarMi(int id)
        {
            return _context.Calisan.Any(e => e.ID == id);
        }
    }
}
