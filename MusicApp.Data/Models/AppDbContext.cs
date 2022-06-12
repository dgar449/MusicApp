
using Microsoft.EntityFrameworkCore;
using MusicApp.Controllers;

namespace MusicApp.Models
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<Song> Song { get; set; }
        public DbSet<Actor> Actors { get; set; }
    }
}
