using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Converters;
using Mexc.Net.Converters;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// An entry in the order book
    /// </summary>
    [JsonConverter(typeof(ArrayConverter<MexcOrderBookEntry>))]
    [SerializationModel]
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
        [ArrayProperty(1), JsonConverter(typeof(BigDecimalConverter))]
        public decimal Quantity { get; set; }
    }
}
