using CryptoExchange.Net.Interfaces;
using Mexc.Net.Objects.Options;
using System;

namespace Mexc.Net.Interfaces
{
    /// <summary>
    /// Mexc order book factory
    /// </summary>
    public interface IMexcOrderBookFactory
    {
        /// <summary>
        /// Spot order book factory methods
        /// </summary>
        public IOrderBookFactory<MexcOrderBookOptions> Spot { get; }

        /// <summary>
        /// Create a new spot SymbolOrderBook
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        ISymbolOrderBook CreateSpot(string symbol, Action<MexcOrderBookOptions>? options = null);
    }
}