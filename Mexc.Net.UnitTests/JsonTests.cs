using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mexc.Net.Interfaces.Clients;
using Mexc.Net.UnitTests.Helpers;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Authentication;

namespace Mexc.Net.UnitTests
{
    [TestFixture]
    public class JsonTests
    {
        private JsonToObjectComparer<IMexcRestClient> _comparer = new JsonToObjectComparer<IMexcRestClient>((json) => TestHelpers.CreateResponseClient(json, options =>
        {
            options.ApiCredentials = new ApiCredentials("123", "123");
            options.SpotOptions.RateLimiters = new List<IRateLimiter>();
            options.SpotOptions.AutoTimestamp = false;
        }));

        [Test]
        public async Task ValidateSpotExchangeDataCalls()
        {
            await _comparer.ProcessSubject(
                "Spot/ExchangeData",
                c => c.SpotApi.ExchangeData,
                useNestedJsonPropertyForCompare: new Dictionary<string, string> {
                },
                parametersToSetNull: new string [] { },
                ignoreProperties: new Dictionary<string, List<string>>
                {
                    { "GetExchangeInfoAsync", new List<string>{ "orderTypes", "filters" } },
                    { "GetRecentTradesAsync", new List<string>{ "id" } },
                    { "GetAggregatedTradeHistoryAsync", new List<string>{ "a", "f", "l" } },
                    { "GetTickersAsync", new List<string>{ "bidQty", "askQty", "quoteVolume", "count" } },
                });
        }
    }
}
