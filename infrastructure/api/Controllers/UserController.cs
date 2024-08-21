using Microsoft.AspNetCore.Mvc;
using core.domain.Entities;
using application.app.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using infrastructure.data.Contexts;
using infrastructure.data.Repositories;
using System;

namespace infrastructure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserService CreateService()
        {
            var dbContext = new SocialNetworkContext();
            var userRepository = new UserRepository(dbContext);
            var userService = new UserService(userRepository);

            return userService;
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<User>> GetUser(string username)
        {
            var service = CreateService();
            var user = await service.GetUserByUsernameAsync(username);
            if (user == null)
            {
                return NotFound("El usuario no existe.");
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            var service = CreateService();
            var userExists = await service.UserExistsAsync(user.Username);
            if (userExists)
            {
                return Conflict("El usuario ya existe.");
            }

            await service.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetUser), new { username = user.Username }, user);
        }
    }
}
