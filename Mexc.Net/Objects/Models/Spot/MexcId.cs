using CryptoExchange.Net.Converters.SystemTextJson;
namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Id
    /// </summary>
    [SerializationModel]
    public record MexcId
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
    }
}
