using KurumsalWebSitesi.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace KurumsalWebSitesi.WebAPIUsing.Controllers
{
    public class CategoriesController : Controller
    {
        string _apiAdres = "https://localhost:7179/api/";
        private readonly HttpClient _httpClient;

        public CategoriesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> IndexAsync(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id Gereklidir!");
            }
            var category = await _httpClient.GetFromJsonAsync<Category>(_apiAdres + "Categories/" + id);
            if (category == null)
            {
                return NotFound("Kayıt Bulunamadı!");
            }
            var products = await _httpClient.GetFromJsonAsync<List<Product>>(_apiAdres + "Products");
            category.Products = products.Where(p => p.IsActive && p.CategoryId == id).ToList();
            return View(category);
        }
    }
}
