using Arbiter.Core.Enums;
using Arbiter.Core.Interfaces;
using Arbiter.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Arbiter.LiveSportsOdds
{
    public class LiveSportsOddsDataFeed : IDataFeed
    {
        public const string ClientName = "LiveSportsOdds";

        private readonly ILogger _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConverter<SportId, Models.Sport> _sportConveter;
        private readonly IConverter<IEnumerable<Game>, Models.GetOddsResponse> _responseConverter;

        public LiveSportsOddsDataFeed(
            ILogger<LiveSportsOddsDataFeed> logger,
            IHttpClientFactory httpClientFactory,
            IConverter<SportId, Models.Sport> sportConveter,
            IConverter<IEnumerable<Game>, Models.GetOddsResponse> responseConverter)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _sportConveter = sportConveter;
            _responseConverter = responseConverter;
        }

        public IEnumerable<SportId> SupportedSports => new[] {
            SportId.NBA, 
            SportId.NCAAM,
            SportId.EuroLeagueBasketball,
            SportId.AussieFootball,
            SportId.EnglishPremierLeague,
            SportId.LaLiga,
            SportId.Ligue1,
            SportId.NHL
        };

        public DataFeedId DataFeedId => DataFeedId.LiveSportsOdds;

        public TimeSpan Throttle => TimeSpan.FromHours(4);

        public async Task<IEnumerable<Game>> GetOdds(SportId sport, CancellationToken cancellation = default)
        {
            var client = _httpClientFactory.CreateClient(ClientName);
            var request = new HttpRequestMessage(HttpMethod.Get, GetOddsRoute(sport));
            using var response = await client.SendAsync(request, cancellation);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync(cancellation);
                _logger.LogError("Received response with status code {StatusCode} and message: {Message}",
                    response.StatusCode,
                    content);
                throw new HttpRequestException(content);
            }

            var result = await JsonSerializer.DeserializeAsync<Models.GetOddsResponse>(
                await response.Content.ReadAsStreamAsync(cancellation), 
                cancellationToken: cancellation);

            if (!result.Success)
            {
                throw new HttpRequestException("Request was not successfull");
            }

            return _responseConverter.Convert(result);
        }

        private string GetOddsRoute(SportId sport)
        {
            var convertedSport = _sportConveter.Convert(sport);
            return $"odds?sport={convertedSport.Key}&region=us&dateFormat=iso";
        }
    }
}
