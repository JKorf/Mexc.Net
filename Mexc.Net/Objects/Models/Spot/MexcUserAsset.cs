using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mexc.Net.Objects.Models.Spot
{
    public class MexcUserAsset
    {
        [JsonProperty("coin")]
        public string Asset { get; set; }
        [JsonProperty("Name")]
        public string AssetName { get; set; }

        [JsonProperty("networkList")]
        public IEnumerable<MexcNetwork> Networks { get; set; }
    }

    public class MexcNetwork
    {
        [JsonProperty("coin")]
        public string Asset { get; set; }
        [JsonProperty("depositDesc")]
        public string DepositDescription { get; set; }
        [JsonProperty("depositEnable")]
        public bool DepositEnabled { get; set; }
        [JsonProperty("minConfirm")]
        public int MinConfirmations { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("network")]
        public string Network { get; set; }
        [JsonProperty("withdrawEnable")]
        public bool WithdrawEnabled { get; set; }
        [JsonProperty("withdrawFee")]
        public decimal WithdrawFee { get; set; }
        [JsonProperty("withdrawIntegerMultiple")]
        public int? WithdrawIntegerMultiple { get; set; }
        [JsonProperty("withdrawMax")]
        public decimal WithdrawMax { get; set; }
        [JsonProperty("withdrawMin")]
        public decimal WithdrawMin { get; set; }
        [JsonProperty("sameAddress")]
        public bool SameAddress { get; set; }
        [JsonProperty("contract")]
        public string Contract { get; set; }
        [JsonProperty("withdrawTips")]
        public string? WithdrawTips { get; set; }
        [JsonProperty("depositTips")]
        public string? DepositTips { get; set; }
    }
}
