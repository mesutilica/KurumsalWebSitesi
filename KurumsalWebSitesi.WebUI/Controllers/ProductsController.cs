using KurumsalWebSitesi.Data;
using Microsoft.AspNetCore.Mvc;

namespace KurumsalWebSitesi.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DatabaseContext _context;

        public ProductsController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index(string q = "")
        {
            return View(_context.Products.Where(p => p.IsActive && p.Name.Contains(q)));
        }

        public IActionResult Details(int? id)
        {
            if (id is null)
            {
                return BadRequest("Geçersiz İstek!");
            }
            var model = _context.Products.FirstOrDefault(p => p.Id == id && p.IsActive);
            if (model == null)
            {
                return NotFound("Kayıt Bulunamadı!");
            }
            return View(model);
        }
    }
}
