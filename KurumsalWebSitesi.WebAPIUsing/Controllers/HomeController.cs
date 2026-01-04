using KurumsalWebSitesi.Core.Entities;
using KurumsalWebSitesi.WebAPIUsing.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KurumsalWebSitesi.WebAPIUsing.Controllers
{
    public class HomeController : Controller
    {
        string _apiAdres = "https://localhost:7179/api/";
        private readonly HttpClient _httpClient;

        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var products = await _httpClient.GetFromJsonAsync<List<Product>>(_apiAdres + "Products");
            var sliders = await _httpClient.GetFromJsonAsync<List<Slider>>(_apiAdres + "Sliders");

            var model = new HomePageViewModel
            {
                Products = products.Where(p => p.IsActive && p.IsHome),
                Sliders = sliders
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
