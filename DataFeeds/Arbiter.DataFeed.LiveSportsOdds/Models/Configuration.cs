namespace Arbiter.DataFeed.LiveSportsOdds.Models
{
    public class Configuration
    {
        public const string Section = "LiveSportsOdds";

        public string Url { get; set; }
        public string Host { get; set; }
        public string Key { get; set; }
    }
}
