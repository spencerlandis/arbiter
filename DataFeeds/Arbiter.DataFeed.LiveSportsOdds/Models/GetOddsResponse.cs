using System.Text.Json.Serialization;

namespace Arbiter.DataFeed.LiveSportsOdds.Models
{
    public class GetOddsResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        [JsonPropertyName("data")]
        public Game[] Data { get; set; }
    }
}
