
namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Trade fee
    /// </summary>
    public record MexcTradeFee
    {
        /// <summary>
        /// Maker fee
        /// </summary>
        [JsonProperty("makerCommission")]
        public decimal MakerFee { get; set; }
        /// <summary>
        /// Taker fee
        /// </summary>
        [JsonProperty("takerCommission")]
        public decimal TakerFee { get; set; }
    }
}
