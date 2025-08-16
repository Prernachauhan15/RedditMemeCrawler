using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedditMemeCrawler.Data;
using System.Text;

namespace RedditMemeCrawler.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReportController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("daily-report")]
        public async Task<IActionResult> GenerateReport()
        {
            var since = DateTime.UtcNow.AddDays(-1);
            var memes = await _context.Memes
                .Where(m => m.CreatedUtc >= since)
                .OrderByDescending(m => m.Upvotes)
                .Take(20)
                .ToListAsync();

            var csv = new StringBuilder();
            csv.AppendLine("Title,Upvotes,Url,CreatedUtc,Permalink");

            foreach (var m in memes)
            {
                csv.AppendLine($"\"{m.Title.Replace("\"", "'")}\",{m.Upvotes},{m.Url},{m.CreatedUtc},{m.Permalink}");
            }

            var bytes = Encoding.UTF8.GetBytes(csv.ToString());
            return File(bytes, "text/csv", "DailyMemeReport.csv");
        }
    }
}
