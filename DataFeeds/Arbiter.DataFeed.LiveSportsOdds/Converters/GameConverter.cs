using Arbiter.Core.Enums;
using Arbiter.Core.Interfaces;
using Arbiter.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Arbiter.LiveSportsOdds.Converters
{
    public class GameConverter : IConverter<Game, Models.Game>
    {
        private readonly IConverter<SportId, Models.Sport> _sportConverter;
        private readonly IConverter<IEnumerable<Odds>, Models.Game> _oddsConverter; 

        public GameConverter(IConverter<SportId, Models.Sport> sportConverter,
            IConverter<IEnumerable<Odds>, Models.Game> oddsConverter)
        {
            _sportConverter = sportConverter;
            _oddsConverter = oddsConverter;
        }

        public Game Convert(Models.Game input)
        {
            return new Game()
            {
                Sport = _sportConverter.Convert(new Models.Sport()
                {
                    Key = input.SportKey,
                    Title = input.SportNice
                }),
                HomeTeam = input.HomeTeam,
                AwayTeam = input.Teams.Single(t => t != input.HomeTeam),
                StartTime = input.CommenceTime,
                Odds = _oddsConverter.Convert(input)
            };
        }

        public Models.Game Convert(Game input)
        {
            throw new System.NotImplementedException();
        }
    }
}
