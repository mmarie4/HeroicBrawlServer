using System.Diagnostics.CodeAnalysis;
using HeroicBrawlServer.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace HeroicBrawlServer.DAL.Repositories
{
    public class AppDbContext : DbContext
    {

        public AppDbContext([NotNull] DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<History> Histories { get; set; }
    }
}
