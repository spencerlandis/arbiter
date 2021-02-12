using Arbiter.Core.Enums;
using Arbiter.Core.Models;
using Arbiter.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Arbiter.Utilities.Calculators
{
    public interface IArbitrageCalculator
    {
        IEnumerable<ArbitrageOpportunity> CalculateArbitrageOpportunities(IEnumerable<Game> games);
    }

    public class ArbitrageCalculator : IArbitrageCalculator
    {
        public IEnumerable<ArbitrageOpportunity> CalculateArbitrageOpportunities(IEnumerable<Game> games)
        {
            return games
                .Where(g => g.StartTime > DateTime.UtcNow.AddMinutes(60))
                .SelectMany(g => GetArbitrageOpportunitiesForGame(g));
        }

        private static readonly OutcomeId[] PrimaryOutcomes = new[] { OutcomeId.AwayWin, OutcomeId.HomeWin };
        private IEnumerable<ArbitrageOpportunity> GetArbitrageOpportunitiesForGame(Game game)
        {
            foreach (var originalBet in game.Odds)
            {
                foreach (var outcome in PrimaryOutcomes)
                {
                    var hedgeOutcome = PrimaryOutcomes.Single(o => o != outcome);
                    foreach (var hedge in game.Odds.Where(o => o.Site != originalBet.Site))
                    {
                        var originalOdds = originalBet.OutcomeOdds[outcome];
                        var hedgeOdds = hedge.OutcomeOdds[hedgeOutcome];
                        var cost = 1.0m / originalOdds
                            + 1.0m / hedgeOdds;
                        if (cost >= 1)
                            continue;

                        var opportunity = new ArbitrageOpportunity()
                        {
                            DataFeedId = game.DataFeedId,
                            Sport = game.Sport,
                            StartTime = game.StartTime,
                            HomeTeam = game.HomeTeam,
                            AwayTeam = game.AwayTeam,
                            Bets = new List<ArbitrageBet>()
                            {
                                new ArbitrageBet()
                                {
                                    Site = originalBet.Site,
                                    LastUpdated = originalBet.LastUpdate,
                                    Outcome = outcome,
                                    OutcomeOdds = originalOdds
                                },
                                new ArbitrageBet()
                                {
                                    Site = hedge.Site,
                                    LastUpdated = hedge.LastUpdate,
                                    Outcome = hedgeOutcome,
                                    OutcomeOdds = hedgeOdds
                                }
                            }
                        };

                        if (originalBet.OutcomeOdds.Keys.Contains(OutcomeId.Tie)
                            || hedge.OutcomeOdds.Keys.Contains(OutcomeId.Tie))
                        {
                            foreach(var tieHedge in game.Odds.Where(o => o.Site != originalBet.Site && o.Site != hedge.Site))
                            {
                                if (!tieHedge.OutcomeOdds.TryGetValue(OutcomeId.Tie, out var tieOdds))
                                    continue;

                                var totalCost = cost + 1.0m / tieOdds;
                                if (totalCost >= 1)
                                    continue;

                                var clone = opportunity.DeepClone();
                                clone.Bets.Add(new ArbitrageBet()
                                {
                                    Site = tieHedge.Site,
                                    LastUpdated = tieHedge.LastUpdate,
                                    Outcome = OutcomeId.Tie,
                                    OutcomeOdds = tieOdds
                                });
                                yield return clone;
                            }
                        }
                        else 
                            yield return opportunity;
                    }
                }
            }
        }
    }
}
