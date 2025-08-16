namespace RedditMemeCrawler.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class RedditApiResponse
    {
        [JsonPropertyName("kind")]
        public string Kind { get; set; }

        [JsonPropertyName("data")]
        public RedditListingData Data { get; set; }
    }

    public class RedditListingData
    {
        [JsonPropertyName("after")]
        public string After { get; set; }

        [JsonPropertyName("dist")]
        public int Dist { get; set; }

        [JsonPropertyName("modhash")]
        public string Modhash { get; set; }

        [JsonPropertyName("geo_filter")]
        public string GeoFilter { get; set; }

        [JsonPropertyName("children")]
        public List<RedditPostWrapper> Children { get; set; }
    }

    public class RedditPostWrapper
    {
        [JsonPropertyName("kind")]
        public string Kind { get; set; }

        [JsonPropertyName("data")]
        public RedditPost Data { get; set; }
    }

    public class RedditPost
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("ups")]
        public int Ups { get; set; }

        [JsonPropertyName("downs")]
        public int Downs { get; set; }

        [JsonPropertyName("upvote_ratio")]
        public double UpvoteRatio { get; set; }

        [JsonPropertyName("permalink")]
        public string Permalink { get; set; }

        [JsonPropertyName("created_utc")]
        public double CreatedUtc { get; set; }
    }

}
