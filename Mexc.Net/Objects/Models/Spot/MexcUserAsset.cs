using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// User asset info
    /// </summary>
    public record MexcUserAsset
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonProperty("Name")]
        public string AssetName { get; set; } = string.Empty;
        /// <summary>
        /// Asset network info
        /// </summary>
        [JsonProperty("networkList")]
        public IEnumerable<MexcNetwork> Networks { get; set; } = Array.Empty<MexcNetwork>();
    }

    /// <summary>
    /// Network info
    /// </summary>
    public record MexcNetwork
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Deposit description
        /// </summary>
        [JsonProperty("depositDesc")]
        public string DepositDescription { get; set; } = string.Empty;
        /// <summary>
        /// Is deposit enabled
        /// </summary>
        [JsonProperty("depositEnable")]
        public bool DepositEnabled { get; set; }
        /// <summary>
        /// Minimal number of confirmations needed for deposit
        /// </summary>
        [JsonProperty("minConfirm")]
        public int MinConfirmations { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("Name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Network name
        /// </summary>
        [JsonProperty("netWork")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// Is withdrawing enabled
        /// </summary>
        [JsonProperty("withdrawEnable")]
        public bool WithdrawEnabled { get; set; }
        /// <summary>
        /// Withdrawal fee
        /// </summary>
        [JsonProperty("withdrawFee")]
        public decimal WithdrawFee { get; set; }
        /// <summary>
        /// Withdrawal multiple
        /// </summary>
        [JsonProperty("withdrawIntegerMultiple")]
        public int? WithdrawIntegerMultiple { get; set; }
        /// <summary>
        /// Max withdrawal
        /// </summary>
        [JsonProperty("withdrawMax")]
        public decimal WithdrawMax { get; set; }
        /// <summary>
        /// Minimal withdrawal
        /// </summary>
        [JsonProperty("withdrawMin")]
        public decimal WithdrawMin { get; set; }
        /// <summary>
        /// Same address
        /// </summary>
        [JsonProperty("sameAddress")]
        public bool SameAddress { get; set; }
        /// <summary>
        /// Contract address
        /// </summary>
        [JsonProperty("contract")]
        public string Contract { get; set; } = string.Empty;
        /// <summary>
        /// Withdraw tips
        /// </summary>
        [JsonProperty("withdrawTips")]
        public string? WithdrawTips { get; set; }
        /// <summary>
        /// Deposit tips
        /// </summary>
        [JsonProperty("depositTips")]
        public string? DepositTips { get; set; }
    }
}
