using Proje.Data;
using Proje.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Proje.Controllers
{
    public class CalisanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CalisanController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var calisanlar = _context.Calisanlar.ToList();
            return View(calisanlar);
        }

        public IActionResult CalisanDetay(int? id)
        {
            if (id == null)
            {
                TempData["error"] = "Lütfen çalışan ID bilgisini giriniz.";
                return View("Error");
            }

            var calisan = _context.Calisanlar
                .Include(x => x.UzmanlikAlanlari)
                .FirstOrDefault(x => x.ID == id);

            if (calisan == null)
            {
                TempData["error"] = "Geçerli bir çalışan ID bulunamadı.";
                return View("Error");
            }

            return View(calisan);
        }

       public IActionResult CalisanEkle()
        {
            return View();
        }

        // POST: Calisan/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CalisanEkle(Calisanlar calisan)
        {
            // Model doğrulaması
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Lütfen tüm alanları doğru şekilde doldurduğunuzdan emin olun.";
                return View(calisan);
            }

            try
            {
                calisan.katilim_tarih = DateTime.Now;

                _context.Calisanlar.Add(calisan);
                await _context.SaveChangesAsync();

                TempData["msj"] = $"{calisan.calisan_ad} {calisan.calisan_soyad} adlı çalışan başarıyla eklendi.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Çalışan eklenemedi. Hata: {ex.Message}";
                return View(calisan);
            }
        }
        public IActionResult CalisanDuzenle(int? id)
        {
            if (id == null)
            {
                TempData["error"] = "Düzenleme için ID gerekli. Lütfen boş geçmeyiniz.";
                return View("Error");
            }

            var calisan = _context.Calisanlar.FirstOrDefault(x => x.ID == id);

            if (calisan == null)
            {
                TempData["error"] = "Geçerli bir çalışan ID bulunamadı.";
                return View("Error");
            }

            return View(calisan);
        }

        [HttpPost]
        public IActionResult CalisanDuzenle(int? id, Calisanlar calisan)
        {
            if (id != calisan.ID)
            {
                TempData["error"] = "ID bulunamadı.";
                return View("Error");
            }

            if (ModelState.IsValid)
            {
                _context.Calisanlar.Update(calisan);
                _context.SaveChanges();

                TempData["msj"] = $"{calisan.calisan_ad} {calisan.calisan_soyad} adlı çalışanın bilgileri güncellendi.";
                return RedirectToAction("Index");
            }

            TempData["error"] = "Lütfen tüm alanları doldurun.";
            return View(calisan);
        }

        public IActionResult CalisanSil(int? id)
        {
            if (id == null)
            {
                TempData["error"] = "Silme işlemini gerçekleştirmek için ID gerekli. Boş geçmeyiniz.";
                return View("Error");
            }

            var calisan = _context.Calisanlar
                .Include(x => x.UzmanlikAlanlari)
                .Include(x => x.Randevular)
                .FirstOrDefault(x => x.ID == id);

            if (calisan == null)
            {
                TempData["error"] = "Çalışan sistemde mevcut değil.";
                return View("Error");
            }

            if (calisan.Randevular.Any())
            {
                TempData["error"] = "Çalışanın randevuları mevcut.";
                return View("Error");
            }

            _context.Calisanlar.Remove(calisan);
            _context.SaveChanges();

            TempData["msj"] = $"{calisan.calisan_ad} {calisan.calisan_soyad} isimli çalışan sistemden silindi.";
            return RedirectToAction("Index");
        }
    }
}
