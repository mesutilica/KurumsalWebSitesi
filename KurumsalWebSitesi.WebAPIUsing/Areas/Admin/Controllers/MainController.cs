using Microsoft.AspNetCore.Mvc;

namespace KurumsalWebSitesi.WebAPIUsing.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
