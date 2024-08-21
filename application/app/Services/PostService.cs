using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core.domain.Entities;
using core.domain.Interfaces.Repositories;

namespace application.app.Services
{
    public class PostService
    {
        private readonly InterfacePostRepository<Post, string> _postRepository;
        private readonly FollowerService _followerService;

        public PostService(InterfacePostRepository<Post, string> postRepository, FollowerService followerService)
        {
            _postRepository = postRepository;
            _followerService = followerService;
        }

        public Post GetById(int id)
        {
            return _postRepository.GetById(id);
        }

        public Post Post(Post entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("El post no puede ser nulo.");
            }

            return _postRepository.post(entity);
        }

        public IEnumerable<Post> GetAll()
        {
            return _postRepository.GetAll();
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            return await _postRepository.GetByIdAsync(id);
        }

        public async Task<Post> PostAsync(Post entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("El post no puede ser nulo.");
            }

            return await _postRepository.PostAsync(entity);
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _postRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsFromFollowedUsersAsync(string username)
        {
            var followedUsers = await _followerService.GetFollowedUsersAsync(username);
            if (followedUsers == null)
            {
                followedUsers = Enumerable.Empty<string>();
            }
            return await _postRepository.GetPostsByAuthorsAsync(followedUsers);
        }

        public async Task<IEnumerable<Post>> GetPostsByAuthorsAsync(IEnumerable<string> authorUsernames)
        {
            return await _postRepository.GetPostsByAuthorsAsync(authorUsernames);
        }
    }
}
