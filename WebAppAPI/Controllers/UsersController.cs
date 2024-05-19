using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using WebAppAPI.Models;
using Context = WebAppAPI.Data.Context;

using Users = WebAppAPI.Models.Users;

namespace WebAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Context _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<Users> _userManager;
        public UsersController(Context context, IConfiguration configuration, UserManager<Users> userManager)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var user = await _userManager.FindByNameAsync(login.Username);

            if (user != null && await _userManager.CheckPasswordAsync(user, login.Password))
            {
                var token = JwtTokenGenerator.GenerateJwtToken(user.UserName, "SECRET_KEY_SECRET_KEY_SECRET_KEY_");

                // Возвращаем токен вместе с ответом
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                return BadRequest("User already exists.");
            }

            Users user = new Users()
            {
                UserName = model.Username,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var token = JwtTokenGenerator.GenerateJwtToken(user.UserName, "SECRET_KEY_SECRET_KEY_SECRET_KEY_");

                // Возвращаем токен вместе с ответом
                return Ok(new { Message = "User created successfully!", Token = token });
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
        


    }
}
