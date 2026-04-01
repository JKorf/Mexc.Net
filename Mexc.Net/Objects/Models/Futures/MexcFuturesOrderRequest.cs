using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Order request
    /// </summary>
    public record MexcFuturesOrderRequest
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>externalOid</c>"] Client order id
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull), JsonPropertyName("externalOid")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull), JsonPropertyName("price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// ["<c>vol</c>"] Quantity
        /// </summary>
        [JsonPropertyName("vol")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull), JsonPropertyName("leverage")]
        public int? Leverage { get; set; }
        /// <summary>
        /// ["<c>side</c>"] Order side
        /// </summary>
        [JsonPropertyName("side"), JsonConverter(typeof(EnumIntWriterConverter<FuturesOrderSide>))]
        public FuturesOrderSide Side { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Order type
        /// </summary>
        [JsonPropertyName("type"), JsonConverter(typeof(EnumIntWriterConverter<FuturesOrderType>))]
        public FuturesOrderType OrderType { get; set; }
        /// <summary>
        /// ["<c>openType</c>"] Margin type
        /// </summary>
        [JsonPropertyName("openType"), JsonConverter(typeof(EnumIntWriterConverter<MarginType>))]
        public MarginType MarginType { get; set; }
        /// <summary>
        /// ["<c>positionId</c>"] Position id
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull), JsonPropertyName("positionId")]
        public long? PositionId { get; set; }
        /// <summary>
        /// ["<c>positionMode</c>"] Position mode
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull), JsonPropertyName("positionMode"), JsonConverter(typeof(EnumIntWriterConverter<PositionMode>))]
        public PositionMode? PositionMode { get; set; }
        /// <summary>
        /// ["<c>reduceOnly</c>"] Reduce only
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull), JsonPropertyName("reduceOnly")]
        public bool? ReduceOnly { get; set; }
        /// <summary>
        /// ["<c>marketCeiling</c>"] Market ceiling
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull), JsonPropertyName("marketCeiling")]
        public bool? MarketCeiling { get; set; }
        /// <summary>
        /// ["<c>flashClose</c>"] Flash close
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull), JsonPropertyName("flashClose")]
        public bool? FlashClose { get; set; }
        /// <summary>
        /// ["<c>stpMode</c>"] Self trade prevention mode
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull), JsonPropertyName("stpMode"), JsonConverter(typeof(EnumIntWriterConverter<StpMode>))]
        public StpMode? StpMode { get; set; }
    }
}
