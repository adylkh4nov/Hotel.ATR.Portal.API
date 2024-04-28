using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppAPI.Data;
using WebAppAPI.Models;

namespace WebAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : Controller
    {
        private Context _context;

        public RoomController(Context context)
        {
            _context = context;
        }
        public IEnumerable<Room> Get()
        {
            var data = _context.Rooms;
            return data;
        }
        [HttpGet("{id}")]
        public Room Get(int Id)
        {
            var data = _context.Rooms.FirstOrDefault(x=>x.Id == Id);
            return data;

        }
        [HttpPost]
        public Room Post(Room room)
        {
            _context.Rooms.Add(room);
            _context.SaveChanges();
            return room;
        }
        [HttpPut]
        public StatusCodeResult Put([FromBody] Room room)
        {
            var data = _context.Rooms.FirstOrDefault(x => x.Id.Equals(room.Id));

            if (data != null)
            {
                data.Price = room.Price;
                data.Name = room.Name;
                data.Descriotion = room.Descriotion;
                data.PathToImage = room.PathToImage;

                _context.SaveChanges();
                return Ok();

            }
            else return NotFound();

           
        }
        [HttpDelete("{id}")]
        public void Delete(int Id)
        {
            var data = _context.Rooms.FirstOrDefault(x => x.Id == Id);
            _context.Rooms.Remove(data);
            _context.SaveChanges();
        }
    }
}
