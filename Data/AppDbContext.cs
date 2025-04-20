using Microsoft.EntityFrameworkCore;
using JokeMVCApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace JokeMVCApp.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Jokes> Jokes { get; set; }
        public DbSet<Books> Books { get; set; }
    }
}
