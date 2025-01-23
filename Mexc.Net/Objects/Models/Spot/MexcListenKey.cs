namespace Mexc.Net.Objects.Models.Spot
{
    internal record MexcListenKey
    {
        [JsonPropertyName("listenKey")]
        public string ListenKey { get; set; } = string.Empty;
    }
}
