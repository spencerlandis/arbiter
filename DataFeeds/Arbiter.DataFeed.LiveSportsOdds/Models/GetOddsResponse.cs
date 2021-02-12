using System.Text.Json.Serialization;

namespace Arbiter.LiveSportsOdds.Models
{
    public class GetOddsResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        [JsonPropertyName("data")]
        public Game[] Data { get; set; }
    }
}
