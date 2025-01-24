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
        public IOrderBookFactory<MexcOrderBookOptions> Spot { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public MexcOrderBookFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            Spot = new OrderBookFactory<MexcOrderBookOptions>(CreateSpot, Create);
        }

        /// <inheritdoc />
        public ISymbolOrderBook Create(SharedSymbol symbol, Action<MexcOrderBookOptions>? options = null)
        {
            var symbolName = symbol.GetSymbol(MexcExchange.FormatSymbol);
            return CreateSpot(symbolName, options);
        }

        /// <inheritdoc />
        public ISymbolOrderBook CreateSpot(string symbol, Action<MexcOrderBookOptions>? options = null)
            => new MexcSpotSymbolOrderBook(symbol,
                                             options,
                                             _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                             _serviceProvider.GetRequiredService<IMexcRestClient>(),
                                             _serviceProvider.GetRequiredService<IMexcSocketClient>());
    }
}
