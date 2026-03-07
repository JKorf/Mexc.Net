namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// User asset info
    /// </summary>
    [SerializationModel]
    public record MexcUserAsset
    {
        /// <summary>
        /// ["<c>coin</c>"] Asset
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>name</c>"] Asset name
        /// </summary>
        [JsonPropertyName("name")]
        public string AssetName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>networkList</c>"] Asset network info
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
        /// ["<c>coin</c>"] Asset
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>depositDesc</c>"] Deposit description
        /// </summary>
        [JsonPropertyName("depositDesc")]
        public string DepositDescription { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>depositEnable</c>"] Is deposit enabled
        /// </summary>
        [JsonPropertyName("depositEnable")]
        public bool DepositEnabled { get; set; }
        /// <summary>
        /// ["<c>minConfirm</c>"] Minimal number of confirmations needed for deposit
        /// </summary>
        [JsonPropertyName("minConfirm")]
        public int MinConfirmations { get; set; }
        /// <summary>
        /// ["<c>Name</c>"] Name
        /// </summary>
        [JsonPropertyName("Name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>netWork</c>"] Network name
        /// </summary>
        [JsonPropertyName("netWork")]
        public string Network { get; set; } = string.Empty;
        [JsonInclude, JsonPropertyName("network")]
        internal string NetworkInt
        {
            set => Network = value;
        }
        /// <summary>
        /// ["<c>withdrawEnable</c>"] Is withdrawing enabled
        /// </summary>
        [JsonPropertyName("withdrawEnable")]
        public bool WithdrawEnabled { get; set; }
        /// <summary>
        /// ["<c>withdrawFee</c>"] Withdrawal fee
        /// </summary>
        [JsonPropertyName("withdrawFee")]
        public decimal WithdrawFee { get; set; }
        /// <summary>
        /// ["<c>withdrawIntegerMultiple</c>"] Withdrawal multiple
        /// </summary>
        [JsonPropertyName("withdrawIntegerMultiple")]
        public int? WithdrawIntegerMultiple { get; set; }
        /// <summary>
        /// ["<c>withdrawMax</c>"] Max withdrawal
        /// </summary>
        [JsonPropertyName("withdrawMax"), JsonConverter(typeof(BigDecimalConverter))]
        public decimal WithdrawMax { get; set; }
        /// <summary>
        /// ["<c>withdrawMin</c>"] Minimal withdrawal
        /// </summary>
        [JsonPropertyName("withdrawMin")]
        public decimal WithdrawMin { get; set; }
        /// <summary>
        /// ["<c>sameAddress</c>"] Same address
        /// </summary>
        [JsonPropertyName("sameAddress")]
        public bool SameAddress { get; set; }
        /// <summary>
        /// ["<c>contract</c>"] Contract address
        /// </summary>
        [JsonPropertyName("contract")]
        public string Contract { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>withdrawTips</c>"] Withdraw tips
        /// </summary>
        [JsonPropertyName("withdrawTips")]
        public string? WithdrawTips { get; set; }
        /// <summary>
        /// ["<c>depositTips</c>"] Deposit tips
        /// </summary>
        [JsonPropertyName("depositTips")]
        public string? DepositTips { get; set; }
    }
}
