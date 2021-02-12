using Arbiter.DataFeed.Shared.Enums;
using Arbiter.DataFeed.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Arbiter.DataFeed.Shared.Interfaces
{
    public interface IDataFeed
    {
        DataFeedId DataFeedId { get; }
        TimeSpan Throttle { get; }
        IEnumerable<SportId> SupportedSports { get; }
        Task<IEnumerable<Game>> GetOdds(SportId sport, CancellationToken cancellation = default);
    }
}
