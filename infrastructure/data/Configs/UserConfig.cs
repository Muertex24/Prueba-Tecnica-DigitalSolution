using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using core.domain.Entities;

namespace infrastructure.data.Configs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Configura el nombre de la tabla
            builder.ToTable("Users");

            // Configura la clave primaria
            builder.HasKey(u => u.Username);

            // Configura la longitud del campo Username
            builder.Property(u => u.Username)
                .HasColumnType("varchar(50)")
                .IsRequired();  // Asegura que el campo no sea nulo
        }
    }
}
