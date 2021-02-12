using Arbiter.Core.Enums;
using System;
using System.Collections.Generic;

namespace Arbiter.Core.Models
{
    public class Odds
    {
        public string Site { get; set; }
        public DateTime LastUpdate { get; set; }
        public Dictionary<OutcomeId, decimal> OutcomeOdds { get; set; }
    }
}
