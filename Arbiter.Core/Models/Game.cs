using Arbiter.Core.Enums;
using System;
using System.Collections.Generic;

namespace Arbiter.Core.Models
{
    public class Game
    {
        public SportId Sport { get; set; }
        public DataFeedId DataFeedId { get; set; }
        public DateTime StartTime { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public IEnumerable<Odds> Odds { get; set; }
    }
}
