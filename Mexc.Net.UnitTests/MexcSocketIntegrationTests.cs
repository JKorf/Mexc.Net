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
        public override bool Run { get; set; } = true;

        public MexcSocketIntegrationTests()
        {
        }

        public override MexcSocketClient GetClient(ILoggerFactory loggerFactory, bool useUpdatedDeserialization)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");

            Authenticated = key != null && sec != null;
            return new MexcSocketClient(Options.Create(new MexcSocketOptions
            {
                OutputOriginalData = true,
                UseUpdatedDeserialization = useUpdatedDeserialization,
                ApiCredentials = Authenticated ? new CryptoExchange.Net.Authentication.ApiCredentials(key, sec) : null
            }), loggerFactory);
        }

        private MexcRestClient GetRestClient(bool useUpdatedDeserialization)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");

            Authenticated = key != null && sec != null;
            return new MexcRestClient(x =>
            {
                x.UseUpdatedDeserialization = useUpdatedDeserialization;
                x.ApiCredentials = Authenticated ? new CryptoExchange.Net.Authentication.ApiCredentials(key, sec) : null;
            });
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task TestSubscriptions(bool useUpdatedDeserialization)
        {
            var listenKey = await GetRestClient(useUpdatedDeserialization).SpotApi.Account.StartUserStreamAsync();
            await RunAndCheckUpdate<MexcStreamBookTick>(useUpdatedDeserialization, (client, updateHandler) => client.SpotApi.SubscribeToAccountUpdatesAsync(listenKey.Data, default, default), false, true);
            await RunAndCheckUpdate<MexcStreamBookTick>(useUpdatedDeserialization, (client, updateHandler) => client.SpotApi.SubscribeToBookTickerUpdatesAsync("ETHUSDT", updateHandler, default), true, false);
        } 
    }
}
