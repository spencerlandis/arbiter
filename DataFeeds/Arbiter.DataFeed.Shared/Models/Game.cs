using Arbiter.DataFeed.Shared.Enums;
using System;
using System.Collections.Generic;

namespace Arbiter.DataFeed.Shared.Models
{
    public class Game
    {
        public SportId Sport { get; set; }
        public DateTime StartTime { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public IEnumerable<Odds> Odds { get; set; }
    }
}
