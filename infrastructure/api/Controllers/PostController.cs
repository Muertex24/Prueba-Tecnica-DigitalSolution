using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using core.domain.Entities;
using application.app.Services;
using infrastructure.data.Repositories;
using infrastructure.data.Contexts;
using System;

namespace infrastructure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private PostService CreateService()
        {
            var dbContext = new SocialNetworkContext();
            var postRepository = new PostRepository(dbContext);
            var userRepository = new UserRepository(dbContext);
            var followerRepository = new FollowerRepository(dbContext);
            var followerService = new FollowerService(followerRepository, userRepository);
            var postService = new PostService(postRepository, followerService);

            return postService;
        }

        private UserService CreateUserService()
        {
            var dbContext = new SocialNetworkContext();
            var userRepository = new UserRepository(dbContext);
            var userService = new UserService(userRepository);

            return userService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> Get(int id)
        {
            var service = CreateService();
            var post = await service.GetByIdAsync(id);

            if (post == null)
            {
                return NotFound("Post no encontrado");
            }

            return Ok(post);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] PostDto postdto)
        {
            if (postdto == null)
            {
                return BadRequest("El objeto de publicación no puede ser nulo.");
            }

            if (string.IsNullOrEmpty(postdto.Content))
            {
                return BadRequest("El contenido de la publicación no puede estar vacío.");
            }

            var userService = CreateUserService();
            var userExists = await userService.UserExistsAsync(postdto.AuthorUsername);

            if (!userExists)
            {
                return BadRequest("El autor especificado no existe.");
            }

            try
            {
                var post = new Post
                {
                    AuthorUsername = postdto.AuthorUsername,
                    Content = postdto.Content,
                    DatePosted = DateTime.Now,
                };
                var service = CreateService();
                var newPost = await service.PostAsync(post);

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> Get()
        {
            var service = CreateService();
            var posts = await service.GetAllAsync();
            return Ok(posts);
        }

        [HttpGet("by-author/{authorUsername}")]
        public async Task<ActionResult<IEnumerable<Post>>> GetPostsByAuthor(string authorUsername)
        {
            var service = CreateService();
            var posts = await service.GetPostsByAuthorsAsync(new[] { authorUsername });
            return Ok(posts);
        }

        [HttpGet("by-following/{username}")]
        public async Task<ActionResult<IEnumerable<Post>>> GetPostsByFollowing(string username)
        {
            var service = CreateService();
            var posts = await service.GetPostsFromFollowedUsersAsync(username);
            return Ok(posts);
        }
    }
}
