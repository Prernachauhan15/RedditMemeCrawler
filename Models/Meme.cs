namespace RedditMemeCrawler.Models
{
    public class Meme
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public int Upvotes { get; set; }
        public DateTime CreatedUtc { get; set; }
        public string Permalink { get; set; } = string.Empty;
    }
}
