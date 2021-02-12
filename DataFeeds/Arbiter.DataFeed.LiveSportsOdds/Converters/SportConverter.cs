using Arbiter.DataFeed.Shared.Enums;
using Arbiter.DataFeed.Shared.Interfaces;
using System;

namespace Arbiter.DataFeed.LiveSportsOdds.Converters
{
    public class SportConverter : IConverter<SportId, Models.Sport>
    {
        public SportId Convert(Models.Sport input)
        {
            return input.Key switch
            {
                "basketball_nba" => SportId.NBA,
                "basketball_ncaab" => SportId.NCAAM,
                _ => throw new ArgumentOutOfRangeException($"Sport {input.Key} is unknown"),
            };
        }

        public Models.Sport Convert(SportId input)
        {
            return input switch
            {
                SportId.NBA => Models.Sport.NBA,
                SportId.NCAAM => Models.Sport.NCAAM,
                _ => throw new ArgumentOutOfRangeException($"Sport {input} is unknown")
            };
        }
    }
}
