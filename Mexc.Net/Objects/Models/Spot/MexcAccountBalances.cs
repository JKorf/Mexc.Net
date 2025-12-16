namespace Mexc.Net.Objects.Models.Spot
{
    internal record MexcAccountBalances
    {
        [JsonPropertyName("balances")]
        public MexcAccountBalance[] Balances { get; set; } = [];
    }
}
