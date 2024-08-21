using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using core.domain.Entities;

namespace infrastructure.data.Configs
{
    public class FollowersConfig : IEntityTypeConfiguration<Follower>
    {
        public void Configure(EntityTypeBuilder<Follower> builder)
        {
            // Configura el nombre de la tabla
            builder.ToTable("Followers");

            // Configura la columna FollowerUsername
            builder.Property(f => f.FollowerUsername)
                .HasColumnType("varchar(50)")
                .IsRequired();  // Asegura que el campo no sea nulo

            // Configura la columna FollowedUsername
            builder.Property(f => f.FollowedUsername)
                .HasColumnType("varchar(50)")
                .IsRequired();  // Asegura que el campo no sea nulo

            // Si deseas agregar una clave primaria compuesta
            builder.HasKey(f => new { f.FollowerUsername, f.FollowedUsername });
        }
    }
}
