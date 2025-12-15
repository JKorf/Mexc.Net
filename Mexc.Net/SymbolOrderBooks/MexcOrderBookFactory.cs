using CryptoExchange.Net.OrderBook;
using CryptoExchange.Net.SharedApis;
using Mexc.Net.Interfaces;
using Mexc.Net.Interfaces.Clients;
using Mexc.Net.Objects.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Mexc.Net.SymbolOrderBooks
{
    /// <summary>
    /// Mexc order book factory
    /// </summary>
    public class MexcOrderBookFactory : IMexcOrderBookFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <inheritdoc />
        public string ExchangeName => MexcExchange.ExchangeName;

        /// <inheritdoc />
        public IOrderBookFactory<MexcOrderBookOptions> Spot { get; }
        /// <inheritdoc />
        public IOrderBookFactory<MexcOrderBookOptions> Futures { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public MexcOrderBookFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            Spot = new OrderBookFactory<MexcOrderBookOptions>(CreateSpot, Create);
            Futures = new OrderBookFactory<MexcOrderBookOptions>(CreateFutures, Create);
        }

        /// <inheritdoc />
        public ISymbolOrderBook Create(SharedSymbol symbol, Action<MexcOrderBookOptions>? options = null)
        {
            var symbolName = symbol.GetSymbol(MexcExchange.FormatSymbol);
            if (symbol.TradingMode == TradingMode.Spot)
                return CreateSpot(symbolName, options);

            return CreateFutures(symbolName, options);
        }

        /// <inheritdoc />
        public ISymbolOrderBook CreateSpot(string symbol, Action<MexcOrderBookOptions>? options = null)
            => new MexcSpotSymbolOrderBook(symbol,
                                             options,
                                             _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                             _serviceProvider.GetRequiredService<IMexcRestClient>(),
                                             _serviceProvider.GetRequiredService<IMexcSocketClient>());

        /// <inheritdoc />
        public ISymbolOrderBook CreateFutures(string symbol, Action<MexcOrderBookOptions>? options = null)
            => new MexcFuturesSymbolOrderBook(symbol,
                                             options,
                                             _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                             _serviceProvider.GetRequiredService<IMexcRestClient>(),
                                             _serviceProvider.GetRequiredService<IMexcSocketClient>());
    }
}
