using CryptoExchange.Net.Converters.SystemTextJson;
using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// User trade update
    /// </summary>
    [SerializationModel]
    public record MexcUserTradeUpdate
    {
        /// <summary>
        /// Trade time
        /// </summary>
        [JsonPropertyName("T")]
        public DateTime TradeTime { get; set; }
        /// <summary>
        /// Trade side
        /// </summary>
        [JsonPropertyName("S")]
        public OrderSide TradeSide { get; set; }
        /// <summary>
        /// Quote quantity
        /// </summary>
        [JsonPropertyName("a")]
        public decimal QuoteQuantity { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("c")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("i")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Trade id
        /// </summary>
        [JsonPropertyName("t")]
        public string TradeId { get; set; } = string.Empty;
        /// <summary>
        /// Is maker
        /// </summary>
        [JsonPropertyName("m")]
        public bool IsMaker { get; set; }
        /// <summary>
        /// Is self trade
        /// </summary>
        [JsonPropertyName("st")]
        public bool IsSelfTrade { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("v")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        [JsonPropertyName("n")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonPropertyName("N")]
        public string FeeAsset { get; set; } = string.Empty;
    }
}
