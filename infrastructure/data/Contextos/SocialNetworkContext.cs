using core.domain.Entities;
using infrastructure.data.Configs;
using Microsoft.EntityFrameworkCore;

public class SocialNetworkContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Follower> Followers { get; set; }

    public SocialNetworkContext(DbContextOptions<SocialNetworkContext> options)
        : base(options)
    {
    }

    public SocialNetworkContext()
    {
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new UserConfig());
        builder.ApplyConfiguration(new PostConfig());
        builder.ApplyConfiguration(new FollowersConfig());
    }
}
