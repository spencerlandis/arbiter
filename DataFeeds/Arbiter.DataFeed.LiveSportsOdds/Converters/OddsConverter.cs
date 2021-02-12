using Arbiter.Core.Enums;
using Arbiter.Core.Interfaces;
using Arbiter.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Arbiter.LiveSportsOdds.Converters
{
    public class OddsConverter : IConverter<IEnumerable<Odds>, Models.Game>
    {
        public IEnumerable<Odds> Convert(Models.Game input)
        {
            var teams = input.Teams;
            var homeIndex = Array.IndexOf(teams, input.HomeTeam);
            var awayIndex = Array.IndexOf(teams, input.Teams.Single(t => t != input.HomeTeam));
            return input.Sites.Select(s =>
            {
                var result = new Odds()
                {
                    Site = s.SiteNice,
                    LastUpdate = s.LastUpdate,
                    OutcomeOdds = new Dictionary<OutcomeId, decimal>()
                    {
                        [OutcomeId.HomeWin] = s.Odds.H2H[homeIndex],
                        [OutcomeId.AwayWin] = s.Odds.H2H[awayIndex]
                    }
                };

                if(s.Odds.H2H.Length > 2)
                    result.OutcomeOdds[OutcomeId.Tie] = s.Odds.H2H[2];

                return result;
            });
        }

        public Models.Game Convert(IEnumerable<Odds> input)
        {
            throw new InvalidOperationException("Can't convert back to Game from a list of Odds");
        }
    }
}
