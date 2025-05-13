using CryptoExchange.Net.Converters.SystemTextJson;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Trade fee
    /// </summary>
    [SerializationModel]
    public record MexcTradeFee
    {
        /// <summary>
        /// Maker fee
        /// </summary>
        [JsonPropertyName("makerCommission")]
        public decimal MakerFee { get; set; }
        /// <summary>
        /// Taker fee
        /// </summary>
        [JsonPropertyName("takerCommission")]
        public decimal TakerFee { get; set; }
    }
}
