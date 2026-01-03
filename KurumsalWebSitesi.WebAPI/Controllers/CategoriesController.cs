using KurumsalWebSitesi.Core.Entities;
using KurumsalWebSitesi.Data;
using Microsoft.AspNetCore.Mvc;

namespace KurumsalWebSitesi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public CategoriesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return _context.Categories;
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public Category Get(int id)
        {
            return _context.Categories.Find(id);
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public void Post([FromBody] Category value)
        {
            _context.Categories.Add(value);
            _context.SaveChanges();
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Category value)
        {
            _context.Categories.Update(value);
            _context.SaveChanges();
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var category = _context.Categories.Find(id);
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}
