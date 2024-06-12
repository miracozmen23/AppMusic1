using AppMusic1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppMusic1.Repositories.Config
{
    public class SongConfig : IEntityTypeConfiguration<Song>
    {
        public void Configure(EntityTypeBuilder<Song> builder)
        {
            builder.HasData(
                new Song { Id = 1, Name = "Devam", AlbumId = 1, Duration = 180 },
                new Song { Id = 2, Name = "Martılar", AlbumId = 2, Duration = 120 },
                new Song { Id = 3, Name = "Poşet", AlbumId = 3, Duration = 150 }
            );
        }
    }
}
