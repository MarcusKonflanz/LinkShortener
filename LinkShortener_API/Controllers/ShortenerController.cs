using LinkShortener.Services;
using Microsoft.AspNetCore.Mvc;
using LinkShortener.Data;
using LinkShortener.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

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

            var fullUrl = $"{Request.Scheme}://{Request.Host}/r/{code}";
            return Ok(new { shortCode = code, fullUrl });
        }

        [HttpGet("/r/{code}")]
        public async Task<IActionResult> RedirectToOriginal(string code)
        {
            var shortUrl = await _context.ShortUrls.FirstOrDefaultAsync(x => x.ShortCode == code);

            if (shortUrl == null)
                return NotFound("Código inválido.");

            // Incrementa os cliques
            shortUrl.Clics++;
            await _context.SaveChangesAsync();

            return Redirect(shortUrl.OriginalUrl);
        }

    }
}
