using Arbiter.Core.Enums;
using Arbiter.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Arbiter.Core.Interfaces
{
    public interface IDataFeed
    {
        DataFeedId DataFeedId { get; }
        TimeSpan Throttle { get; }
        IEnumerable<SportId> SupportedSports { get; }
        Task<IEnumerable<Game>> GetOdds(SportId sport, CancellationToken cancellation = default);
    }
}
