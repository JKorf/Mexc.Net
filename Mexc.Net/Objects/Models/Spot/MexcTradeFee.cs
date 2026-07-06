namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Trade fee
    /// </summary>
    [SerializationModel]
    public record MexcTradeFee
    {
        /// <summary>
        /// ["<c>makerCommission</c>"] Maker fee
        /// </summary>
        [JsonPropertyName("makerCommission")]
        public decimal MakerFee { get; set; }
        /// <summary>
        /// ["<c>takerCommission</c>"] Taker fee
        /// </summary>
        [JsonPropertyName("takerCommission")]
        public decimal TakerFee { get; set; }
        /// <summary>
        /// ["<c>rpiMakerCommission</c>"] RPI maker commission
        /// </summary>
        [JsonPropertyName("rpiMakerCommission")]
        public decimal? RpiMakerCommission { get; set; }
    }
}
