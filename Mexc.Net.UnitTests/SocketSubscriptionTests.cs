using CryptoExchange.Net.Testing;
using Mexc.Net.Clients;
using CryptoExchange.Net.Authentication;
using Mexc.Net.Objects.Models.Spot;
using Mexc.Net.Objects.Models.Futures;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CryptoExchange.Net.Objects;
using Mexc.Net.Objects.Models;

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
                opts.ApiCredentials = new MexcCredentials().WithHMAC("123", "456");
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
                OutputOriginalData = true,
                ApiCredentials = new MexcCredentials().WithHMAC("123", "456")
            }), loggerFact);
            var tester = new SocketSubscriptionValidator<MexcSocketClient>(client, "Subscriptions/FuturesApi", "wss://contract.mexc.com/edge", "data");
            await tester.ValidateAsync<MexcFuturesTickerUpdate[]>((client, handler) => client.FuturesApi.SubscribeToTickersUpdatesAsync(handler), "Tickers", ignoreProperties: []);
            await tester.ValidateAsync<MexcFuturesTicker>((client, handler) => client.FuturesApi.SubscribeToTickerUpdatesAsync("BTC_USDT", handler), "Ticker", ignoreProperties: ["contractId"]);
            await tester.ValidateAsync<MexcFuturesTrade[]>((client, handler) => client.FuturesApi.SubscribeToTradeUpdatesAsync("BTC_USDT", handler), "Trades", ignoreProperties: ["O"]);
            await tester.ValidateAsync<MexcFuturesStreamKline>((client, handler) => client.FuturesApi.SubscribeToKlineUpdatesAsync("BTC_USDT", Enums.FuturesKlineInterval.OneMonth, handler), "Klines", ignoreProperties: []);
            await tester.ValidateAsync<MexcFuturesOrderBook>((client, handler) => client.FuturesApi.SubscribeToOrderBookUpdatesAsync("BTC_USDT", handler), "Book", ignoreProperties: []);
            await tester.ValidateAsync<MexcFuturesOrderBook>((client, handler) => client.FuturesApi.SubscribeToPartialOrderBookUpdatesAsync("BTC_USDT", 5, handler), "PartialBook", ignoreProperties: []);
            await tester.ValidateAsync<MexcFundingRateUpdate>((client, handler) => client.FuturesApi.SubscribeToFundingRateUpdatesAsync("BTC_USDT", handler), "FundingRate", ignoreProperties: []);
            await tester.ValidateAsync<MexcPriceUpdate>((client, handler) => client.FuturesApi.SubscribeToIndexPriceUpdatesAsync("BTC_USDT", handler), "IndexPrice", ignoreProperties: []);
            await tester.ValidateAsync<MexcPriceUpdate>((client, handler) => client.FuturesApi.SubscribeToMarkPriceUpdatesAsync("BTC_USDT", handler), "MarkPrice", ignoreProperties: []);
            await tester.ValidateAsync<MexcContract>((client, handler) => client.FuturesApi.SubscribeToSymbolUpdatesAsync(handler), "Symbol", ignoreProperties: ["openingCountdownOption"]);

            await tester.ValidateAsync<MexcFuturesBalanceUpdate>((client, handler) => client.FuturesApi.SubscribeToUserDataUpdatesAsync(handler), "Balance", ignoreProperties: []);
            await tester.ValidateAsync<MexcFuturesOrder>((client, handler) => client.FuturesApi.SubscribeToUserDataUpdatesAsync(null, handler), "Order", ignoreProperties: ["remainVol"]);
            await tester.ValidateAsync<MexcPosition>((client, handler) => client.FuturesApi.SubscribeToUserDataUpdatesAsync(null, null, handler), "Position", ignoreProperties: []);
            await tester.ValidateAsync<MexcRiskLimit>((client, handler) => client.FuturesApi.SubscribeToUserDataUpdatesAsync(null, null, null, handler), "RiskLimit", ignoreProperties: ["maxVolView"]);
            await tester.ValidateAsync<MexcPositionModeUpdate>((client, handler) => client.FuturesApi.SubscribeToUserDataUpdatesAsync(null, null, null, null, null, handler), "PositionMode", ignoreProperties: []);
            await tester.ValidateAsync<MexcStopOrder>((client, handler) => client.FuturesApi.SubscribeToUserDataUpdatesAsync(null, null, null, null, null, null, handler), "PlanOrder", ignoreProperties: []);
            await tester.ValidateAsync<MexcTpSlOrder>((client, handler) => client.FuturesApi.SubscribeToUserDataUpdatesAsync(null, null, null, null, null, null, null, handler), "TpSlOrder", ignoreProperties: []);
            await tester.ValidateAsync<MexcTrailingOrder>((client, handler) => client.FuturesApi.SubscribeToUserDataUpdatesAsync(null, null, null, null, null, null, null, null, handler), "TrailingOrder", ignoreProperties: []);
            await tester.ValidateAsync<MexcTpSlPriceUpdate>((client, handler) => client.FuturesApi.SubscribeToUserDataUpdatesAsync(null, null, null, null, null, null, null, null, null, handler), "TpSlPrice", ignoreProperties: []);
            await tester.ValidateAsync<MexcFuturesUserTradeUpdate>((client, handler) => client.FuturesApi.SubscribeToUserDataUpdatesAsync(null, null, null, null, null, null, null, null, null, null, handler), "UserTrade", ignoreProperties: []);
            await tester.ValidateAsync<MexcChaseOrderFailure>((client, handler) => client.FuturesApi.SubscribeToUserDataUpdatesAsync(null, null, null, null, null, null, null, null, null, null, null, handler), "ChaseFail", ignoreProperties: []);
            await tester.ValidateAsync<MexcLiquidationRiskUpdate>((client, handler) => client.FuturesApi.SubscribeToUserDataUpdatesAsync(null, null, null, null, null, null, null, null, null, null, null, null, handler), "LiquidationRisk", ignoreProperties: []);
            await tester.ValidateAsync<MexcLeverageModeUpdate>((client, handler) => client.FuturesApi.SubscribeToUserDataUpdatesAsync(null, null, null, null, null, null, null, null, null, null, null, null, null, handler), "LeverageMode", ignoreProperties: []);
            await tester.ValidateAsync<MexcReversePositionUpdate>((client, handler) => client.FuturesApi.SubscribeToUserDataUpdatesAsync(null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, handler), "ReversePosition", ignoreProperties: []);
            await tester.ValidateAsync<MexcLiquidationUpdate>((client, handler) => client.FuturesApi.SubscribeToUserDataUpdatesAsync(null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, handler), "Liquidation", ignoreProperties: []);

        }
    }
}
