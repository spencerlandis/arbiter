using Arbiter.DataFeed.Shared.Interfaces;
using Arbiter.DataFeed.Shared.Models;
using System.Collections.Generic;
using System.Linq;

namespace Arbiter.DataFeed.LiveSportsOdds.Converters
{
    public class ResponseConverter : IConverter<IEnumerable<Game>, Models.GetOddsResponse>
    {
        private readonly IConverter<Game, Models.Game> _gameConverter;

        public ResponseConverter(IConverter<Game, Models.Game> gameConverter)
        {
            _gameConverter = gameConverter;
        }

        public IEnumerable<Game> Convert(Models.GetOddsResponse input)
        {
            return input.Data.Select(d => _gameConverter.Convert(d));
        }

        public Models.GetOddsResponse Convert(IEnumerable<Game> input)
        {
            return new Models.GetOddsResponse()
            {
                Success = true,
                Data = input.Select(i => _gameConverter.Convert(i)).ToArray()
            };
        }
    }
}
