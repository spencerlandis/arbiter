namespace Arbiter.DataFeed.LiveSportsOdds.Models
{
    public class Sport
    {
        public string Key { get; set; }
        public string Title { get; set; }

        public static Sport NBA => new Sport()
        {
            Key = "basketball_nba",
            Title = "NBA"
        };

        public static Sport NCAAM => new Sport()
        {
            Key = "basketball_ncaab",
            Title = "NCAAB"
        };
    }
}
