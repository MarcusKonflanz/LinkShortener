using Microsoft.EntityFrameworkCore;
using LinkShortener.Models;

namespace LinkShortener.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSet que representa a tabela ShortUrls no banco
        public DbSet<ShortUrl> ShortUrls { get; set; }
    }
}
