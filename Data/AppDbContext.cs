using Microsoft.EntityFrameworkCore;
using JokeMVCApp.Models;

namespace JokeMVCApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Jokes> Jokes { get; set; }
        public DbSet<Books> Books { get; set; }
    }
}
