using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using core.domain.Entities;
using infrastructure.data.Configs;


namespace infrastructure.data.Contexts
{
    public class SocialNetworkContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Follower> Followers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySql("Server=yourServer;Database=YourDataBase;User=yourUser;Password=yourPassword;", 
                new MySqlServerVersion(new Version(8, 0, 21)),
                mySqlOptions => mySqlOptions.EnableRetryOnFailure());
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserConfig());
            builder.ApplyConfiguration(new PostConfig());
            builder.ApplyConfiguration(new FollowersConfig());
        }
    }
}
