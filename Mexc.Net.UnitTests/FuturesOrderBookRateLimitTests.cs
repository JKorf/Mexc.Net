using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net;
using CryptoExchange.Net.Objects;
using Mexc.Net.Clients;
using Mexc.Net.Objects.Options;
using Microsoft.Extensions.Options;

namespace Mexc.Net.UnitTests
{
    [TestFixture]
    public class FuturesOrderBookRateLimitTests
    {
        private static MexcRestClient CreateClient(HttpClient httpClient, string group, RateLimitingBehaviour behaviour = RateLimitingBehaviour.Fail, MexcEnvironment environment = null)
            => new(httpClient, null, Options.Create(new MexcRestOptions
            {
                AutoTimestamp = false,
                RateLimitGroup = group,
                RateLimitingBehaviour = behaviour,
                Environment = environment ?? MexcEnvironment.Live
            }));

        private static async Task FillBudgetAsync(MexcRestClient first, MexcRestClient second, string symbolPrefix, bool distinctSymbols = true)
        {
            for (var i = 0; i < 10; i++)
            {
                var client = i % 2 == 0 ? first : second;
                var symbol = symbolPrefix + (distinctSymbols ? i : 0) + "_USDT";
                var result = await client.FuturesApi.ExchangeData.GetOrderBookAsync(symbol);
                Assert.That(result.Success, Is.True, $"Snapshot {i + 1} must fit in the shared budget: {result.Error}");
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task GetOrderBookAsync_EleventhRequestAcrossClients_IsLimited_TestAsync(bool distinctSymbols)
        {
            // Different symbols and separate clients must not each receive a new depth budget.
            // Arrange
            using var handler = new SnapshotHandler();
            using var httpClient = new HttpClient(handler);
            var group = Guid.NewGuid().ToString("N");
            using var first = CreateClient(httpClient, group);
            using var second = CreateClient(httpClient, group);
            await FillBudgetAsync(first, second, symbolPrefix: group, distinctSymbols: distinctSymbols);

            // Act
            var result = await second.FuturesApi.ExchangeData.GetOrderBookAsync(group + (distinctSymbols ? 10 : 0) + "_USDT");

            // Assert
            Assert.That(result.Error, Is.InstanceOf<BaseRateLimitError>(), "The eleventh snapshot must be rejected by the local limiter.");
            Assert.That(handler.Requests.Count, Is.EqualTo(10), "Only ten HTTP requests may be sent in the window.");
            Assert.That(handler.Requests.Select(request => request.Uri.AbsolutePath).Distinct().Count(), Is.EqualTo(distinctSymbols ? 10 : 1), "Budget sharing must preserve each request's symbol in its URL.");
        }

        [Test]
        public async Task GetOrderBookAsync_WaitingForBudget_AllowsOverlappingRequestsAndResumes_TestAsync()
        {
            // Keep ten responses pending: admission should limit request starts, not serialize HTTP responses.
            // Arrange
            var releaseResponses = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
            using var handler = new SnapshotHandler(ct => releaseResponses.Task.WaitAsync(ct));
            using var httpClient = new HttpClient(handler);
            var group = Guid.NewGuid().ToString("N");
            using var first = CreateClient(httpClient, group, behaviour: RateLimitingBehaviour.Wait);
            using var second = CreateClient(httpClient, group, behaviour: RateLimitingBehaviour.Wait);
            using var cancellation = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            var startedAt = Stopwatch.GetTimestamp();
            var requests = Enumerable.Range(0, 10)
                .Select(i => (i % 2 == 0 ? first : second).FuturesApi.ExchangeData.GetOrderBookAsync(group + i + "_USDT", ct: cancellation.Token))
                .ToList();

            try
            {
                await handler.TenRequestsStarted.Task.WaitAsync(cancellation.Token);

                // Act
                requests.Add(second.FuturesApi.ExchangeData.GetOrderBookAsync(group + "10_USDT", ct: cancellation.Token));

                // Assert
                Assert.That(handler.Requests.Count, Is.EqualTo(10), "The eleventh request must wait for the shared window.");
                await handler.ElevenRequestsStarted.Task.WaitAsync(cancellation.Token);
                Assert.That(Stopwatch.GetElapsedTime(startedAt, handler.Requests.Last().Timestamp), Is.GreaterThanOrEqualTo(TimeSpan.FromSeconds(2)), "The next snapshot may start only after the two-second budget recovers.");
                Assert.That(requests.All(request => !request.IsCompleted), Is.True, "Requests must overlap while their HTTP responses are pending.");
            }
            finally
            {
                releaseResponses.TrySetResult();
                var results = await Task.WhenAll(requests);
                Assert.That(results.All(result => result.Success), Is.True, "Admitted requests must return their snapshots successfully.");
            }
        }

        [Test]
        public async Task GetOrderBookAsync_CanceledBudgetWait_DoesNotSendOrResetBudget_TestAsync()
        {
            // Arrange
            using var handler = new SnapshotHandler();
            using var httpClient = new HttpClient(handler);
            var group = Guid.NewGuid().ToString("N");
            using var first = CreateClient(httpClient, group);
            using var second = CreateClient(httpClient, group, behaviour: RateLimitingBehaviour.Wait);
            await FillBudgetAsync(first, second, symbolPrefix: group);
            using var cancellation = new CancellationTokenSource();

            // Act
            var pending = second.FuturesApi.ExchangeData.GetOrderBookAsync(group + "10_USDT", ct: cancellation.Token);
            cancellation.Cancel();
            var canceled = await pending;
            var next = await first.FuturesApi.ExchangeData.GetOrderBookAsync(group + "11_USDT");

            // Assert
            Assert.That(canceled.Error, Is.InstanceOf<CancellationRequestedError>(), "Canceling the wait must return cancellation without making an HTTP request.");
            Assert.That(next.Error, Is.InstanceOf<BaseRateLimitError>(), "Canceling one waiter must leave the shared budget exhausted.");
            Assert.That(handler.Requests.Count, Is.EqualTo(10), "Neither the canceled nor the rejected request may reach HTTP.");
        }

        [Test]
        public async Task GetOrderBookAsync_ExhaustedBudget_DoesNotLimitOtherFuturesEndpoints_TestAsync()
        {
            // Arrange
            using var handler = new SnapshotHandler();
            using var httpClient = new HttpClient(handler);
            var group = Guid.NewGuid().ToString("N");
            using var client = CreateClient(httpClient, group);
            await FillBudgetAsync(client, client, symbolPrefix: group);

            // Act
            var result = await client.FuturesApi.ExchangeData.GetIndexPriceAsync(group + "_USDT");

            // Assert
            Assert.That(result.Success, Is.True, "Exhausting depth requests must not consume the index-price budget.");
            Assert.That(handler.Requests.Count, Is.EqualTo(11), "The unrelated endpoint must still reach HTTP.");
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task GetOrderBookAsync_SeparateHostOrRateLimitGroup_HasIndependentBudget_TestAsync(bool separateHost)
        {
            // Arrange
            using var handler = new SnapshotHandler();
            using var httpClient = new HttpClient(handler);
            var group = Guid.NewGuid().ToString("N");
            using var first = CreateClient(httpClient, group);
            await FillBudgetAsync(first, first, symbolPrefix: group);
            var environment = separateHost ? MexcEnvironment.CreateCustom("other", "https://spot.example", "wss://spot.example", "https://futures.example", "wss://futures.example") : null;
            using var second = CreateClient(httpClient, separateHost ? group : group + "-other", environment: environment);

            // Act
            var result = await second.FuturesApi.ExchangeData.GetOrderBookAsync(group + "10_USDT");

            // Assert
            Assert.That(result.Success, Is.True, "A different host or rate-limit group must retain its own budget.");
            Assert.That(handler.Requests.Count, Is.EqualTo(11), "The independent request must reach HTTP.");
        }

        private sealed class SnapshotHandler(Func<CancellationToken, Task> beforeResponse = null) : HttpMessageHandler
        {
            public ConcurrentQueue<(Uri Uri, long Timestamp)> Requests { get; } = new();
            public TaskCompletionSource TenRequestsStarted { get; } = new(TaskCreationOptions.RunContinuationsAsynchronously);
            public TaskCompletionSource ElevenRequestsStarted { get; } = new(TaskCreationOptions.RunContinuationsAsynchronously);

            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                Requests.Enqueue((request.RequestUri, Stopwatch.GetTimestamp()));
                if (Requests.Count == 10)
                    TenRequestsStarted.TrySetResult();
                if (Requests.Count == 11)
                    ElevenRequestsStarted.TrySetResult();
                if (beforeResponse != null)
                    await beforeResponse(cancellationToken);

                var data = request.RequestUri.AbsolutePath.Contains("/index_price/")
                    ? "{\"symbol\":\"TEST_USDT\",\"indexPrice\":1,\"timestamp\":1700000000000}"
                    : "{\"asks\":[[2,1,1]],\"bids\":[[1,1,1]],\"version\":1,\"timestamp\":1700000000000}";
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("{\"success\":true,\"code\":0,\"data\":" + data + "}")
                };
            }
        }
    }
}
