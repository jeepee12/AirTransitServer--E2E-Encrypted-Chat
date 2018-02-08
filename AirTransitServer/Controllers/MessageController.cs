using System;
using AirTransitServer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AirTransitServer.Controllers
{
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private readonly MessageContext _ctx;

        public MessageController(MessageContext context)
        {
            _ctx = context;

            // todo delete this
            if (!_ctx.Messages.Any())
            {
                _ctx.Messages.Add(new Message { PhoneNumber = "12345", Content = "Encrypted Stuff"});
                _ctx.SaveChanges();
            }
        }

        // GET api/<controller>/5
        [HttpGet("{senderPhoneNumber}")]
        public IActionResult Get(string senderPhoneNumber, string authSignature)
        {
            if(CheckSignature(senderPhoneNumber, authSignature))
                return Ok(_ctx.Messages.Where(m => m.PhoneNumber == senderPhoneNumber));
            return NotFound();
            //return Enumerable.Empty<Message>();
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Message message)
        {
            if (message == null)
            {
                return BadRequest();
            }

            _ctx.Messages.Add(message);
            _ctx.SaveChanges();

            return Ok(message);
        }

        // DELETE api/<controller>/5
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

            return NotFound();
        }

        private bool CheckSignature(string phoneNumber, string signature)
        {
            // todo check signature here
            return true;
        }
    }
}
