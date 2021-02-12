using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Hosting;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Arbiter.Utilities.Cache
{
    public interface ICacheManager
    {
        Task SetAsync<T>(string key, T value, TimeSpan expiration, CancellationToken cancellation);
        Task<T> GetAsync<T>(string key, CancellationToken cancellation);
    }

    public class CacheManager : ICacheManager
    {
        private readonly IDistributedCache _cache;
        private readonly IHostEnvironment _hostEnvironment;

        public CacheManager(
            IDistributedCache cache, 
            IHostEnvironment hostEnvironment)
        {
            _cache = cache;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<T> GetAsync<T>(string key, CancellationToken cancellation)
        {
            var prefixedKey = GetPrefixedString(key);
            var cachedValue = await _cache.GetStringAsync(prefixedKey, cancellation);
            if (cachedValue == null)
                return default;

            return JsonSerializer.Deserialize<T>(cachedValue);
        }

        public Task SetAsync<T>(string key, T value, TimeSpan expiration, CancellationToken cancellation)
        {
            var serialized = JsonSerializer.Serialize(value);
            var prefixedKey = GetPrefixedString(key);
            return _cache.SetStringAsync(prefixedKey, serialized, cancellation);
        }

        private string GetPrefixedString(string key) => $"{{Arbiter}}:{{{GetEnvironmentName()}}}:{key}";

        private string GetEnvironmentName()
        {
            if (_hostEnvironment.IsDevelopment())
                return System.Environment.MachineName;

            return _hostEnvironment.EnvironmentName;
        }
    }
}
