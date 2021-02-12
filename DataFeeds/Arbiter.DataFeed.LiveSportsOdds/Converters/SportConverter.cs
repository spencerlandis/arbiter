using Arbiter.Core.Enums;
using Arbiter.Core.Interfaces;
using System;

namespace Arbiter.LiveSportsOdds.Converters
{
    public class SportConverter : IConverter<SportId, Models.Sport>
    {
        public SportId Convert(Models.Sport input)
        {
            return input.Key switch
            {
                "basketball_nba" => SportId.NBA,
                "basketball_ncaab" => SportId.NCAAM,
                "basketball_euroleague" => SportId.EuroLeagueBasketball,
                "soccer_epl" => SportId.EnglishPremierLeague,
                "soccer_france_ligue_one" => SportId.Ligue1,
                "soccer_spain_la_liga" => SportId.LaLiga,
                "aussierules_afl" => SportId.AussieFootball,
                "icehockey_nhl" => SportId.NHL,
                _ => throw new ArgumentOutOfRangeException($"Sport {input.Key} is unknown"),
            };
        }

        public Models.Sport Convert(SportId input)
        {
            return input switch
            {
                SportId.NBA => Models.Sport.NBA,
                SportId.NCAAM => Models.Sport.NCAAM,
                SportId.EuroLeagueBasketball => Models.Sport.EuroLeagueBasketball,
                SportId.EnglishPremierLeague => Models.Sport.EnglishPermierLeague,
                SportId.Ligue1 => Models.Sport.Ligue1,
                SportId.LaLiga => Models.Sport.LaLiga,
                SportId.AussieFootball => Models.Sport.AussieFootball,
                SportId.NHL => Models.Sport.NHL,
                _ => throw new ArgumentOutOfRangeException($"Sport {input} is unknown")
            };
        }
    }
}
