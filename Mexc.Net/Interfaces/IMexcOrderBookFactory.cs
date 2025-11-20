using CryptoExchange.Net.SharedApis;
using Mexc.Net.Objects.Options;

namespace Mexc.Net.Interfaces
{
    /// <summary>
    /// Mexc order book factory
    /// </summary>
    public interface IMexcOrderBookFactory : IExchangeService
    {
        /// <summary>
        /// Spot order book factory methods
        /// </summary>
        public IOrderBookFactory<MexcOrderBookOptions> Spot { get; }

        /// <summary>
        /// Create a SymbolOrderBook for the symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="options">Book options</param>
        /// <returns></returns>
        ISymbolOrderBook Create(SharedSymbol symbol, Action<MexcOrderBookOptions>? options = null);

        /// <summary>
        /// Create a new spot SymbolOrderBook
        /// </summary>
        ISymbolOrderBook CreateSpot(string symbol, Action<MexcOrderBookOptions>? options = null);
        /// <summary>
        /// Create a new futures SymbolOrderBook
        /// </summary>
        ISymbolOrderBook CreateFutures(string symbol, Action<MexcOrderBookOptions>? options = null);
    }
}