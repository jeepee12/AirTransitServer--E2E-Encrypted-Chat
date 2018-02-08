using AirTransitServer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AirTransitServer.Controllers
{
    [Route("api/[controller]")]
    public class RegistryController : Controller
    {
        private readonly RegistryContext _ctx;

        public RegistryController(RegistryContext context)
        {
            _ctx = context;

            // todo delete this
            if (!_ctx.Registries.Any())
            {
                _ctx.Registries.Add(new Registry { PhoneNumber = "12345", PublicKey = "myPKey"});
                _ctx.SaveChanges();
            }
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Registry> Get()
        {
            return _ctx.Registries.ToList();
        }

        // GET api/<controller>/5
        [HttpGet("{phoneNumber}", Name = "GetPublicKey")]
        public IActionResult Get(string phoneNumber)
        {
            var item = _ctx.Registries.FirstOrDefault(r => r.PhoneNumber == phoneNumber);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Registry registry)
        {
            if (registry == null)
            {
                return BadRequest();
            }

            _ctx.Registries.Add(registry);
            _ctx.SaveChanges();

            return CreatedAtRoute("GetPublicKey", new { phoneNumber = registry.PhoneNumber }, registry);
        }

        // PUT api/<controller>/5
        [HttpPut("{phoneNumber}")]
        public IActionResult Update(string phoneNumber, [FromBody] Registry registry)
        {
            if (registry == null || registry.PublicKey != phoneNumber)
            {
                return BadRequest();
            }

            var r = _ctx.Registries.FirstOrDefault(t => t.PhoneNumber == phoneNumber);
            if (r == null)
            {
                return NotFound();
            }

            r.PublicKey = registry.PublicKey;

            _ctx.Registries.Update(r);
            _ctx.SaveChanges();
            return new NoContentResult();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{phoneNumber}")]
        public IActionResult Delete(string phoneNumber)
        {
            var registry = _ctx.Registries.FirstOrDefault(t => t.PhoneNumber == phoneNumber);
            if (registry == null)
            {
                return NotFound();
            }

            _ctx.Registries.Remove(registry);
            _ctx.SaveChanges();
            return new NoContentResult();
        }
    }
}
