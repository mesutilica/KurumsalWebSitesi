using KurumsalWebSitesi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KurumsalWebSitesi.WebUI.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly DatabaseContext _context;

        public CategoriesController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id Gereklidir!");
            }
            var model = _context.Categories.Include(x => x.Products.Where(p => p.IsActive)).FirstOrDefault(x => x.Id == id && x.IsActive);
            if (model == null)
            {
                return NotFound("Kayıt Bulunamadı!");
            }
            return View(model);
        }
    }
}
