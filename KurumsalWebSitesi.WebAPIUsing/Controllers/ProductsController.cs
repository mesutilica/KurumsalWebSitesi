using KurumsalWebSitesi.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KurumsalWebSitesi.WebAPIUsing.Controllers
{
    public class ProductsController : Controller
    {
        string _apiAdres = "https://localhost:7179/api/";
        private readonly HttpClient _httpClient;

        public ProductsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> IndexAsync(string q = "")
        {
            var products = await _httpClient.GetFromJsonAsync<List<Product>>(_apiAdres + "Products");
            products = products.Where(p => p.IsActive && p.Name.Contains(q)).ToList();
            return View(products);
        }
        public async Task<IActionResult> DetailsAsync(int? id)
        {
            if (id is null)
            {
                return BadRequest("Geçersiz İstek!");
            }
            var model = await _httpClient.GetFromJsonAsync<Product>(_apiAdres + "Products/" + id);
            if (model == null)
            {
                return NotFound("Kayıt Bulunamadı!");
            }
            return View(model);
        }
    }
}
