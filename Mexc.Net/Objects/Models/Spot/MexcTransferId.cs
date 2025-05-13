using CryptoExchange.Net.Converters.SystemTextJson;
namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Transfer id
    /// </summary>
    [SerializationModel]
    public record MexcTransferId
    {
        /// <summary>
        /// Transfer id
        /// </summary>
        [JsonPropertyName("tranId")]
        public string TransferId { get; set; } = string.Empty;
    }
}
