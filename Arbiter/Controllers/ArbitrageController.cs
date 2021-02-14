using Arbiter.Core.Enums;
using Arbiter.Core.Models;
using Arbiter.Utilities.Calculators;
using Arbiter.Utilities.Managers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Arbiter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArbitrageController : ControllerBase
    {
        private readonly IDataFeedManager _feedManager;
        private readonly IArbitrageCalculator _arbitrageCalculator;

        public ArbitrageController(IDataFeedManager feedManager, IArbitrageCalculator arbitrageCalculator)
        {
            _feedManager = feedManager;
            _arbitrageCalculator = arbitrageCalculator;
        }

        [HttpGet("feed/{dataFeedId}/sport/{sportId}")]
        public async Task<ActionResult<IEnumerable<Game>>> CalculateArbitrageFromFeed(SportId sportId, DataFeedId dataFeedId, CancellationToken cancellation)
        {
            var games = await _feedManager.GetOddsFromDataFeed(sportId, dataFeedId, cancellation);
            return Ok(_arbitrageCalculator.CalculateArbitrageOpportunities(games).ToList());
        }
    }
}
