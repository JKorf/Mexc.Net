using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Mexc.Net.Clients;
using Mexc.Net.Objects.Options;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Mexc.Net.Benchmark.Client
{
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net48)]
    //[SimpleJob(RuntimeMoniker.Net90)]
    [SimpleJob(RuntimeMoniker.Net10_0)]
    public class Tester
    {
        public MexcSocketClient SocketClient;
        public MexcRestClient RestClient;
        public ILogger Logger;

        private const int _socketUpdateReceiveTarget = 10000; // Should match the number in the server


        [GlobalSetup(Targets = [nameof(SocketUpdated), nameof(RestUpdated)])]
        public void SetupUpdatedDeserialization()
        {
            CreateClient(true);
        }

        [GlobalSetup(Targets = [nameof(SocketLegacy), nameof(RestLegacy)])]
        public void SetupLegacyDeserialization()
        {
            CreateClient(false);
        }

        [Benchmark()]
        public async Task SocketUpdated()
        {
            var waitEvent = new AsyncResetEvent(false, false);
            var received = 0;
            var result = await SocketClient.SpotApi.SubscribeToTradeUpdatesAsync("ETHUSDT", x =>
            {
                received++;
                if (received == _socketUpdateReceiveTarget)
                    waitEvent.Set();

            }, CancellationToken.None);

            await waitEvent.WaitAsync();
            await result.Data.CloseAsync();
        }

        [Benchmark()]
        public async Task SocketLegacy()
        {
            var waitEvent = new AsyncResetEvent(false, false);
            var received = 0;
            var result = await SocketClient.SpotApi.SubscribeToTradeUpdatesAsync("ETHUSDT", x =>
            {
                received++;
                if (received == _socketUpdateReceiveTarget)
                    waitEvent.Set();

            }, CancellationToken.None);

            await waitEvent.WaitAsync();
            await result.Data.CloseAsync();
        }


        //[Benchmark()]
        public async Task RestUpdated()
        {
            for (var i = 0; i < 1000; i++)
            {
                var result = await RestClient.SpotApi.ExchangeData.GetServerTimeAsync();
            }
        }


        //[Benchmark()]
        public async Task RestLegacy()
        {
            for(var i = 0; i < 1000; i++)
            {
                var result = await RestClient.SpotApi.ExchangeData.GetServerTimeAsync();
            }
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            SocketClient.Dispose();
            RestClient.Dispose();
        }

        private void CreateClient(bool enableNewDeserialization)
        {
            var logger = new LoggerFactory();
            logger.AddProvider(new TraceLoggerProvider(LogLevel.Trace));
            var env = MexcEnvironment.CreateCustom("Benchmark", "http://localhost:57589", "ws://localhost:57589/ws", "", "");
            SocketClient = new MexcSocketClient(Options.Create(new MexcSocketOptions
            {
                ReconnectPolicy = ReconnectPolicy.Disabled,
                UseUpdatedDeserialization = enableNewDeserialization,
                RateLimiterEnabled = false,
                Environment = env
            }), logger);
            RestClient = new MexcRestClient(null, logger, Options.Create(new MexcRestOptions
            {
                UseUpdatedDeserialization = enableNewDeserialization,
                RateLimiterEnabled = false,
                Environment = env
            }));
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // For manual testing:

            //var test = new Tester();
            //test.SetupUpdatedDeserialization();
            //Console.ReadLine();
            //Console.WriteLine("Starting");
            //for (var i = 0; i < 1; i++)
            //{
            //    test.SocketUpdated().Wait();
            //}
            //Console.WriteLine("Finished");
            //Console.ReadLine();
            //test.GlobalCleanup();

            BenchmarkRunner.Run<Tester>();
        }
    }
}