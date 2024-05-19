using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebAppAPI.Models
{
	public class Message
	{

		public int Id {  get; set; }
		public string? Email { get; set; } 
		public string? Fullname { get; set; }
		public string? Company { get; set; }
		public decimal? Budget { get; set; }
		public string? MessageText { get; set; }

	}
}
