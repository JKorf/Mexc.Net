using CryptoExchange.Net.Testing;
using Mexc.Net.Clients;
using CryptoExchange.Net.Authentication;
using Mexc.Net.Objects.Models.Spot;

namespace Mexc.Net.UnitTests
{
    [TestFixture]
    public class SocketSubscriptionTests
    {
        [Test]
        public async Task ValidateExchangeDataSubscriptions()
        {
            var client = new MexcSocketClient(opts =>
            {
                opts.ApiCredentials = new ApiCredentials("123", "456");
            });
            var tester = new SocketSubscriptionValidator<MexcSocketClient>(client, "Subscriptions/SpotApi", "wss://wbs.mexc.com", "d");
            await tester.ValidateAsync<MexcStreamTrade[]>((client, handler) => client.SpotApi.SubscribeToTradeUpdatesAsync("ETHUSDT", handler), "Trades", nestedJsonProperty: "d.deals");
            await tester.ValidateAsync<MexcStreamKline>((client, handler) => client.SpotApi.SubscribeToKlineUpdatesAsync("ETHUSDT", Enums.KlineInterval.OneDay, handler), "Klines", nestedJsonProperty: "d.k");
            await tester.ValidateAsync<MexcStreamOrderBook>((client, handler) => client.SpotApi.SubscribeToOrderBookUpdatesAsync("ETHUSDT", handler), "Book");
            await tester.ValidateAsync<MexcStreamOrderBook>((client, handler) => client.SpotApi.SubscribeToPartialOrderBookUpdatesAsync("ETHUSDT", 10, handler), "PartialBook");
            await tester.ValidateAsync<MexcStreamBookTick>((client, handler) => client.SpotApi.SubscribeToBookTickerUpdatesAsync("ETHUSDT", handler), "BookTicker");
            await tester.ValidateAsync<MexcStreamMiniTick>((client, handler) => client.SpotApi.SubscribeToMiniTickerUpdatesAsync("ETHUSDT", handler), "MiniTicker", ignoreProperties: ["lastRT", "MT", "NV"]);
            await tester.ValidateAsync<MexcAccountUpdate>((client, handler) => client.SpotApi.SubscribeToAccountUpdatesAsync("123", handler), "Account");
            await tester.ValidateAsync<MexcUserOrderUpdate>((client, handler) => client.SpotApi.SubscribeToOrderUpdatesAsync("123", handler), "Orders");
            await tester.ValidateAsync<MexcUserTradeUpdate>((client, handler) => client.SpotApi.SubscribeToUserTradeUpdatesAsync("123", handler), "UserTrades");
        }
    }
}
