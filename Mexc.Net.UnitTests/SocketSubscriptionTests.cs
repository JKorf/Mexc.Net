using CryptoExchange.Net.Testing;
using Mexc.Net.Clients;
using CryptoExchange.Net.Authentication;
using Mexc.Net.Objects.Models.Spot;
using Mexc.Net.Objects.Models.Futures;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CryptoExchange.Net.Objects;

namespace Mexc.Net.UnitTests
{
    [TestFixture]
    public class SocketSubscriptionTests
    {
        [Test]
        public Task ValidateSpotSubscriptions()
        {
            var client = new MexcSocketClient(opts =>
            {
                opts.ApiCredentials = new ApiCredentials("123", "456");
            });
            var tester = new SocketSubscriptionValidator<MexcSocketClient>(client, "Subscriptions/SpotApi", "wss://wbs.mexc.com", "d");

            return Task.CompletedTask;
            // Changed to protobuf, no support for testing that yet
            //await tester.ValidateAsync<MexcStreamTrade[]>((client, handler) => client.SpotApi.SubscribeToTradeUpdatesAsync("ETHUSDT", 10, handler), "Trades", nestedJsonProperty: "d.deals");
            //await tester.ValidateAsync<MexcStreamKline>((client, handler) => client.SpotApi.SubscribeToKlineUpdatesAsync("ETHUSDT", Enums.KlineInterval.OneDay, handler), "Klines", nestedJsonProperty: "d.k");
            //await tester.ValidateAsync<MexcStreamOrderBook>((client, handler) => client.SpotApi.SubscribeToOrderBookUpdatesAsync("ETHUSDT", 10, handler), "Book");
            //await tester.ValidateAsync<MexcStreamOrderBook>((client, handler) => client.SpotApi.SubscribeToPartialOrderBookUpdatesAsync("ETHUSDT", 10, handler), "PartialBook");
            //await tester.ValidateAsync<MexcStreamBookTick>((client, handler) => client.SpotApi.SubscribeToBookTickerUpdatesAsync("ETHUSDT", handler), "BookTicker");
            //await tester.ValidateAsync<MexcAccountUpdate>((client, handler) => client.SpotApi.SubscribeToAccountUpdatesAsync("123", handler), "Account");
            //await tester.ValidateAsync<MexcUserOrderUpdate>((client, handler) => client.SpotApi.SubscribeToOrderUpdatesAsync("123", handler), "Orders");
            //await tester.ValidateAsync<MexcUserTradeUpdate>((client, handler) => client.SpotApi.SubscribeToUserTradeUpdatesAsync("123", handler), "UserTrades");
        }

        [Test]
        public async Task ValidateFuturesSubscriptions()
        {

            var loggerFact = new LoggerFactory();
            loggerFact.AddProvider(new TraceLoggerProvider());

            var client = new MexcSocketClient(Options.Create(new Objects.Options.MexcSocketOptions
            {
                ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456")
            }), loggerFact);
            var tester = new SocketSubscriptionValidator<MexcSocketClient>(client, "Subscriptions/FuturesApi", "wss://contract.mexc.com/edge", "data");
            await tester.ValidateAsync<MexcFuturesTickerUpdate[]>((client, handler) => client.FuturesApi.SubscribeToTickersUpdatesAsync(handler), "Tickers", ignoreProperties: []);
            await tester.ValidateAsync<MexcFuturesTicker>((client, handler) => client.FuturesApi.SubscribeToTickerUpdatesAsync("BTC_USDT", handler), "Ticker", ignoreProperties: ["contractId"]);
            await tester.ValidateAsync<MexcFuturesTrade>((client, handler) => client.FuturesApi.SubscribeToTradeUpdatesAsync("BTC_USDT", handler), "Trades", ignoreProperties: ["O"]);
            await tester.ValidateAsync<MexcFuturesStreamKline>((client, handler) => client.FuturesApi.SubscribeToKlineUpdatesAsync("BTC_USDT", Enums.FuturesKlineInterval.OneMonth, handler), "Klines", ignoreProperties: []);
            await tester.ValidateAsync<MexcFuturesOrderBook>((client, handler) => client.FuturesApi.SubscribeToOrderBookUpdatesAsync("BTC_USDT", handler), "Book", ignoreProperties: []);
            await tester.ValidateAsync<MexcFuturesOrderBook>((client, handler) => client.FuturesApi.SubscribeToPartialOrderBookUpdatesAsync("BTC_USDT", 5, handler), "PartialBook", ignoreProperties: []);
            await tester.ValidateAsync<MexcFundingRateUpdate>((client, handler) => client.FuturesApi.SubscribeToFundingRateUpdatesAsync("BTC_USDT", handler), "FundingRate", ignoreProperties: []);
            await tester.ValidateAsync<MexcPriceUpdate>((client, handler) => client.FuturesApi.SubscribeToIndexPriceUpdatesAsync("BTC_USDT", handler), "IndexPrice", ignoreProperties: []);
            await tester.ValidateAsync<MexcPriceUpdate>((client, handler) => client.FuturesApi.SubscribeToMarkPriceUpdatesAsync("BTC_USDT", handler), "MarkPrice", ignoreProperties: []);
            
            await tester.ValidateAsync<MexcFuturesBalanceUpdate>((client, handler) => client.FuturesApi.SubscribeToUserDataUpdatesAsync(handler), "Balance", ignoreProperties: []);
            await tester.ValidateAsync<MexcFuturesOrder>((client, handler) => client.FuturesApi.SubscribeToUserDataUpdatesAsync(null, handler), "Order", ignoreProperties: []);
            await tester.ValidateAsync<MexcPosition>((client, handler) => client.FuturesApi.SubscribeToUserDataUpdatesAsync(null, null, handler), "Position", ignoreProperties: []);

        }
    }
}
