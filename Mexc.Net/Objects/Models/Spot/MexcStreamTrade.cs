using CryptoExchange.Net.Converters.SystemTextJson;
using Mexc.Net.Enums;
using ProtoBuf;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Trade info
    /// </summary>
    [SerializationModel]
    [ProtoContract]
    public record MexcStreamTrade
    {
        /// <summary>
        /// Order side
        /// </summary>
        [JsonPropertyName("S")]
        [ProtoMember(3)]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("p")]
        [ProtoMember(1)]
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("v")]
        [JsonConverter(typeof(BigDecimalConverter))]
        [ProtoMember(2)]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Trade time
        /// </summary>
        [JsonPropertyName("t")]
        [ProtoMember(4)]
        public DateTime Timestamp { get; set; }
    }
}
