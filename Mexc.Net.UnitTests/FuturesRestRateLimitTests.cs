using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net;
using CryptoExchange.Net.Objects;
using Mexc.Net.Clients;
using Mexc.Net.Enums;
using Mexc.Net.Objects.Options;
using Microsoft.Extensions.Options;

namespace Mexc.Net.UnitTests
{
    [TestFixture]
    public class FuturesRestRateLimitTests
    {
        private static MexcRestClient CreateClient(HttpClient httpClient, string group, RateLimitingBehaviour behaviour = RateLimitingBehaviour.Fail)
            => new(httpClient, null, Options.Create(new MexcRestOptions
            {
                AutoTimestamp = false,
                ApiCredentials = new MexcCredentials().WithHMAC("test-key", "test-secret"),
                RateLimitGroup = group,
                RateLimitingBehaviour = behaviour,
                Environment = MexcEnvironment.CreateCustom(group, "https://spot.example", "wss://spot.example", $"https://{group}.example", "wss://futures.example")
            }));

        private static IEnumerable<TestCaseData> VariablePathRequests()
        {
            yield return Case("IndexPrice", async (client, i) => await client.FuturesApi.ExchangeData.GetIndexPriceAsync($"TEST{i}_USDT"));
            yield return Case("MarkPrice", async (client, i) => await client.FuturesApi.ExchangeData.GetMarkPriceAsync($"TEST{i}_USDT"));
            yield return Case("FundingRate", async (client, i) => await client.FuturesApi.ExchangeData.GetFundingRateAsync($"TEST{i}_USDT"));
            yield return Case("Klines", async (client, i) => await client.FuturesApi.ExchangeData.GetKlinesAsync($"TEST{i}_USDT", FuturesKlineInterval.OneMinute));
            yield return Case("IndexPriceKlines", async (client, i) => await client.FuturesApi.ExchangeData.GetIndexPriceKlinesAsync($"TEST{i}_USDT", FuturesKlineInterval.OneMinute));
            yield return Case("MarkPriceKlines", async (client, i) => await client.FuturesApi.ExchangeData.GetMarkPriceKlinesAsync($"TEST{i}_USDT", FuturesKlineInterval.OneMinute));
            yield return Case("RecentTrades", async (client, i) => await client.FuturesApi.ExchangeData.GetRecentTradesAsync($"TEST{i}_USDT"));
            yield return Case("Balance", async (client, i) => await client.FuturesApi.Account.GetBalanceAsync($"ASSET{i}"));
            yield return Case("ProfitRate", async (client, i) => await client.FuturesApi.Account.GetProfitRateAsync(i % 2 == 0 ? ProfitPeriod.Day : ProfitPeriod.Week), distinctPaths: 2);
            yield return Case("OrderByClientOrderId", async (client, i) => await client.FuturesApi.Trading.GetOrderByClientOrderIdAsync($"TEST{i}_USDT", $"client-{i}"));
            yield return Case("Order", async (client, i) => await client.FuturesApi.Trading.GetOrderAsync(i + 1));
            yield return Case("OrderTrades", async (client, i) => await client.FuturesApi.Trading.GetOrderTradesAsync(i + 1));
        }

        private static TestCaseData Case(string name, Func<MexcRestClient, int, Task<ICallResult>> send, int distinctPaths = 20)
            => new TestCaseData(send, distinctPaths).SetName($"FuturesRest_{name}_SharesBudgetAcrossPathValuesAndClients_TestAsync");

        private static void AssertSent(ICallResult result)
            => Assert.That(result.Error?.Message, Is.EqualTo("Simulated response"), "An admitted request must reach the fake HTTP server.");

        [TestCaseSource(nameof(VariablePathRequests))]
        public async Task FuturesRest_VariablePath_SharesBudgetAcrossPathValuesAndClients_TestAsync(Func<MexcRestClient, int, Task<ICallResult>> send, int distinctPaths)
        {
            // Each URL value previously created another limiter, so a burst could exceed the endpoint's budget.
            // Arrange
            using var handler = new RecordingHandler();
            using var httpClient = new HttpClient(handler);
            var group = Guid.NewGuid().ToString("N");
            using var first = CreateClient(httpClient, group);
            using var second = CreateClient(httpClient, group);
            for (var i = 0; i < 20; i++)
                AssertSent(await send(i % 2 == 0 ? first : second, i));

            // Act
            var result = await send(second, 20);

            // Assert
            Assert.That(result.Error, Is.InstanceOf<BaseRateLimitError>(), "Changing a URL value must not grant another endpoint budget.");
            Assert.That(handler.Requests.Count, Is.EqualTo(20), "Only twenty requests may reach HTTP in this window.");
            Assert.That(handler.Requests.Select(uri => uri.AbsolutePath).Distinct().Count(), Is.EqualTo(distinctPaths), "Sharing the budget must preserve the requested URL values.");
        }

        [Test]
        public async Task GetTickerAsync_CombinedWithGetTickersAsync_SharesTenRequestBudget_TestAsync()
        {
            // Arrange
            using var handler = new RecordingHandler();
            using var httpClient = new HttpClient(handler);
            var group = Guid.NewGuid().ToString("N");
            using var first = CreateClient(httpClient, group);
            using var second = CreateClient(httpClient, group);
            for (var i = 0; i < 5; i++)
            {
                AssertSent(await first.FuturesApi.ExchangeData.GetTickerAsync($"TEST{i}_USDT"));
                AssertSent(await second.FuturesApi.ExchangeData.GetTickersAsync());
            }

            // Act
            var result = await first.FuturesApi.ExchangeData.GetTickerAsync("NEXT_USDT");

            // Assert
            Assert.That(result.Error, Is.InstanceOf<BaseRateLimitError>(), "Single-symbol and all-symbol tickers share ten requests per two seconds.");
            Assert.That(handler.Requests.Count, Is.EqualTo(10), "The eleventh ticker request must not reach HTTP.");
        }

        [Test]
        public async Task SetLeverageAsync_ExhaustedBudget_WaitsForTenSecondWindow_TestAsync()
        {
            // Arrange
            using var handler = new RecordingHandler();
            using var httpClient = new HttpClient(handler);
            var group = Guid.NewGuid().ToString("N");
            using var first = CreateClient(httpClient, group);
            using var second = CreateClient(httpClient, group, behaviour: RateLimitingBehaviour.Wait);
            using var cancellation = new CancellationTokenSource(TimeSpan.FromSeconds(20));
            var startedAt = Stopwatch.GetTimestamp();
            for (var i = 0; i < 10; i++)
                AssertSent(await first.FuturesApi.Account.SetLeverageAsync(2, positionId: i + 1));

            // Act
            var rejected = await first.FuturesApi.Account.SetLeverageAsync(2, positionId: 11);

            // Assert
            Assert.That(rejected.Error, Is.InstanceOf<BaseRateLimitError>(), "Only ten leverage changes may start in the window.");
            Assert.That(handler.Requests.Count, Is.EqualTo(10), "A rejected change must not reach HTTP.");
            AssertSent(await second.FuturesApi.Account.SetLeverageAsync(2, positionId: 12, ct: cancellation.Token));
            Assert.That(Stopwatch.GetElapsedTime(startedAt), Is.GreaterThanOrEqualTo(TimeSpan.FromSeconds(10)), "The leverage budget must recover after ten seconds, not two.");
            Assert.That(handler.Requests.Count, Is.EqualTo(11), "The waiting change must reach HTTP after recovery.");
        }

        [Test]
        public async Task PlaceOrderAsync_ExhaustedBudget_PreservesIndependentCancelAndBatchQueryBudgets_TestAsync()
        {
            // Arrange
            using var handler = new RecordingHandler();
            using var httpClient = new HttpClient(handler);
            var group = Guid.NewGuid().ToString("N");
            using var first = CreateClient(httpClient, group);
            using var second = CreateClient(httpClient, group);
            for (var i = 0; i < 4; i++)
                AssertSent(await (i % 2 == 0 ? first : second).FuturesApi.Trading.PlaceOrderAsync($"TEST{i}_USDT", FuturesOrderSide.OpenLong, type: FuturesOrderType.Limit, quantity: 1, price: 1));

            // Act
            var rejected = await second.FuturesApi.Trading.PlaceOrderAsync("NEXT_USDT", FuturesOrderSide.OpenLong, type: FuturesOrderType.Limit, quantity: 1, price: 1);
            for (var i = 0; i < 5; i++)
                AssertSent(await second.FuturesApi.Trading.GetOrdersByIdAsync(new long[] { i + 1 }));
            var rejectedQuery = await first.FuturesApi.Trading.GetOrdersByIdAsync(new long[] { 6 });
            for (var i = 0; i < 20; i++)
                AssertSent(await first.FuturesApi.Trading.CancelOrdersAsync(new long[] { i + 1 }));
            var rejectedCancel = await second.FuturesApi.Trading.CancelOrdersAsync(new long[] { 21 });

            // Assert
            Assert.That(rejected.Error, Is.InstanceOf<BaseRateLimitError>(), "The four-request order budget must remain enforced.");
            Assert.That(rejectedQuery.Error, Is.InstanceOf<BaseRateLimitError>(), "Batch queries have a separate five-request budget.");
            Assert.That(rejectedCancel.Error, Is.InstanceOf<BaseRateLimitError>(), "Cancellation retains its separate twenty-request budget.");
            Assert.That(handler.Requests.Count, Is.EqualTo(29), "Each endpoint must admit exactly its own budget.");
        }

        [Test]
        public async Task GetKlinesAsync_ExhaustedBudget_DoesNotLimitIndexOrMarkKlines_TestAsync()
        {
            // Arrange
            using var handler = new RecordingHandler();
            using var httpClient = new HttpClient(handler);
            var group = Guid.NewGuid().ToString("N");
            using var client = CreateClient(httpClient, group);
            for (var i = 0; i < 20; i++)
                AssertSent(await client.FuturesApi.ExchangeData.GetKlinesAsync("TEST_USDT", FuturesKlineInterval.OneMinute));

            // Act
            var rejected = await client.FuturesApi.ExchangeData.GetKlinesAsync("TEST_USDT", FuturesKlineInterval.OneMinute);
            var index = await client.FuturesApi.ExchangeData.GetIndexPriceKlinesAsync("TEST_USDT", FuturesKlineInterval.OneMinute);
            var mark = await client.FuturesApi.ExchangeData.GetMarkPriceKlinesAsync("TEST_USDT", FuturesKlineInterval.OneMinute);

            // Assert
            Assert.That(rejected.Error, Is.InstanceOf<BaseRateLimitError>(), "The ordinary candle budget must be exhausted.");
            AssertSent(index);
            AssertSent(mark);
            Assert.That(handler.Requests.Count, Is.EqualTo(22), "The three candle endpoints must have independent budgets despite their common URL prefix.");
        }

        [Test]
        public async Task GetFundingRateAsync_ExhaustedBudget_DoesNotLimitFundingHistoryOrAllRates_TestAsync()
        {
            // Funding history shares the symbol endpoint's URL prefix but must retain an independent budget.
            // Arrange
            using var handler = new RecordingHandler();
            using var httpClient = new HttpClient(handler);
            var group = Guid.NewGuid().ToString("N");
            using var client = CreateClient(httpClient, group);
            for (var i = 0; i < 20; i++)
                AssertSent(await client.FuturesApi.ExchangeData.GetFundingRateAsync($"TEST{i}_USDT"));

            // Act
            var rejectedRate = await client.FuturesApi.ExchangeData.GetFundingRateAsync("NEXT_USDT");
            for (var i = 0; i < 20; i++)
                AssertSent(await client.FuturesApi.ExchangeData.GetFundingRateHistoryAsync($"TEST{i}_USDT"));
            var rejectedHistory = await client.FuturesApi.ExchangeData.GetFundingRateHistoryAsync("NEXT_USDT");
            var allRates = await client.FuturesApi.ExchangeData.GetFundingRatesAsync();

            // Assert
            Assert.That(rejectedRate.Error, Is.InstanceOf<BaseRateLimitError>(), "The per-symbol funding endpoint must share its twenty-request budget.");
            Assert.That(rejectedHistory.Error, Is.InstanceOf<BaseRateLimitError>(), "Funding history must enforce its independent twenty-request budget.");
            AssertSent(allRates);
            Assert.That(handler.Requests.Count, Is.EqualTo(41), "Both endpoint budgets and the independent all-rates request must reach HTTP.");
        }

        [Test]
        public async Task GetSymbolAsync_CombinedWithGetSymbolsAsync_SharesLegacyContractBudget_TestAsync()
        {
            // Arrange
            using var handler = new RecordingHandler();
            using var httpClient = new HttpClient(handler);
            var group = Guid.NewGuid().ToString("N");
            using var first = CreateClient(httpClient, group);
            using var second = CreateClient(httpClient, group);
            AssertSent(await first.FuturesApi.ExchangeData.GetSymbolAsync("TEST_USDT"));

            // Act
            var result = await second.FuturesApi.ExchangeData.GetSymbolsAsync();

            // Assert
            Assert.That(result.Error, Is.InstanceOf<BaseRateLimitError>(), "Both methods use the same legacy contract endpoint and budget.");
            Assert.That(handler.Requests.Count, Is.EqualTo(1), "The second contract request must not reach HTTP.");
        }

        private sealed class RecordingHandler : HttpMessageHandler
        {
            public ConcurrentQueue<Uri> Requests { get; } = new();

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                Requests.Enqueue(request.RequestUri);
                // A uniform error response exercises admission for every native method without requiring unrelated response models.
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("{\"success\":false,\"code\":123456,\"message\":\"Simulated response\"}")
                });
            }
        }
    }
}
