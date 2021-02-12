namespace Arbiter.DataFeed.Shared.Models
{
    public class Odds
    {
        public string Site { get; set; }
        public decimal HomeOdds { get; set; }
        public decimal AwayOdds { get; set; }
        public decimal? TieOdds { get; set; }
    }
}
