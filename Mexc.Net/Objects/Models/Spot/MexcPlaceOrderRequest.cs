using Mexc.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Place order request
    /// </summary>
    public record MexcPlaceOrderRequest
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonPropertyName("type")]
        public OrderType Type { get; set; }
        /// <summary>
        /// Order quantity
        /// </summary>
        [JsonPropertyName("quantity"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? Quantity { get; set; }
        /// <summary>
        /// Quote order quantity
        /// </summary>
        [JsonPropertyName("quoteOrderQty"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? QuoteQuantity { get; set; }
        /// <summary>
        /// Order price
        /// </summary>
        [JsonPropertyName("price"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? Price { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("newClientOrderId"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? ClientOrderId { get; set; }
    }
}
