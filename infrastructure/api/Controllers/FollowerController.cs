using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using core.domain.Entities;
using application.app.Services;
using infrastructure.data.Contexts;
using infrastructure.data.Repositories;
using System;
using System.Threading.Tasks;

namespace infrastructure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowerController : ControllerBase
    {
        private FollowerService CreateFollowerService()
        {
            var dbContext = new SocialNetworkContext();
            var followerRepository = new FollowerRepository(dbContext);
            var userRepository = new UserRepository(dbContext);
            var followerService = new FollowerService(followerRepository, userRepository);
            return followerService;
        }

        [HttpPost("follow")]
        public async Task<IActionResult> Follow([FromBody] Follower follower)
        {
            var service = CreateFollowerService();
            try
            {
                await service.FollowAsync(follower);
                return Ok("Seguido exitosamente.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPost("unfollow")]
        public async Task<IActionResult> Unfollow([FromBody] Follower follower)
        {
            var service = CreateFollowerService();
            try
            {
                await service.UnfollowAsync(follower);
                return Ok("Dejado de seguir exitosamente.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("followers/{username}")]
        public async Task<IActionResult> GetFollowersAsync(string username)
        {
            var service = CreateFollowerService();
            try
            {
                var followers = await service.GetFollowersAsync(username);
                return Ok(followers);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("followed/{username}")]
        public async Task<IActionResult> GetFollowedUsersAsync(string username)
        {
            var service = CreateFollowerService();
            try
            {
                var followedUsers = await service.GetFollowedUsersAsync(username);
                return Ok(followedUsers);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
