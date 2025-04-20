using Mexc.Net.Clients;
using Mexc.Net.Objects.Models;
using Mexc.Net.Objects.Options;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mexc.Net.Objects.Models.Spot;

namespace Mexc.Net.UnitTests
{
    [NonParallelizable]
    internal class MexcSocketIntegrationTests : SocketIntegrationTest<MexcSocketClient>
    {
        public override bool Run { get; set; } = false;

        public MexcSocketIntegrationTests()
        {
        }

        public override MexcSocketClient GetClient(ILoggerFactory loggerFactory)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");

            Authenticated = key != null && sec != null;
            return new MexcSocketClient(Options.Create(new MexcSocketOptions
            {
                OutputOriginalData = true,
                ApiCredentials = Authenticated ? new CryptoExchange.Net.Authentication.ApiCredentials(key, sec) : null
            }), loggerFactory);
        }

        private MexcRestClient GetRestClient()
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");

            Authenticated = key != null && sec != null;
            return new MexcRestClient(x =>
            {
                x.ApiCredentials = Authenticated ? new CryptoExchange.Net.Authentication.ApiCredentials(key, sec) : null;
            });
        }

        [Test]
        public async Task TestSubscriptions()
        {
            var listenKey = await GetRestClient().SpotApi.Account.StartUserStreamAsync();
            await RunAndCheckUpdate<MexcStreamBookTick>((client, updateHandler) => client.SpotApi.SubscribeToAccountUpdatesAsync(listenKey.Data, default, default), false, true);
            await RunAndCheckUpdate<MexcStreamBookTick>((client, updateHandler) => client.SpotApi.SubscribeToBookTickerUpdatesAsync("ETHUSDT", updateHandler, default), true, false);
        } 
    }
}
