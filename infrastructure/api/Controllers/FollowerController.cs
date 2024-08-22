using Microsoft.AspNetCore.Mvc;
using core.domain.Entities;
using application.app.Services;
using System;
using System.Threading.Tasks;

namespace infrastructure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowerController : ControllerBase
    {
        private readonly FollowerService _followerService;

        public FollowerController(FollowerService followerService)
        {
            _followerService = followerService;
        }

        [HttpPost("follow")]
        public async Task<IActionResult> Follow([FromBody] Follower follower)
        {
            try
            {
                await _followerService.FollowAsync(follower);
                return Ok("Seguido exitosamente. ✔️");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message} ❌");
            }
        }

        [HttpPost("unfollow")]
        public async Task<IActionResult> Unfollow([FromBody] Follower follower)
        {
            try
            {
                await _followerService.UnfollowAsync(follower);
                return Ok("Dejado de seguir exitosamente. ✔️");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message} ❌");
            }
        }

        [HttpGet("followers/{username}")]
        public async Task<IActionResult> GetFollowersAsync(string username)
        {
            try
            {
                var followers = await _followerService.GetFollowersAsync(username);
                return Ok(followers);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message} ❌");
            }
        }

        [HttpGet("followed/{username}")]
        public async Task<IActionResult> GetFollowedUsersAsync(string username)
        {
            try
            {
                var followedUsers = await _followerService.GetFollowedUsersAsync(username);
                return Ok(followedUsers);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message} ❌");
            }
        }
    }
}
