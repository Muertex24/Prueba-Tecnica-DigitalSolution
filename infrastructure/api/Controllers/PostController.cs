using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using core.domain.Entities;
using application.app.Services;
using System;

namespace infrastructure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly PostService _postService;
        private readonly UserService _userService;

        public PostController(PostService postService, UserService userService)
        {
            _postService = postService;
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> Get(int id)
        {
            var post = await _postService.GetByIdAsync(id);
            if (post == null)
            {
                return NotFound("Post no encontrado");
            }
            return Ok(post);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] PostDto postDto)
        {
            if (postDto == null)
            {
                return BadRequest("El objeto de publicación no puede ser nulo.");
            }

            if (string.IsNullOrEmpty(postDto.Content))
            {
                return BadRequest("El contenido de la publicación no puede estar vacío.");
            }

            var userExists = await _userService.UserExistsAsync(postDto.AuthorUsername);

            if (!userExists)
            {
                return BadRequest("El autor especificado no existe.");
            }

            try
            {
                var post = new Post
                {
                    AuthorUsername = postDto.AuthorUsername,
                    Content = postDto.Content,
                    DatePosted = DateTime.Now,
                };

                var newPost = await _postService.PostAsync(post);

                return Ok(new
                {
                    Message = "¡Publicación agregada satisfactoriamente!",
                    PostContent = newPost.Content,
                    PostID = newPost.postID,
                    DatePosted = newPost.DatePosted
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error al agregar la publicación: {ex.Message}");
            }
        }

        [HttpGet("GetAllPost")]
        public async Task<ActionResult<IEnumerable<Post>>> Get()
        {
            var posts = await _postService.GetAllAsync();
            return Ok(posts);
        }

        [HttpGet("by-author/{authorUsername}")]
        public async Task<ActionResult<IEnumerable<Post>>> GetPostsByAuthor(string authorUsername)
        {
            var posts = await _postService.GetPostsByAuthorsAsync(new[] { authorUsername });
            return Ok(posts);
        }

        [HttpGet("by-following/{username}")]
        public async Task<ActionResult<IEnumerable<Post>>> GetPostsByFollowing(string username)
        {
            var posts = await _postService.GetPostsFromFollowedUsersAsync(username);
            return Ok(posts);
        }
    }
}
