using CryptoExchange.Net.Converters.SystemTextJson;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// User asset info
    /// </summary>
    [SerializationModel]
    public record MexcUserAsset
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonPropertyName("name")]
        public string AssetName { get; set; } = string.Empty;
        /// <summary>
        /// Asset network info
        /// </summary>
        [JsonPropertyName("networkList")]
        public MexcNetwork[] Networks { get; set; } = Array.Empty<MexcNetwork>();
    }

    /// <summary>
    /// Network info
    /// </summary>
    [SerializationModel]
    public record MexcNetwork
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Deposit description
        /// </summary>
        [JsonPropertyName("depositDesc")]
        public string DepositDescription { get; set; } = string.Empty;
        /// <summary>
        /// Is deposit enabled
        /// </summary>
        [JsonPropertyName("depositEnable")]
        public bool DepositEnabled { get; set; }
        /// <summary>
        /// Minimal number of confirmations needed for deposit
        /// </summary>
        [JsonPropertyName("minConfirm")]
        public int MinConfirmations { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        [JsonPropertyName("Name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Network name
        /// </summary>
        [JsonPropertyName("netWork")]
        public string Network { get; set; } = string.Empty;
        [JsonInclude, JsonPropertyName("network")]
        internal string NetworkInt
        {
            set => Network = value;
        }
        /// <summary>
        /// Is withdrawing enabled
        /// </summary>
        [JsonPropertyName("withdrawEnable")]
        public bool WithdrawEnabled { get; set; }
        /// <summary>
        /// Withdrawal fee
        /// </summary>
        [JsonPropertyName("withdrawFee")]
        public decimal WithdrawFee { get; set; }
        /// <summary>
        /// Withdrawal multiple
        /// </summary>
        [JsonPropertyName("withdrawIntegerMultiple")]
        public int? WithdrawIntegerMultiple { get; set; }
        /// <summary>
        /// Max withdrawal
        /// </summary>
        [JsonPropertyName("withdrawMax"), JsonConverter(typeof(BigDecimalConverter))]
        public decimal WithdrawMax { get; set; }
        /// <summary>
        /// Minimal withdrawal
        /// </summary>
        [JsonPropertyName("withdrawMin")]
        public decimal WithdrawMin { get; set; }
        /// <summary>
        /// Same address
        /// </summary>
        [JsonPropertyName("sameAddress")]
        public bool SameAddress { get; set; }
        /// <summary>
        /// Contract address
        /// </summary>
        [JsonPropertyName("contract")]
        public string Contract { get; set; } = string.Empty;
        /// <summary>
        /// Withdraw tips
        /// </summary>
        [JsonPropertyName("withdrawTips")]
        public string? WithdrawTips { get; set; }
        /// <summary>
        /// Deposit tips
        /// </summary>
        [JsonPropertyName("depositTips")]
        public string? DepositTips { get; set; }
    }
}
