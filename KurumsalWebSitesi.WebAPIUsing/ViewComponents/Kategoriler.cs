using KurumsalWebSitesi.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace KurumsalWebSitesi.WebUI.ViewComponents
{
    public class Kategoriler : ViewComponent
    {
        string _apiAdres = "https://localhost:7179/api/categories/";
        private readonly HttpClient _httpClient;

        public Kategoriler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdres);
            return View(model);
        }
    }
}
