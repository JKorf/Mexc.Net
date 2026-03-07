using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Account info
    /// </summary>
    [SerializationModel]
    public record MexcAccountInfo
    {
        /// <summary>
        /// ["<c>makerCommission</c>"] Maker fee
        /// </summary>
        [JsonPropertyName("makerCommission")]
        public decimal? MakerFee { get; set; }
        /// <summary>
        /// ["<c>takerCommission</c>"] Taker fee
        /// </summary>
        [JsonPropertyName("takerCommission")]
        public decimal? TakerFee { get; set; }
        /// <summary>
        /// ["<c>buyerCommission</c>"] Buyer fee
        /// </summary>
        [JsonPropertyName("buyerCommission")]
        public decimal? BuyerFee { get; set; }
        /// <summary>
        /// ["<c>sellerCommission</c>"] Seller fee
        /// </summary>
        [JsonPropertyName("sellerCommission")]
        public decimal? SellerFee { get; set; }
        /// <summary>
        /// ["<c>canTrade</c>"] Can trade
        /// </summary>
        [JsonPropertyName("canTrade")]
        public bool CanTrade { get; set; }
        /// <summary>
        /// ["<c>canWithdraw</c>"] Can withdraw
        /// </summary>
        [JsonPropertyName("canWithdraw")]
        public bool CanWithdraw { get; set; }
        /// <summary>
        /// ["<c>canDeposit</c>"] Can deposit
        /// </summary>
        [JsonPropertyName("canDeposit")]
        public bool CanDeposit { get; set; }
        /// <summary>
        /// ["<c>updateTime</c>"] Update time
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// ["<c>accountType</c>"] Account type
        /// </summary>
        [JsonPropertyName("accountType")]
        public AccountType AccountType { get; set; }
        /// <summary>
        /// ["<c>balances</c>"] Balances
        /// </summary>
        [JsonPropertyName("balances")]
        public MexcAccountBalance[] Balances { get; set; } = Array.Empty<MexcAccountBalance>();
        /// <summary>
        /// ["<c>permissions</c>"] Permissions
        /// </summary>
        [JsonPropertyName("permissions")]
        public string[] Permissions { get; set; } = Array.Empty<string>();
    }

    /// <summary>
    /// Balance info
    /// </summary>
    [SerializationModel]
    public record MexcAccountBalance
    {
        /// <summary>
        /// ["<c>asset</c>"] The asset this balance is for
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>free</c>"] The quantity that isn't locked in a trade
        /// </summary>
        [JsonPropertyName("free")]
        public decimal Available { get; set; }
        /// <summary>
        /// ["<c>locked</c>"] The quantity that is currently locked in a trade
        /// </summary>
        [JsonPropertyName("locked")]
        public decimal Locked { get; set; }
        /// <summary>
        /// The total balance of this asset (Free + Locked)
        /// </summary>
        public decimal Total => Available + Locked;
    }
}
