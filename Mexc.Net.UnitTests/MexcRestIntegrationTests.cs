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
        public override bool Run { get; set; } = false;

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
                ApiCredentials = Authenticated ? new ApiCredentials(key, sec) : null
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
            await RunAndCheckResult(client => client.SpotApi.Account.GetAccountInfoAsync(default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetUserAssetsAsync(default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetDepositHistoryAsync(default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetWithdrawHistoryAsync(default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetWithdrawAddressesAsync(default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetTransferHistoryAsync(Enums.AccountType.Spot, Enums.AccountType.Futures, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetAssetsForDustTransferAsync(default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetDustLogAsync(default, default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetMxDeductionStatusAsync(default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetTradeFeeAsync("ETHUSDT", default), true);
        }

        [Test]
        public async Task TestSpotExchangeData()
        {
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetServerTimeAsync(default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetApiSymbolsAsync(default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetExchangeInfoAsync(default, default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetOrderBookAsync("ETHUSDT", default, default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetRecentTradesAsync("ETHUSDT", default, default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetAggregatedTradeHistoryAsync("ETHUSDT", default, default, default, default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetKlinesAsync("ETHUSDT",Enums.KlineInterval.OneDay, default, default, default, default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetAveragePriceAsync("ETHUSDT", default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetTickerAsync("ETHUSDT", default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetPricesAsync(default, default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetBookPricesAsync("ETHUSDT", default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetBookPricesAsync(default), false);
        }

        [Test]
        public async Task TestSpotTrading()
        {
            await RunAndCheckResult(client => client.SpotApi.Trading.GetOpenOrdersAsync("ETHUSDT", default), true);
            await RunAndCheckResult(client => client.SpotApi.Trading.GetOrdersAsync("ETHUSDT", default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Trading.GetUserTradesAsync("ETHUSDT", default, default, default, default, default), true);
        }

        [Test]
        public async Task TestOrderBooks()
        {
            await TestOrderBook(new MexcSpotSymbolOrderBook("ETHUSDT"));
        }
    }
}
