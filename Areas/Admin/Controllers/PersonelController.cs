using Microsoft.AspNetCore.Mvc;

namespace Proje.Areas.Admin.Controllers
{
    public class PersonelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
