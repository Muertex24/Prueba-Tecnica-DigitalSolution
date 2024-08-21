using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using core.domain.Entities;

namespace infrastructure.data.Configs
{
    public class PostConfig : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            // Configura el nombre de la tabla
            builder.ToTable("Posts");

            builder.HasKey(p => p.postID);

            builder.Property(p => p.postID)
                .ValueGeneratedOnAdd() 
                .HasColumnType("int");

            // Configura la columna Author
            builder.Property(p => p.AuthorUsername)
                .HasColumnType("varchar(50)")
                .IsRequired(); 

            // Configura la columna Content
            builder.Property(p => p.Content)
                .HasColumnType("varchar(256)")
                .IsRequired();  

            // Configura la columna DatePosted
            builder.Property(p => p.DatePosted)
                .HasColumnType("datetime")
                .IsRequired(); 
        }
    }
}
