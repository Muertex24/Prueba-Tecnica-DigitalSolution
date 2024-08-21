using System;

namespace core.domain.Entities
{
    public class Follower
    {
        public string FollowerUsername { get; set; }
        public string FollowedUsername { get; set; }

        public Follower(string followerUsername, string followedUsername)
        {
            if (string.IsNullOrEmpty(followerUsername))
                throw new ArgumentException("El nombre de usuario del seguidor no puede estar vacío.", nameof(followerUsername));

            if (string.IsNullOrEmpty(followedUsername))
                throw new ArgumentException("El nombre de usuario seguido no puede estar vacío.", nameof(followedUsername));

            FollowerUsername = followerUsername;
            FollowedUsername = followedUsername;
        }

    }
}
