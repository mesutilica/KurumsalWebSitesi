using KurumsalWebSitesi.Core.Entities;
using KurumsalWebSitesi.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KurumsalWebSitesi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlidersController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public SlidersController(DatabaseContext context)
        {
            _context = context;
        }
        // GET: api/<SlidersController>
        [HttpGet]
        public IEnumerable<Slider> Get()
        {
            return _context.Sliders;
        }

        // GET api/<SlidersController>/5
        [HttpGet("{id}")]
        public Slider Get(int id)
        {
            return _context.Sliders.Find(id);
        }

        // POST api/<SlidersController>
        [HttpPost]
        public void Post([FromBody] Slider value)
        {
            _context.Sliders.Add(value);
            _context.SaveChanges();
        }

        // PUT api/<SlidersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Slider value)
        {
            _context.Sliders.Update(value);
            _context.SaveChanges();
        }

        // DELETE api/<SlidersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var value = _context.Sliders.Find(id);
            _context.Sliders.Remove(value);
            _context.SaveChanges();
        }
    }
}
