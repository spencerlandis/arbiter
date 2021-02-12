using Arbiter.DataFeed.Shared.Enums;
using Arbiter.DataFeed.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Arbiter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OddsController : ControllerBase
    {
        private readonly ILogger<OddsController> _logger;
        private readonly IDictionary<DataFeedId, IDataFeed> _dataFeeds;

        public OddsController(
            ILogger<OddsController> logger,
            IEnumerable<IDataFeed> dataFeeds)
        {
            _logger = logger;
            _dataFeeds = dataFeeds.ToDictionary(d => d.DataFeedId);
        }

        [HttpGet("{sportId}/{dataFeedId}")]
        public async Task<ActionResult> GetOddsFromDataFeed(SportId sportId, DataFeedId dataFeedId, CancellationToken cancellationToken)
        {
            if(!_dataFeeds.TryGetValue(dataFeedId, out var dataFeed))
            {
                _logger.LogError("Data feed id {DataFeedId} is invalid or not available", dataFeedId);
                return BadRequest($"Data feed id {dataFeedId} is invalid or not available");
            }

            return Ok(await dataFeed.GetOdds(sportId, cancellationToken));
        }
    }
}
