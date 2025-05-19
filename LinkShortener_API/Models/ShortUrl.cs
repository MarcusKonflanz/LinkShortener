namespace LinkShortener.Models
{
    public class ShortUrl
    {
        public int id {  get; set; }
        public string OriginalUrl { get; set; }
        public string ShortCode { get; set; }
        public DateTime CreateAt { get; set; }
        public int Clics { get; set; }
    }
}
