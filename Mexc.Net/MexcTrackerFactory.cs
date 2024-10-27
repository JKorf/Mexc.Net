using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.Klines;
using CryptoExchange.Net.Trackers.Trades;
using Mexc.Net.Clients;
using Mexc.Net.Interfaces;
using Mexc.Net.Interfaces.Clients;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Mexc.Net
{
    /// <inheritdoc />
    public class MexcTrackerFactory : IMexcTrackerFactory
    {
        private readonly IServiceProvider? _serviceProvider;

        /// <summary>
        /// ctor
        /// </summary>
        public MexcTrackerFactory()
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public MexcTrackerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc />
        public IKlineTracker CreateKlineTracker(SharedSymbol symbol, SharedKlineInterval interval, int? limit = null, TimeSpan? period = null)
        {
            var restClient = (_serviceProvider?.GetRequiredService<IMexcRestClient>() ?? new MexcRestClient()).SpotApi.SharedClient;
            var socketClient = (_serviceProvider?.GetRequiredService<IMexcSocketClient>() ?? new MexcSocketClient()).SpotApi.SharedClient;

            return new KlineTracker(
                _serviceProvider?.GetRequiredService<ILoggerFactory>().CreateLogger(restClient.Exchange),
                restClient,
                socketClient,
                symbol,
                interval,
                limit,
                period
                );
        }
        /// <inheritdoc />
        public ITradeTracker CreateTradeTracker(SharedSymbol symbol, int? limit = null, TimeSpan? period = null)
        {
            var restClient = (_serviceProvider?.GetRequiredService<IMexcRestClient>() ?? new MexcRestClient()).SpotApi.SharedClient;
            var socketClient = (_serviceProvider?.GetRequiredService<IMexcSocketClient>() ?? new MexcSocketClient()).SpotApi.SharedClient;

            return new TradeTracker(
                _serviceProvider?.GetRequiredService<ILoggerFactory>().CreateLogger(restClient.Exchange),
                restClient,
                null,
                socketClient,
                symbol,
                limit,
                period
                );
        }
    }
}
