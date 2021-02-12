using Arbiter.Core.Enums;
using Arbiter.Core.Interfaces;
using Arbiter.Core.Models;
using Arbiter.Utilities.Cache;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Arbiter.Utilities.Managers
{
    public interface IDataFeedManager
    {
        Task<IEnumerable<Game>> GetOddsFromDataFeed(SportId sportId, DataFeedId dataFeedId, CancellationToken cancellationToken);
    }

    public class DataFeedManager: IDataFeedManager
    {
        private readonly ILogger<DataFeedManager> _logger;
        private readonly IDictionary<DataFeedId, IDataFeed> _dataFeeds;
        private readonly ICacheManager _cache;

        public DataFeedManager(
            ILogger<DataFeedManager> logger,
            IEnumerable<IDataFeed> dataFeeds, 
            ICacheManager cache)
        {
            _logger = logger;
            _dataFeeds = dataFeeds.ToDictionary(d => d.DataFeedId);
            _cache = cache;
        }

        public async Task<IEnumerable<Game>> GetOddsFromDataFeed(SportId sportId, DataFeedId dataFeedId, CancellationToken cancellation)
        {
            var key = GetFeedKey(sportId, dataFeedId);

            var result = await _cache.GetAsync<IEnumerable<Game>>(key, cancellation);
            if (result == null)
            {
                if (!_dataFeeds.TryGetValue(dataFeedId, out var dataFeed))
                {
                    _logger.LogError("Data feed id {DataFeedId} is invalid or not available", dataFeedId);
                    throw new ArgumentException($"Data feed id {dataFeedId} is invalid or not available");
                }
                result = await dataFeed.GetOdds(sportId, cancellation);

                await _cache.SetAsync(key, result, dataFeed.Throttle, cancellation);
            }

            return result;
        }

        private static string GetFeedKey(SportId sportId, DataFeedId dataFeedId)
        {
            return $"{{{dataFeedId}}}:{{{sportId}}}";
        }
    }
}
