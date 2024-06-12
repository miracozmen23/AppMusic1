using AppMusic1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppMusic1.Repositories.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User { Id = 1, Username = "Miraç Özmen", Password = "12345", Role = "admin" },
                new User { Id = 2, Username = "Ertan Bütün", Password = "abcde", Role = "StandartUser" }
                );
        }
    }
}
