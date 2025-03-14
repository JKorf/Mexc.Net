using CryptoExchange.Net.Converters.SystemTextJson;
namespace Mexc.Net.Objects.Models.Spot
{
    [SerializationModel]
    internal record MexcListenKey
    {
        [JsonPropertyName("listenKey")]
        public string ListenKey { get; set; } = string.Empty;
    }
}
