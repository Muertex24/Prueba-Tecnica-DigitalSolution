using System;
using System.Collections.Generic;
using System.Text;
using core.domain.Entities;
using System.Threading.Tasks;

namespace core.domain.Interfaces {
	public interface FollowerInterface<Entity> {
        void Add(Follower follower);
        void Remove(Follower follower);
        IEnumerable<Follower> GetFollowers(string followedUsername);
        IEnumerable<Follower> GetFollowedUsers(string followerUsername);
        Task AddAsync(Follower follower);
        Task RemoveAsync(Follower follower);
        Task<IEnumerable<Follower>> GetFollowersAsync(string followedUsername);
        Task<IEnumerable<Follower>> GetFollowedUsersAsync(string followerUsername);
        Task<bool> FollowerExistsAsync(string followerUsername, string followedUsername);
        
	}
}
