using System;
using System.Text.Json.Serialization;

namespace Arbiter.LiveSportsOdds.Models
{
    public class Game
    {
        [JsonPropertyName("sport_key")]
        public string SportKey { get; set; }
        [JsonPropertyName("sport_nice")]
        public string SportNice { get; set; }
        [JsonPropertyName("teams")]
        public string[] Teams { get; set; }
        [JsonPropertyName("home_team")]
        public string HomeTeam { get; set; }
        [JsonPropertyName("commence_time")]
        public DateTime CommenceTime { get; set; }
        [JsonPropertyName("sites")]
        public Site[] Sites { get; set; }
    }
}
