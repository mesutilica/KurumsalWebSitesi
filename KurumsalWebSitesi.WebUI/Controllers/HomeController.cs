using System.Diagnostics;
using KurumsalWebSitesi.Data;
using KurumsalWebSitesi.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KurumsalWebSitesi.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext _context;

        public HomeController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new HomePageViewModel
            {
                Products = _context.Products.Where(p => p.IsActive && p.IsHome),
                Sliders = _context.Sliders
            };
            return View(model);
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
