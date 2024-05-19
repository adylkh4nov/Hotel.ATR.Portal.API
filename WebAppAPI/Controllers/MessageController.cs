

using Microsoft.AspNetCore.Mvc;
using WebAppAPI.Models;
using WebAppAPI.Data;

namespace WebAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        Context _context;

        public MessageController(Context context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable<Message> Get()
        {
            var data = _context.Messages;
            return data;
        }
        [HttpGet("{Id}")]
        public Message Get(int Id)
        {
			var data = _context.Messages.FirstOrDefault(x => x.Id == Id);
            return data;

        }
        [HttpPost]
        public Message Post(Message message)
        {
            _context.Messages.Add(message);
            _context.SaveChanges();
            return message;
        }
        [HttpPut]
        public StatusCodeResult Put([FromBody] Message msg)
        {
            var data = _context.Messages.FirstOrDefault(x => x.Id.Equals(msg.Id));

            if (data != null)
            {
                data.Email = msg.Email;
                data.Fullname = msg.Fullname;
                data.MessageText = msg.MessageText;
                data.Budget = msg.Budget;
                data.Company = msg.Company;
                data.MessageText = msg.MessageText;
                _context.SaveChanges();
                return Ok();

            }

            else return NotFound();
        }
        [HttpDelete("{Id}")]
        public void Delete(int Id)
        {
            var data = _context.Messages.FirstOrDefault(x => x.Id == Id);
            _context.Messages.Remove(data);
            _context.SaveChanges();
        }
    }
}
