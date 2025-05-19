using LinkShortener.Data;
using LinkShortener.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.Services
{
    public class UrlShortenerService
    {
        private readonly AppDbContext _context;
        public UrlShortenerService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<string> GenerateUniqueCode()
        {
            string shortCode;
            bool exists;

            do
            {
                shortCode = Guid.NewGuid().ToString("N").Substring(0, 6);
                exists = await _context.ShortUrls.AnyAsync(u => u.ShortCode == shortCode);
            } while (exists);

            return shortCode;
        }
        public async Task<string> ShortenUrlAsync(string originalUrl)
        {
            var existing = await _context.ShortUrls.FirstOrDefaultAsync(u => u.OriginalUrl == originalUrl);

            if (existing != null)
                return existing.ShortCode;

            var shortCode = await GenerateUniqueCode();

            var shortUrl = new ShortUrl
            {
                ShortCode = shortCode,
                OriginalUrl = originalUrl,
                CreateAt = DateTime.UtcNow
            };

            _context.ShortUrls.Add(shortUrl);
            await _context.SaveChangesAsync();

            return shortCode;
        }
        public async Task<string?> GetOriginalUrlAsync(string shortCode)
        {
            var shortUrl = await _context.ShortUrls.FirstOrDefaultAsync(u => u.ShortCode == shortCode);

            return shortUrl?.OriginalUrl;
        }
    }
}
