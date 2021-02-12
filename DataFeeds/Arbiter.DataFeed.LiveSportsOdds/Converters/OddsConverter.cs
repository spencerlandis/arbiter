using Arbiter.DataFeed.Shared.Interfaces;
using Arbiter.DataFeed.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Arbiter.DataFeed.LiveSportsOdds.Converters
{
    public class OddsConverter : IConverter<IEnumerable<Odds>, Models.Game>
    {
        public IEnumerable<Odds> Convert(Models.Game input)
        {
            var teams = input.Teams;
            var homeIndex = Array.IndexOf(teams, input.HomeTeam);
            var awayIndex = Array.IndexOf(teams, input.Teams.Single(t => t != input.HomeTeam));
            return input.Sites.Select(s => new Odds()
            {
                Site = s.SiteNice,
                LastUpdate = s.LastUpdate,
                HomeOdds = s.Odds.H2H[homeIndex],
                AwayOdds = s.Odds.H2H[awayIndex],
                TieOdds = s.Odds.H2H.Length > 2 ? s.Odds.H2H[2] : null
            });
        }

        public Models.Game Convert(IEnumerable<Odds> input)
        {
            throw new InvalidOperationException("Can't convert back to Game from a list of Odds");
        }
    }
}
