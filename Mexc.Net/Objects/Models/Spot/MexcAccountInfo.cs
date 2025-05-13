using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// Maker fee
        /// </summary>
        [JsonPropertyName("makerCommission")]
        public decimal? MakerFee { get; set; }
        /// <summary>
        /// Taker fee
        /// </summary>
        [JsonPropertyName("takerCommission")]
        public decimal? TakerFee { get; set; }
        /// <summary>
        /// Buyer fee
        /// </summary>
        [JsonPropertyName("buyerCommission")]
        public decimal? BuyerFee { get; set; }
        /// <summary>
        /// Seller fee
        /// </summary>
        [JsonPropertyName("sellerCommission")]
        public decimal? SellerFee { get; set; }
        /// <summary>
        /// Can trade
        /// </summary>
        [JsonPropertyName("canTrade")]
        public bool CanTrade { get; set; }
        /// <summary>
        /// Can withdraw
        /// </summary>
        [JsonPropertyName("canWithdraw")]
        public bool CanWithdraw { get; set; }
        /// <summary>
        /// Can deposit
        /// </summary>
        [JsonPropertyName("canDeposit")]
        public bool CanDeposit { get; set; }
        /// <summary>
        /// Update time
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// Account type
        /// </summary>
        [JsonPropertyName("accountType")]
        public AccountType AccountType { get; set; }
        /// <summary>
        /// Balances
        /// </summary>
        [JsonPropertyName("balances")]
        public MexcAccountBalance[] Balances { get; set; } = Array.Empty<MexcAccountBalance>();
        /// <summary>
        /// Permissions
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
        /// The asset this balance is for
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The quantity that isn't locked in a trade
        /// </summary>
        [JsonPropertyName("free")]
        public decimal Available { get; set; }
        /// <summary>
        /// The quantity that is currently locked in a trade
        /// </summary>
        [JsonPropertyName("locked")]
        public decimal Locked { get; set; }
        /// <summary>
        /// The total balance of this asset (Free + Locked)
        /// </summary>
        public decimal Total => Available + Locked;
    }
}
