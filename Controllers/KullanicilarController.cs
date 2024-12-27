using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proje.Data;
using Proje.Models;

namespace Proje.Controllers
{
    [Authorize(Roles = "Admin")]
    public class KullanicilarController : Controller
    {
        private readonly ApplicationDbContext _context;
        public KullanicilarController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Listele()
        {
            var kullanicilar = await _context.Musteri.ToListAsync();
            return View(kullanicilar);
        }
        public IActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ekle(Kullanicilar kullanici)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kullanici);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Listele));
            }
            return View(kullanici);
        }
        public async Task<IActionResult> Guncelle(string id)
        {
            if (id == null)
            {
                return NotFound("Geçersiz ID.");
            }

            var kullanici = await _context.Musteri.FindAsync(id);
            if (kullanici == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            return View(kullanici);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Guncelle(string id, Kullanicilar kullanici)
        {
            if (id != kullanici.Id)
            {
                return BadRequest("ID uyuşmazlığı.");
            }

            var mevcutKullanici = await _context.Musteri.FindAsync(id);
            if (mevcutKullanici == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }
            mevcutKullanici.Ad = kullanici.Ad;
            mevcutKullanici.Soyad = kullanici.Soyad;
            mevcutKullanici.Email = kullanici.Email;
            mevcutKullanici.Telefon = kullanici.Telefon;
            try
            {
                _context.Update(mevcutKullanici);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Musteri.Any(e => e.Id == id))
                {
                    return NotFound("Kullanıcı veritabanında mevcut değil.");
                }
                throw;
            }

            TempData["Message"] = "Kullanıcı başarıyla güncellendi.";
            return RedirectToAction(nameof(Listele));
        }
        public async Task<IActionResult> Sil(string? id)
        {
            if (id == null)
                return NotFound();

            var kullanici = await _context.Musteri.FindAsync(id);
            if (kullanici == null)
                return NotFound();

            return View(kullanici);
        }
        [HttpPost, ActionName("Sil")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SilOnayla(string id)
        {
            var kullanici = await _context.Musteri.FindAsync(id);
            if (kullanici == null)
                return NotFound();

            _context.Musteri.Remove(kullanici);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Listele));
        }
    }
}
