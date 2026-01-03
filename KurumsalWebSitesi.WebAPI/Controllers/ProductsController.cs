using KurumsalWebSitesi.Core.Entities;
using KurumsalWebSitesi.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KurumsalWebSitesi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ProductsController(DatabaseContext context)
        {
            _context = context;
        }
        // GET: api/<ProductsController>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _context.Products;
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            return _context.Products.Find(id);
        }

        // POST api/<ProductsController>
        [HttpPost]
        public void Post([FromBody] Product value)
        {
            _context.Products.Add(value);
            _context.SaveChanges();
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Product value)
        {
            _context.Products.Update(value);
            _context.SaveChanges();
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var kayit = Get(id);
            _context.Products.Remove(kayit);
            _context.SaveChanges();
        }
    }
}
