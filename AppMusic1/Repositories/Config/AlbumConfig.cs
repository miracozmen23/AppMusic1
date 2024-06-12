using AppMusic1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppMusic1.Repositories.Config
{
    public class AlbumConfig : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.HasData(
                new Album { Id = 1, Name = "Makaveli",SingerId=1 },
                new Album { Id = 2, Name = "An", SingerId=2 },
                new Album { Id = 3, Name = "Kara Kedi", SingerId = 3 }
            );
        }
    }
}
