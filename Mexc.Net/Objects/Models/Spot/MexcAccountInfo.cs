using Mexc.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Account info
    /// </summary>
    public class MexcAccountInfo
    {
        /// <summary>
        /// Maker fee
        /// </summary>
        [JsonProperty("makerCommission")]
        public decimal? MakerFee { get; set; }
        /// <summary>
        /// Taker fee
        /// </summary>
        [JsonProperty("takerCommission")]
        public decimal? TakerFee { get; set; }
        /// <summary>
        /// Buyer fee
        /// </summary>
        [JsonProperty("buyerCommission")]
        public decimal? BuyerFee { get; set; }
        /// <summary>
        /// Seller fee
        /// </summary>
        [JsonProperty("sellerCommission")]
        public decimal? SellerFee { get; set; }
        /// <summary>
        /// Can trade
        /// </summary>
        [JsonProperty("canTrade")]
        public bool CanTrade { get; set; }
        /// <summary>
        /// Can withdraw
        /// </summary>
        [JsonProperty("canWithdraw")]
        public bool CanWithdraw { get; set; }
        /// <summary>
        /// Can deposit
        /// </summary>
        [JsonProperty("canDeposit")]
        public bool CanDeposit { get; set; }
        /// <summary>
        /// Update time
        /// </summary>
        [JsonProperty("updateTime")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// Account type
        /// </summary>
        [JsonProperty("accountType")]
        public AccountType AccountType { get; set; }
        /// <summary>
        /// Balances
        /// </summary>
        [JsonProperty("balances")]
        public IEnumerable<MexcAccountBalance> Balances { get; set; } = Array.Empty<MexcAccountBalance>();
        /// <summary>
        /// Permissions
        /// </summary>
        [JsonProperty("permissions")]
        public IEnumerable<string> Permissions { get; set; } = Array.Empty<string>();
    }

    /// <summary>
    /// Balance info
    /// </summary>
    public class MexcAccountBalance
    {
        /// <summary>
        /// The asset this balance is for
        /// </summary>
        [JsonProperty("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The quantity that isn't locked in a trade
        /// </summary>
        [JsonProperty("free")]
        public decimal Available { get; set; }
        /// <summary>
        /// The quantity that is currently locked in a trade
        /// </summary>
        [JsonProperty("locked")]
        public decimal Locked { get; set; }
        /// <summary>
        /// The total balance of this asset (Free + Locked)
        /// </summary>
        public decimal Total => Available + Locked;
    }
}
