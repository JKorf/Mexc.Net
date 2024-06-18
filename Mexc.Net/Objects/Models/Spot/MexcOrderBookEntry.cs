using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Newtonsoft.Json;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// An entry in the order book
    /// </summary>
    [JsonConverter(typeof(ArrayConverter))]
    public record MexcOrderBookEntry : ISymbolOrderBookEntry
    {
        /// <summary>
        /// The price of this order book entry
        /// </summary>
        [ArrayProperty(0)]
        public decimal Price { get; set; }
        /// <summary>
        /// The quantity of this price in the order book
        /// </summary>
        [ArrayProperty(1)]
        public decimal Quantity { get; set; }
    }
}
