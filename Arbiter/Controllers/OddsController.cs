using Arbiter.DataFeed.Shared.Enums;
using Arbiter.DataFeed.Shared.Models;
using Arbiter.Utilities.Managers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Arbiter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OddsController : ControllerBase
    {
        private readonly IDataFeedManager _feedManager;

        public OddsController(IDataFeedManager feedManager)
        {
            _feedManager = feedManager;
        }

        [HttpGet("{sportId}/{dataFeedId}")]
        public async Task<ActionResult<IEnumerable<Game>>> GetOddsFromDataFeed(SportId sportId, DataFeedId dataFeedId, CancellationToken cancellation)
        {
            return Ok(await _feedManager.GetOddsFromDataFeed(sportId, dataFeedId, cancellation));
        }
    }
}
