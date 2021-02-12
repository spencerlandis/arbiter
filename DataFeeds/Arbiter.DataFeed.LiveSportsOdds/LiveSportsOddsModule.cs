using Arbiter.LiveSportsOdds.Converters;
using Arbiter.Core.Enums;
using Arbiter.Core.Interfaces;
using Arbiter.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace Arbiter.LiveSportsOdds
{
    public static class LiveSportsOddsModule
    {
        public static IServiceCollection LoadLiveSportsOddsModule(this IServiceCollection services, IConfiguration configuration)
        {
            //converters
            services.AddScoped<IConverter<SportId, Models.Sport>, SportConverter>();
            services.AddScoped<IConverter<IEnumerable<Odds>, Models.Game>, OddsConverter>();
            services.AddScoped<IConverter<Game, Models.Game>, GameConverter>();
            services.AddScoped<IConverter<IEnumerable<Game>, Models.GetOddsResponse>, ResponseConverter>();

            //http client
            services.Configure<Models.Configuration>(configuration.GetSection(Models.Configuration.Section));
            services.AddHttpClient(LiveSportsOddsDataFeed.ClientName, (serviceProvider, client) =>
            {
                var config = serviceProvider.GetRequiredService<IOptions<Models.Configuration>>().Value;
                client.BaseAddress = new System.Uri(config.Url);
                client.DefaultRequestHeaders.Add("x-rapidapi-host", "odds.p.rapidapi.com");
                client.DefaultRequestHeaders.Add("x-rapidapi-key", config.Key);
            });

            services.AddScoped<IDataFeed, LiveSportsOddsDataFeed>();

            return services;
        }
    }
}
