using Arbiter.Utilities.Cache;
using Arbiter.Utilities.Managers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Arbiter.Utilities
{
    public static class UtilitiesModule
    {
        public static IServiceCollection LoadUtilitiesModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("redis");
            });

            services.AddScoped<ICacheManager, CacheManager>();

            services.AddScoped<IDataFeedManager, DataFeedManager>();

            return services;
        }
    }
}
