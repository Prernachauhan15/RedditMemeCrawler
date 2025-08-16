using Microsoft.AspNetCore.Mvc;
using RedditMemeCrawler.Data;
using RedditMemeCrawler.Models;
using System.Text.Json;

namespace RedditMemeCrawler.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemeController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;

        public MemeController(AppDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClient = httpClientFactory.CreateClient();
        }
        [HttpPost("fetch")]
        public async Task<IActionResult> FetchMemes([FromQuery] int limit = 20)
        {
            try
            {
                // Build Reddit API URL
                var url = $"https://www.reddit.com/r/memes/top.json?sort=top&t=day&limit={limit}";

                // Set headers
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("User-Agent", "CSharpRedditMemeCrawler/1.0");
                _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

                // Fetch the response
                using var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode(); // Throws if not 2xx

               // var json = await response.Content.ReadAsStringAsync();

        
                //Console.WriteLine(json.Substring(0, Math.Min(1000, json.Length)));

                using var stream = await response.Content.ReadAsStreamAsync();

                // Deserialize into typed objects
                var redditResponse = await JsonSerializer.DeserializeAsync<RedditApiResponse>(stream);

                if (redditResponse == null || redditResponse.Data.Children.Count == 0)
                    return NotFound("No memes found.");

                // Map to Meme entity
                    var memes = redditResponse.Data.Children.Select(p => new Meme
                {
                    Title = p.Data.Title,
                    Url = p.Data.Url,
                    Upvotes = p.Data.Ups,
                    CreatedUtc = DateTimeOffset.FromUnixTimeSeconds((long)Math.Round(p.Data.CreatedUtc)).UtcDateTime,
                Permalink = "https://reddit.com" + p.Data.Permalink
                }).ToList();

                // Save to database
                _context.Memes.AddRange(memes);
                await _context.SaveChangesAsync();

                return Ok(memes);
            }
            catch (HttpRequestException ex)
            {
                // HTTP errors (e.g., 403, 500)
                return StatusCode(503, $"Error fetching from Reddit: {ex.Message}");
            }
            catch (JsonException ex)
            {
                // JSON parsing errors
                return StatusCode(500, $"Error parsing Reddit response: {ex.Message}");
            }
            catch (Exception ex)
            {
                // General errors
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}
