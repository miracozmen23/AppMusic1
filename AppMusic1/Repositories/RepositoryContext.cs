using AppMusic1.Models;
using AppMusic1.Repositories.Config;
using Microsoft.EntityFrameworkCore;

namespace AppMusic1.Repositories
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Singer> Singers { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SingerConfig());
            modelBuilder.ApplyConfiguration(new SongConfig());
            modelBuilder.ApplyConfiguration(new AlbumConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
        }
    }
}
