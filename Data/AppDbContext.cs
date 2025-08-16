using Microsoft.EntityFrameworkCore;
using RedditMemeCrawler.Models;

namespace RedditMemeCrawler.Data
{
    public class AppDbContext : DbContext
    {   
        public DbSet<Meme> Memes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
