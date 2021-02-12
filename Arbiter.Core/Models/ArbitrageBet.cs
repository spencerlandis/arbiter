using Arbiter.Core.Enums;
using System;

namespace Arbiter.Core.Models
{
    public class ArbitrageBet
    {
        public OutcomeId Outcome { get; set; }
        public string Site { get; set; }
        public decimal OutcomeOdds { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
