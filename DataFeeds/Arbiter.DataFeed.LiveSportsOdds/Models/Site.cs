using System;
using System.Text.Json.Serialization;

namespace Arbiter.DataFeed.LiveSportsOdds.Models
{
    public class Site
    {
        [JsonPropertyName("site_key")]
        public string SiteKey { get; set; }
        [JsonPropertyName("site_nice")]
        public string SiteNice { get; set; }
        [JsonPropertyName("last_update")]
        public DateTime LastUpdate { get; set; }
        [JsonPropertyName("odds")]
        public Odds Odds { get; set; }
    }
}
