namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Detailed futures ticker update
    /// </summary>
    public record MexcFuturesDetailTickerUpdate
    {
        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Last price
        /// </summary>
        public decimal LastPrice { get; set; }

        /// <summary>
        /// Best bid price
        /// </summary>
        public decimal BestBidPrice { get; set; }

        /// <summary>
        /// Best ask price
        /// </summary>
        public decimal BestAskPrice { get; set; }

        /// <summary>
        /// Volume in last 24h
        /// </summary>
        public decimal Volume24h { get; set; }

        /// <summary>
        /// Volume in quote asset last 24h
        /// </summary>
        public decimal QuoteVolume24h { get; set; }

        /// <summary>
        /// Open interest
        /// </summary>
        public decimal OpenInterest { get; set; }

        /// <summary>
        /// Lowest price last 24h
        /// </summary>
        public decimal LowPrice { get; set; }

        /// <summary>
        /// Highest price last 24h
        /// </summary>
        public decimal HighPrice { get; set; }

        /// <summary>
        /// Change percentage
        /// </summary>
        public decimal ChangePercentage { get; set; }

        /// <summary>
        /// Change value
        /// </summary>
        public decimal ChangeValue { get; set; }

        /// <summary>
        /// Index price
        /// </summary>
        public decimal IndexPrice { get; set; }

        /// <summary>
        /// Mark price
        /// </summary>
        public decimal MarkPrice { get; set; }

        /// <summary>
        /// Funding rate
        /// </summary>
        public decimal FundingRate { get; set; }

        /// <summary>
        /// Max bid price
        /// </summary>
        public decimal MaxBidPrice { get; set; }

        /// <summary>
        /// Min ask price
        /// </summary>
        public decimal MinAskPrice { get; set; }

        /// <summary>
        /// Timestamp
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Ticker time zone
        /// </summary>
        public string TimeZone { get; set; } = string.Empty;

        /// <summary>
        /// Price change rates as fractions
        /// </summary>
        public decimal?[] ChangeRates { get; set; } = [];

        /// <summary>
        /// Time zones for the price change rates
        /// </summary>
        public string[] ChangeRatesTimeZones { get; set; } = [];
    }
}
