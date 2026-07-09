using Mexc.Net.Clients;
using Mexc.Net.Objects;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Mexc.Net.SymbolOrderBooks;
using CryptoExchange.Net.Objects.Errors;

namespace Mexc.Net.UnitTests
{
    [NonParallelizable]
    internal class MexcRestIntegrationTests : RestIntegrationTest<MexcRestClient>
    {
        public override bool Run { get; set; } = true;

        public MexcRestIntegrationTests()
        {
        }

        public override MexcRestClient GetClient(ILoggerFactory loggerFactory)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");

            Authenticated = key != null && sec != null;
            return new MexcRestClient(null, loggerFactory, Options.Create(new Objects.Options.MexcRestOptions
            {
                OutputOriginalData = true,
                ApiCredentials = Authenticated ? new MexcCredentials().WithHMAC(key, sec) : null
            }));
        }

        [Test]
        public async Task TestErrorResponseParsing()
        {
            if (!ShouldRun())
                return;

            var result = await CreateClient().SpotApi.ExchangeData.GetKlinesAsync("TSTTST", Enums.KlineInterval.OneDay, default);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Error.ErrorCode, Is.EqualTo("-1121"));
            Assert.That(result.Error.ErrorType, Is.EqualTo(ErrorType.UnknownSymbol));
        }

        [Test]
        public async Task TestSpotAccount()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetAccountInfoAsync(default), true, ignoreProperties: [
                "available" // Same value as free
                ]);
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetUserAssetsAsync(default), true);
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetDepositHistoryAsync(default, default, default, default, default, default), true);
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetWithdrawHistoryAsync(default, default, default, default, default, default), true);
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetWithdrawAddressesAsync(default, default, default, default), true);
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetTransferHistoryAsync(Enums.AccountType.Spot, Enums.AccountType.Futures, default, default, default, default, default), true);
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetAssetsForDustTransferAsync(default), true);
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetDustLogAsync(default, default, default, default, default), true);
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetMxDeductionStatusAsync(default), true, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetTradeFeeAsync("ETHUSDT", default), true, "data");
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public async Task TestSpotExchangeData()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetServerTimeAsync(default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetApiSymbolsAsync(default), false);
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetExchangeInfoAsync(default, default), false, ignoreProperties: [
                "filters",// Never set, unknown model
                "rateLimits", // Never set, unknown model
                "exchangeFilters", // Never set, unknown model
                "quoteAssetPrecision", // Already have 2 quote asset precision properties, unclear what this does
                "conceptPlateIds", // Unknown what these mean
                "st" // Unknown value
                ]);
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetOrderBookAsync("ETHUSDT", default, default), false);
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetRecentTradesAsync("ETHUSDT", default, default), false, ignoreProperties: [
                "id" // Never set
                ]);
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetAggregatedTradeHistoryAsync("ETHUSDT", default, default, default, default), false, ignoreProperties: [
                "a",// Always null
                "f",// Always null
                "l" // Always null
                ]);
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetKlinesAsync("ETHUSDT",Enums.KlineInterval.OneDay, default, default, default, default), false);
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetAveragePriceAsync("ETHUSDT", default), false);
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetTickerAsync("ETHUSDT", default), false,
                ignoreProperties: [
                    "count" // always null
                    ]);
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetPricesAsync(default, default), false);
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetBookPricesAsync("ETHUSDT", default), false);
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetBookPricesAsync(default), false);
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public async Task TestSpotTrading()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(warnings, client => client.SpotApi.Trading.GetOpenOrdersAsync("ETHUSDT", default), true);
            await RunAndCheckResult(warnings, client => client.SpotApi.Trading.GetOrdersAsync("ETHUSDT", default, default, default, default), true);
            await RunAndCheckResult(warnings, client => client.SpotApi.Trading.GetUserTradesAsync("ETHUSDT", default, default, default, default, default), true);
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }


        [Test]
        public async Task TestFuturesAccount()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(warnings, client => client.FuturesApi.Account.GetBalanceAsync("USDC", default), true, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.Account.GetBalancesAsync(default), true, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.Account.GetTransferHistoryAsync(default, default, default, default, default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.Account.GetFundingHistoryAsync(default, default, default, default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.Account.GetTradingFeesAsync("ETH_USDC", default), true, "data", ignoreProperties: ["leverageFeeRates", "tieredFeeRates", "inviterKyc"]);
            await RunAndCheckResult(warnings, client => client.FuturesApi.Account.GetLeverageAsync("ETH_USDC", default), true,  "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.Account.GetPositionModeAsync(default), true, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.Account.GetProfitRateAsync(Enums.ProfitPeriod.Day, default), true, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.Account.GetDeductionConfigAsync(default), true, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.Account.GetDiscountTypesAsync(default), true, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.Account.GetZeroFeeSymbolsAsync(default, default), true, "data", ["hotRecs", "soonEffectiveContracts", "soonEffectiveHotRecs"]);

            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public async Task TestFuturesExchangeData()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(client => client.FuturesApi.ExchangeData.GetServerTimeAsync(default), false);
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetSymbolsAsync(default), false, compareNestedProperty: "data", ignoreProperties: ["tagIdList", "typeLabel", "leverageFeeRates", "tieredFeeRates", "deliveryPriceTrend"]);
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetTransferableAssetsAsync(default), false, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetOrderBookAsync("ETH_USDC", default, default), false, "data", ignoreProperties: ["cts"]);
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetIndexPriceAsync("ETH_USDC", default), false, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetMarkPriceAsync("ETH_USDC", default), false, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetFundingRateAsync("ETH_USDC", default), false, "data");
            await RunAndCheckResult(client => client.FuturesApi.ExchangeData.GetKlinesAsync("ETH_USDC", Enums.FuturesKlineInterval.OneDay, default, default, default), false);
            await RunAndCheckResult(client => client.FuturesApi.ExchangeData.GetIndexPriceKlinesAsync("ETH_USDC", Enums.FuturesKlineInterval.OneDay, default, default, default), false);
            await RunAndCheckResult(client => client.FuturesApi.ExchangeData.GetMarkPriceKlinesAsync("ETH_USDC", Enums.FuturesKlineInterval.OneDay, default, default, default), false);
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetRecentTradesAsync("ETH_USDC", default, default), false, "data", ignoreProperties: ["O", "cts"]);
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetTickerAsync("ETH_USDC", default), false, "data", ["riseFallRates", "riseFallRatesOfTimezone"]);
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetTickersAsync(default), false, "data", ["riseFallRates", "riseFallRatesOfTimezone"]);
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetRiskFundBalancesAsync(default), false, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetRiskFundBalanceHistoryAsync("ETH_USDC", default, default, default), false, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetFundingRateHistoryAsync("ETH_USDC", default, default, default), false, "data");

            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public async Task TestFuturesTrading()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(warnings, client => client.FuturesApi.Trading.GetOpenOrdersAsync(default, default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.Trading.GetOrderHistoryAsync(default, default, default, default, default, default, default, default, default), true, "data", ignoreProperties: ["priceStr", "dealAvgPriceStr", "showCancelReason", "showProfitRateShare", "zeroSaveTotalFeeBinance", "zeroTradeTotalFeeBinance"]);
            await RunAndCheckResult(warnings, client => client.FuturesApi.Trading.GetUserTradesAsync("ETH_USDC", default, default, default, default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.Trading.GetTriggerOrdersAsync(default, default, default, default, default, default, default), true, "data", ["ensureStopLoss"]);
            await RunAndCheckResult(warnings, client => client.FuturesApi.Trading.GetTpSlOrdersAsync(default, default, default, default , default , default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.Trading.GetRiskLimitsAsync(default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.Trading.GetPositionHistoryAsync(default, default, default, default, default), true, "data", ["zeroSaveTotalFeeBinance", "zeroTradeTotalFeeBinance", "deductFeeList", "positionShowStatus", "holdAvgPriceFullyScale", "openAvgPriceFullyScale"]);
            await RunAndCheckResult(warnings, client => client.FuturesApi.Trading.GetPositionsAsync(default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.Trading.GetOpenOrderCountsAsync(default), true, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.Trading.GetOpenTpSlOrdersAsync(default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.Trading.GetTrailingOrdersAsync(default, default, default, default, default, default, default, default), true, "data");

            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public async Task TestOrderBooks()
        {
            await TestOrderBook(new MexcSpotSymbolOrderBook("ETHUSDT"));
        }
    }
}
