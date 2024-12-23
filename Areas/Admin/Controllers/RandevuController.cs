using Microsoft.AspNetCore.Mvc;

namespace Proje.Areas.Admin.Controllers
{
    public class RandevuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
