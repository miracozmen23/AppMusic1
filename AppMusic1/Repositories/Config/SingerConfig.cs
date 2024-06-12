using AppMusic1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppMusic1.Repositories.Config
{
    public class SingerConfig : IEntityTypeConfiguration<Singer>
    {
        public void Configure(EntityTypeBuilder<Singer> builder)
        {
            builder.HasData(
                new Singer { Id = 1, Name = "Motive"},
                new Singer { Id = 2, Name = "Edis"},
                new Singer { Id = 3, Name = "Serdar Ortaç"}
            );
        }
    }
}
