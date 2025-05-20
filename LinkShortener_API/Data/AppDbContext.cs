using Microsoft.EntityFrameworkCore;
using LinkShortener.Models;

namespace LinkShortener.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<ShortUrl> ShortUrls { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Apenas para ferramentas de migração (não usado em runtime)
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=shortener.db");
            }
        }
    }
}