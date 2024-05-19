using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebAppAPI.Models
{
	public class Users : IdentityUser
	{
		public int Id { get; set; }
		public string Email { get; set; }

	}
}