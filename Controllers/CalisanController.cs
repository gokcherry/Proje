using Proje.Data;
using Proje.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            return View();
        }

        public IActionResult CalisanDetay(int? id)
        {
            if (id == null)
            {
                TempData["error"] = "Lütfen çalışan ID bilgisini giriniz.";
                return View("Error");
            }

            var calisan = _context.Calisanlar
                .Include(x => x.uzmanlik)
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
              ViewBag.UzmanlikAlanlari = _context.UzmanlikAlanlari.Select(u => new SelectListItem
            {
                Value = u.ID.ToString(),
                Text = u.ad
            }).ToList();

            return View();
        }

        // POST: Calisan/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CalisanEkle(Calisanlar calisan)
        {
            if (ModelState.IsValid)
            {
                // Seçilen uzmanlık ID'si ile ilişkilendirme
                var uzmanlik = await _context.UzmanlikAlanlari
                    .FirstOrDefaultAsync(u => u.ID == calisan.uzmanlik_ID);

                if (uzmanlik != null)
                {
                    // Çalışanı eklerken uzmanlık bilgisini ilişkilendiriyoruz
                    calisan.uzmanlik = uzmanlik;

                    _context.Calisanlar.Add(calisan);
                    await _context.SaveChangesAsync();

                    TempData["msj"] = $"{calisan.calisan_ad} {calisan.calisan_soyad} adlı çalışan başarıyla eklendi.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "Seçilen uzmanlık alanı geçerli değil.";
                    ViewBag.UzmanlikAlanlari = _context.UzmanlikAlanlari
                        .Select(u => new SelectListItem
                        {
                            Value = u.ID.ToString(),
                            Text = u.ad
                        }).ToList();
                    return View(calisan);
                }
            }

            TempData["error"] = "Formda hata var. Lütfen tüm alanları doğru şekilde doldurduğunuzdan emin olun.";
            ViewBag.UzmanlikAlanlari = _context.UzmanlikAlanlari
                .Select(u => new SelectListItem
                {
                    Value = u.ID.ToString(),
                    Text = u.ad
                }).ToList();
            return View(calisan);
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
                .Include(x => x.uzmanlik)
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
