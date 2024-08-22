using core.domain.Entities;
using core.domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace infrastructure.data.Repositories
{
    public class FollowerRepository : FollowerInterface<Follower>
    {
        private readonly SocialNetworkContext _context;

        public FollowerRepository(SocialNetworkContext context)
        {
            _context = context;
        }

        public void Add(Follower follower)
        {
            _context.Followers.Add(follower);
            _context.SaveChanges();
        }

        public void Remove(Follower follower)
        {
            var existingFollower = _context.Followers
                .Find(follower.FollowerUsername, follower.FollowedUsername);
            if (existingFollower != null)
            {
                _context.Followers.Remove(existingFollower);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Follower> GetFollowers(string followedUsername)
        {
            return _context.Followers
                .Where(f => f.FollowedUsername == followedUsername)
                .ToList();
        }

        public IEnumerable<Follower> GetFollowedUsers(string followerUsername)
        {
            return _context.Followers
                .Where(f => f.FollowerUsername == followerUsername)
                .ToList();
        }

        public async Task AddAsync(Follower follower)
        {
            await _context.Followers.AddAsync(follower);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Follower follower)
        {
            var existingFollower = await _context.Followers
                .FindAsync(follower.FollowerUsername, follower.FollowedUsername);
            if (existingFollower != null)
            {
                _context.Followers.Remove(existingFollower);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Follower>> GetFollowersAsync(string followedUsername)
        {
            return await _context.Followers
                .Where(f => f.FollowedUsername == followedUsername)
                .ToListAsync();
        }

        public async Task<IEnumerable<Follower>> GetFollowedUsersAsync(string followerUsername)
        {
            return await _context.Followers
                .Where(f => f.FollowerUsername == followerUsername)
                .ToListAsync();
        }

        public async Task<bool> FollowerExistsAsync(string followerUsername, string followedUsername)
        {
            return await _context.Followers
                .AnyAsync(f => f.FollowerUsername == followerUsername && f.FollowedUsername == followedUsername);
        }
    }
}
