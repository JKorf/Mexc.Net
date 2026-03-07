namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Trading fee info
    /// </summary>
    public record MexcFuturesFee
    {
        /// <summary>
        /// ["<c>level</c>"] Level
        /// </summary>
        [JsonPropertyName("level")]
        public int Level { get; set; }
        /// <summary>
        /// ["<c>dealAmount</c>"] Trade quantity over last 30 days
        /// </summary>
        [JsonPropertyName("dealAmount")]
        public decimal TradeQuantity30Days { get; set; }
        /// <summary>
        /// ["<c>walletBalance</c>"] Wallet balance of yesterday
        /// </summary>
        [JsonPropertyName("walletBalance")]
        public decimal WalletBalance { get; set; }
        /// <summary>
        /// ["<c>makerFee</c>"] Maker fee
        /// </summary>
        [JsonPropertyName("makerFee")]
        public decimal MakerFee { get; set; }
        /// <summary>
        /// ["<c>takerFee</c>"] Taker fee
        /// </summary>
        [JsonPropertyName("takerFee")]
        public decimal TakerFee { get; set; }
        /// <summary>
        /// ["<c>makerFeeDiscount</c>"] Maker fee discount
        /// </summary>
        [JsonPropertyName("makerFeeDiscount")]
        public decimal MakerFeeDiscount { get; set; }
        /// <summary>
        /// ["<c>takerFeeDiscount</c>"] Taker fee discount
        /// </summary>
        [JsonPropertyName("takerFeeDiscount")]
        public decimal TakerFeeDiscount { get; set; }
    }


}
