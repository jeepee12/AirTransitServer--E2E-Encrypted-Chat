using AirTransitServer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AirTransitServer.Controllers
{
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private readonly MessageContext _ctx;

        public MessageController(MessageContext context)
        {
            _ctx = context;
        }

        // GET api/<controller>/<senderPhoneNumber>
        // A signature must be specified in the body to confirm the identify of the sender.
        [HttpGet("{senderPhoneNumber}")]
        public IActionResult Get(string senderPhoneNumber, string authSignature)
        {
            if(CheckSignature(senderPhoneNumber, authSignature))
                return Ok(_ctx.Messages.Where(m => m.PhoneNumber == senderPhoneNumber));
            return NotFound();
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Message message)
        {
            if (message == null)
                return BadRequest();

            _ctx.Messages.Add(message);
            _ctx.SaveChanges();

            return Ok(message);
        }

        // DELETE api/<controller>/<messageId>
        [HttpDelete("{messageId}")]
        public IActionResult Delete(Guid messageId, string authSignature)
        {
            Message message = _ctx.Messages.FirstOrDefault(m => m.Id == messageId);

            if (message == null)
                return NotFound();

            if (CheckSignature(message.PhoneNumber, authSignature))
            {
                _ctx.Messages.Remove(message);
                _ctx.SaveChanges();
                return NoContent();
            }

            return Forbid();
        }

        private bool CheckSignature(string phoneNumber, string signature)
        {
            // todo check signature here
            return true;
        }
    }
}
