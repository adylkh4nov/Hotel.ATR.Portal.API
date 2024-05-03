using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppAPI.Data;
using WebAppAPI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        [HttpGet("{id}")]
        public Message Get(int Id)
        {
            var data = _context.Messages.FirstOrDefault(x => x.id == Id);
            return data;

        }
        [HttpPost]
        public Message Post(Message message)
        {
            _context.Messages.Add(message);
            _context.SaveChanges();
            return message;
        }
        public StatusCodeResult Put([FromBody] Message msg)
        {
            var data = _context.Messages.FirstOrDefault(x => x.id.Equals(msg.id));

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
        [HttpDelete("{id}")]
        public void Delete(int Id)
        {
            var data = _context.Messages.FirstOrDefault(x => x.id == Id);
            _context.Messages.Remove(data);
            _context.SaveChanges();
        }
    }
}
