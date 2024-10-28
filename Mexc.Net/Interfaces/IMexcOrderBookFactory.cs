using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.SharedApis;
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
        /// Create a SymbolOrderBook for the symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="options">Book options</param>
        /// <returns></returns>
        ISymbolOrderBook Create(SharedSymbol symbol, Action<MexcOrderBookOptions>? options = null);

        /// <summary>
        /// Create a new spot SymbolOrderBook
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        ISymbolOrderBook CreateSpot(string symbol, Action<MexcOrderBookOptions>? options = null);
    }
}