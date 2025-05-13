using CryptoExchange.Net.Converters.SystemTextJson;
namespace Mexc.Net.Objects.Sockets.Models
{
    /// <summary>
    /// Stream event
    /// </summary>
    [SerializationModel]
    public abstract record MexcStreamEvent
    {
        /// <summary>
        /// Event type
        /// </summary>
        [JsonPropertyName("e")]
        public string Event { get; set; } = string.Empty;
    }
}
