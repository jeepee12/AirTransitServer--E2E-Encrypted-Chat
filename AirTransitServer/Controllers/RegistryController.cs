using AirTransitServer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AirTransitServer.Controllers
{
    [Route("api/[controller]")]
    public class RegistryController : Controller
    {
        private readonly RegistryContext _ctx;

        public RegistryController(RegistryContext context)
        {
            _ctx = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Registry> Get()
        {
            return _ctx.Registries.ToList();
        }

        // GET api/<controller>/<phoneNumber>
        [HttpGet("{phoneNumber}", Name = "GetPublicKey")]
        public IActionResult Get(string phoneNumber)
        {
            var item = _ctx.Registries.FirstOrDefault(r => r.PhoneNumber == phoneNumber);

            if (item == null)
                return NotFound();

            return new ObjectResult(item);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Registry registry)
        {
            if (registry == null)
                return BadRequest();

            _ctx.Registries.Add(registry);
            _ctx.SaveChanges();

            return CreatedAtRoute("GetPublicKey", new { phoneNumber = registry.PhoneNumber }, registry);
        }

        // PUT api/<controller>/<phoneNumber>
        [HttpPut("{phoneNumber}")]
        public IActionResult Update(string phoneNumber, [FromBody] Registry registry)
        {
            if (registry == null || registry.PublicKey != phoneNumber)
                return BadRequest();

            var reg = _ctx.Registries.FirstOrDefault(t => t.PhoneNumber == phoneNumber);

            if (reg == null)
                return NotFound();

            reg.PublicKey = registry.PublicKey;

            _ctx.Registries.Update(reg);
            _ctx.SaveChanges();

            return new NoContentResult();
        }

        // DELETE api/<controller>/<phoneNumber>
        [HttpDelete("{phoneNumber}")]
        public IActionResult Delete(string phoneNumber)
        {
            var registry = _ctx.Registries.FirstOrDefault(t => t.PhoneNumber == phoneNumber);

            if (registry == null)
                return NotFound();

            _ctx.Registries.Remove(registry);
            _ctx.SaveChanges();
            return new NoContentResult();
        }
    }
}
