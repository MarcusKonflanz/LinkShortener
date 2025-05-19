using LinkShortener.Services;
using Microsoft.AspNetCore.Mvc;
using LinkShortener.Data;
using System.Threading.Tasks;
using LinkShortener.Models;

namespace LinkShortener.Controllers
{
    [ApiController]
    [Route("api/shortUrl")]
    public class ShortenerController : ControllerBase
    {
        private readonly UrlShortenerService _urlShortenerService;
        private readonly AppDbContext _context;

        public ShortenerController(UrlShortenerService shortenerService, AppDbContext context)
        {
            _urlShortenerService = shortenerService;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> ShortenUrl([FromBody] string originalUrl)
        {
            var code = await _urlShortenerService.GenerateUniqueCode();

            var entity = new ShortUrl
            {
                OriginalUrl = originalUrl,
                ShortCode = code,
                CreateAt = DateTime.UtcNow,
                Clics = 0
            };


            _context.ShortUrls.Add(entity);
            await _context.SaveChangesAsync();

            var shortUrl = $"{Request.Scheme}://{Request.Host}/r/{code}";

            return Ok(new { shortUrl });
        }

        [HttpGet]
        public async Task<IActionResult> RedirectToOriginal(string shortCode)
        {
            var originalUrl = await _urlShortenerService.GetOriginalUrlAsync(shortCode);

            if (originalUrl is null)
                return NotFound();

            return Redirect(originalUrl);
        }

    }
}
