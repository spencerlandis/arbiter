using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Arbiter.Core.Enums;
using Arbiter.Core.Models;
using Arbiter.Utilities.Managers;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("feed/{dataFeedId}/sport/{sportId}")]
        public async Task<ActionResult<IEnumerable<Game>>> GetOddsFromDataFeed(
            DataFeedId dataFeedId,
            SportId sportId,
            CancellationToken cancellation)
        {
            return Ok(await _feedManager.GetOddsFromDataFeed(sportId, dataFeedId, cancellation));
        }

        [HttpGet("feeds")]
        public ActionResult<IEnumerable<Option>> GetDataFeeds()
        {
            return Ok(Option.GetFromEnum<DataFeedId>());
        }

        [HttpGet("feed/{dataFeedId}/sports")]
        public ActionResult<IEnumerable<Option>> GetDataFeedSports(DataFeedId dataFeedId)
        {
            var sports = _feedManager.GetDataFeedSports(dataFeedId);
            return Ok(Option.GetFromEnum(sports));
        }
    }
}
