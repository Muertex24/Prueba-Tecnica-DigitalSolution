using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core.domain.Entities;
using core.domain.Interfaces.Repositories;
using core.domain.Interfaces;

namespace application.app.Services
{
    public class FollowerService
    {
        private readonly FollowerInterface<Follower> _followerRepository;
        private readonly InterfaceUserRepository _userRepository;

        public FollowerService(FollowerInterface<Follower> followerRepository, InterfaceUserRepository userRepository)
        {
            _followerRepository = followerRepository;
            _userRepository = userRepository;
        }

        public async Task FollowAsync(Follower follower)
        {
            if (follower == null || string.IsNullOrEmpty(follower.FollowerUsername) || string.IsNullOrEmpty(follower.FollowedUsername))
            {
                throw new ArgumentException("Datos del seguidor o seguido inválidos.");
            }

            // Verifica si el usuario que se quiere seguir existe en la base de datos
            var userExists = await _userRepository.UserExistsAsync(follower.FollowedUsername);
            if (!userExists)
            {
                throw new ArgumentException("El usuario que se quiere seguir no está registrado.");
            }

            // Verifica si ya existe la relación de seguimiento
            var followerExists = await _followerRepository.FollowerExistsAsync(follower.FollowerUsername, follower.FollowedUsername);
            if (followerExists)
            {
                throw new InvalidOperationException("Ya sigues a este usuario.");
            }

            await _followerRepository.AddAsync(follower);
        }

        public async Task UnfollowAsync(Follower follower)
        {
            if (follower == null || string.IsNullOrEmpty(follower.FollowerUsername) || string.IsNullOrEmpty(follower.FollowedUsername))
            {
                throw new ArgumentException("Datos del seguidor o seguido inválidos.");
            }

            await _followerRepository.RemoveAsync(follower);
        }

        public async Task<IEnumerable<string>> GetFollowersAsync(string username)
        {
            var followers = await _followerRepository.GetFollowersAsync(username);
            var followerUsernames = followers.Select(f => f.FollowerUsername);
            return followerUsernames;
        }

        public async Task<IEnumerable<string>> GetFollowedUsersAsync(string username)
        {
            var followedUsers = await _followerRepository.GetFollowedUsersAsync(username);
            return followedUsers.Select(f => f.FollowedUsername).Distinct();
        }
    }
}
