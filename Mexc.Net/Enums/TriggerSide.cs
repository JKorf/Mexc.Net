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
        /// ["<c>0</c>"] Untriggered
        /// </summary>
        [Map("0")]
        Untriggered,
        /// <summary>
        /// ["<c>1</c>"] Take profit
        /// </summary>
        [Map("1")]
        TakeProfit,
        /// <summary>
        /// ["<c>2</c>"] Stop loss
        /// </summary>
        [Map("2")]
        StopLoss
    }
}
