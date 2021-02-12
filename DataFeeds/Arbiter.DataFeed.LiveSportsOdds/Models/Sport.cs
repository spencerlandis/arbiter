namespace Arbiter.LiveSportsOdds.Models
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

        public static Sport EuroLeagueBasketball => new Sport()
        {
            Key = "basketball_euroleague",
            Title = "EuroLeague Basketball"
        };

        public static Sport EnglishPermierLeague => new Sport()
        {
            Key = "soccer_epl",
            Title = "English Permier League"
        };

        public static Sport Ligue1 => new Sport()
        {
            Key = "soccer_france_ligue_one",
            Title = "Ligue 1 - France"
        };

        public static Sport LaLiga => new Sport()
        {
            Key = "soccer_spain_la_liga",
            Title = "La Liga - Spain"
        };

        public static Sport AussieFootball => new Sport()
        {
            Key = "aussierules_afl",
            Title = "Aussie Football"
        };

        public static Sport NHL => new Sport()
        {
            Key = "icehockey_nhl",
            Title = "NHL"
        };
    }
}
