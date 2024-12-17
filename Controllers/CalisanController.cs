using Proje.Data;
using Proje.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Proje.Controllers
{
    public class CalisanController:Controller
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
            if (id is null)
            {
                TempData["error"] = "Lütfen çalışan ID bilgisini giriniz.";
                return View("Error");
            }
            var c = _context.Calisanlar
                .Include(x => x.UzmanlikAlanlari)
                .FirstOrDefault(x => x.ID == id);
            if (c == null)
            {
                TempData["error"] = "lütfen geçerli bir çalışan ID giriniz.";
                return View("Error");
            }

            return View(c);

        }

        public IActionResult CalisanEkle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CalisanEkle(Calisanlar c)
        {
            if(ModelState.IsValid)
            {
                _context.Calisanlar.Add(c);
                _context.SaveChanges();

                TempData["msj"] = c.calisan_ad +" "+ c.calisan_soyad+ " adlı çalışan eklendi.";
                return RedirectToAction("Index");
            }

            TempData["msj"] = "Ekleme işlemi başarısız.";
            return View(c);
        }

        public IActionResult CalisanDuzenle(int? id)
        {
            if(id is null)
            {
                TempData["error"] = "Düzenleme için ID gerekli. Lütfen boş geçmeyiniz";
                return View("CalisanHata");
            }

            var c= _context.Calisanlar.FirstOrDefault(x => x.ID == id);

            if(c==null)
            {
                TempData["error"] = "Düzenleme yapmak için geçerli bir ID gerekli. Lütfen kontrol ediniz.";
                return View("CalisanHata");
            }
            return View(c);
        }

        [HttpPost]
        public IActionResult CalisanDuzenle(int? id, Calisanlar c)
        {
            if(id !=c.ID)
            {
                TempData["error"] = "ID bulunamadı.";
                return View("CalisanHata");
            }
            if (ModelState.IsValid)
            {
                _context.Calisanlar.Update(c);
                _context.SaveChanges();

                TempData["msj"] = c.calisan_ad + "" + c.calisan_soyad + "adlı çalışanın bilgileri güncellendi.";
                return RedirectToAction("Index");
            }

            TempData["error"] = "Lütfen tüm alanları doldurun.";
            return View("CalisanHata");
        }

        public IActionResult CalisanSil(int? id)
        {
            if(id is null)
            {
                TempData["error"] = "Silme işlemini gerçekleştirmek için ID gerekli. Boş geçmeyiniz.";
                return View("CalisanHata");
            }

            var c = _context.Calisanlar.Include(x => x.UzmanlikAlanlari).FirstOrDefault(x => x.ID == id);

            if(c == null)
            {
                TempData["error"] = "Çalışan sistemde mevcut değil.";
                return View("CalisanHata");
            }

            if(c.Randevular.Count>0)
            {
                TempData["error"] = "Çalışanın randevuları mevcut.";
                return View("CalisanHata");
            }

            _context.Calisanlar.Remove(c);
            _context.SaveChanges();
            TempData["msj"] = c.calisan_ad + " " + c.calisan_soyad + " isimli çalışan sistemden silindi.";
            return RedirectToAction("Index");
        }

    }
}
