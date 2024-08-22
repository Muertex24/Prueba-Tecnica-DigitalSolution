using Microsoft.AspNetCore.Mvc;
using core.domain.Entities;
using application.app.Services;
using System.Threading.Tasks;

namespace infrastructure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetUser/{username}")]
        public async Task<ActionResult<User>> GetUser(string username)
        {
            var user = await _userService.GetUserByUsernameAsync(username);
            if (user == null)
            {
                return NotFound("El usuario no existe. ❌");
            }
            return Ok(user);
        }

        [HttpPost("UserCreator")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            var userExists = await _userService.UserExistsAsync(user.Username);
            if (userExists)
            {
                return Conflict("El usuario ya existe. ❌");
            }

            await _userService.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetUser), new { username = user.Username }, user);
        }
    }
}
