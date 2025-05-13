using CryptoExchange.Net.Converters.SystemTextJson;
namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Order book info
    /// </summary>
    [SerializationModel]
    public record MexcOrderBook
    {
        /// <summary>
        /// Last update id
        /// </summary>
        [JsonPropertyName("lastUpdateId")]
        public long LastUpdateId { get; set; }

        /// <summary>
        /// The list of bids
        /// </summary>
        [JsonPropertyName("bids")]
        public MexcOrderBookEntry[] Bids { get; set; } = Array.Empty<MexcOrderBookEntry>();

        /// <summary>
        /// The list of asks
        /// </summary>
        [JsonPropertyName("asks")]
        public MexcOrderBookEntry[] Asks { get; set; } = Array.Empty<MexcOrderBookEntry>();
    }
}
