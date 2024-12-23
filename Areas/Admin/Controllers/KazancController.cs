using Microsoft.AspNetCore.Mvc;

namespace Proje.Areas.Admin.Controllers
{
    public class KazancController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
