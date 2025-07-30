using CryptoExchange.Net.Objects.Options;

namespace Mexc.Net.Objects.Options
{
    /// <summary>
    /// Options for the Kucoin SymbolOrderBook
    /// </summary>
    public class MexcOrderBookOptions : OrderBookOptions
    {
        /// <summary>
        /// Default options for new order books
        /// </summary>
        public static MexcOrderBookOptions Default { get; set; } = new MexcOrderBookOptions();

        /// <summary>
        /// The top amount of results to keep in sync. If for example limit=10 is used, the order book will contain the 10 best bids and 10 best asks. Leaving this null will sync the full order book
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// Update interval, either 10 or 100ms
        /// </summary>
        public int? UpdateInterval { get; set; }

        /// <summary>
        /// After how much time we should consider the connection dropped if no data is received for this time after the initial subscriptions
        /// </summary>
        public TimeSpan? InitialDataTimeout { get; set; }

        internal MexcOrderBookOptions Copy()
        {
            var result = Copy<MexcOrderBookOptions>();
            result.Limit = Limit;
            result.InitialDataTimeout = InitialDataTimeout;
            return result;
        }
    }
}
