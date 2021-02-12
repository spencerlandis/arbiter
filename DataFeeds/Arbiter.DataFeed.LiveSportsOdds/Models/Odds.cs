using System.Text.Json.Serialization;

namespace Arbiter.DataFeed.LiveSportsOdds.Models
{
    public class Odds
    {
        [JsonPropertyName("h2h")]
        public decimal[] H2H { get; set; }
    }
}
