using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Mexc.Net.Enums
{
    /// <summary>
    /// Trigger side
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TriggerSide>))]
    public enum TriggerSide
    {
        /// <summary>
        /// Untriggered
        /// </summary>
        [Map("0")]
        Untriggered,
        /// <summary>
        /// Take profit
        /// </summary>
        [Map("1")]
        TakeProfit,
        /// <summary>
        /// Stop loss
        /// </summary>
        [Map("2")]
        StopLoss
    }
}
