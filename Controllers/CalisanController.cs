using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proje.Data;
using Proje.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Proje.Controllers
{
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
                return NotFound();

            var calisan = await _context.Calisan
                .Include(c => c.UzmanlikAlanlari)
                    .ThenInclude(cu => cu.Uzmanlik)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (calisan == null)
                return NotFound();

            return View(calisan);
        }

        public IActionResult Ekle()
        {
            ViewData["UzmanlikAlanlari"] = new SelectList(_context.UzmanlikAlanlari, "ID", "Ad");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ekle([Bind("Ad,Soyad,Email,Telefon")] Calisan calisan, int[] UzmanlikAlanlari)
        {
            if (ModelState.IsValid)
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

                return RedirectToAction(nameof(Listele));
            }

            ViewData["UzmanlikAlanlari"] = new SelectList(_context.UzmanlikAlanlari, "ID", "Ad");
            return View(calisan);
        }

        public async Task<IActionResult> Guncelle(int? id)
        {
            if (id == null)
                return NotFound();

            var calisan = await _context.Calisan
                .Include(c => c.UzmanlikAlanlari)
                .FirstOrDefaultAsync(c => c.ID == id);

            if (calisan == null)
                return NotFound();

            ViewData["UzmanlikAlanlari"] = new SelectList(_context.UzmanlikAlanlari, "ID", "Ad");
            return View(calisan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Guncelle(int id, [Bind("ID,Ad,Soyad,Email,Telefon")] Calisan calisan, int[] UzmanlikAlanlari)
        {
            if (id != calisan.ID)
                return NotFound();

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
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalisanVarMi(calisan.ID))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Listele));
            }

            ViewData["UzmanlikAlanlari"] = new SelectList(_context.UzmanlikAlanlari, "ID", "Ad");
            return View(calisan);
        }

        public IActionResult Sil(int? id)
        {
            if (id == null)
            {
                TempData["hata"] = "Silme işlemini gerçekleştirmek için ID gerekli. Boş geçmeyiniz.";
                return RedirectToAction("Hata", new { mesaj = "ID gerekli." });
            }

            var calisan = _context.Calisan
                .Include(c => c.UzmanlikAlanlari)
                .Include(c => c.Randevular)
                .FirstOrDefault(c => c.ID == id);

            if (calisan == null)
            {
                TempData["hata"] = "Silinmek istenen çalışan sistemde mevcut değil.";
                return RedirectToAction("Hata", new { mesaj = "Çalışan bulunamadı." });
            }

            if (calisan.Randevular != null && calisan.Randevular.Any())
            {
                TempData["hata"] = "Çalışanın randevuları mevcut. Silmeden önce ilişkili randevuları kaldırmanız gerekir.";
                return RedirectToAction("Hata", new { mesaj = "Çalışanın randevuları mevcut." });
            }

            var uzmanliklar = _context.CalisanUzmanlik.Where(cu => cu.CalisanID == calisan.ID);
            _context.CalisanUzmanlik.RemoveRange(uzmanliklar);

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
