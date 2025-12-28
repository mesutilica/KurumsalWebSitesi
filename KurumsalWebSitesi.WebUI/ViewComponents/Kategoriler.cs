using KurumsalWebSitesi.Data;
using Microsoft.AspNetCore.Mvc;

namespace KurumsalWebSitesi.WebUI.ViewComponents
{
    public class Kategoriler : ViewComponent
    {
        private readonly DatabaseContext _context;

        public Kategoriler(DatabaseContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            return View(_context.Categories.Where(c => c.IsActive));
        }
    }
}
