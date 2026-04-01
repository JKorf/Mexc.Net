namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Ticker
    /// </summary>
    public record MexcFuturesTicker
    {
        /// <summary>
        /// ["<c>contractId</c>"] Contract id
        /// </summary>
        [JsonPropertyName("contractId")]
        public long ContractId { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>lastPrice</c>"] Last price
        /// </summary>
        [JsonPropertyName("lastPrice")]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// ["<c>bid1</c>"] Best bid price
        /// </summary>
        [JsonPropertyName("bid1")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// ["<c>ask1</c>"] Best ask price
        /// </summary>
        [JsonPropertyName("ask1")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// ["<c>volume24</c>"] Volume in last 24h
        /// </summary>
        [JsonPropertyName("volume24")]
        public decimal Volume24h { get; set; }
        /// <summary>
        /// ["<c>amount24</c>"] Volume in quote asset last 24h
        /// </summary>
        [JsonPropertyName("amount24")]
        public decimal QuoteVolume24h { get; set; }
        /// <summary>
        /// ["<c>holdVol</c>"] Open interest
        /// </summary>
        [JsonPropertyName("holdVol")]
        public decimal OpenInterest { get; set; }
        /// <summary>
        /// ["<c>lower24Price</c>"] Lowest price last 24h
        /// </summary>
        [JsonPropertyName("lower24Price")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// ["<c>high24Price</c>"] Highest price last 24h
        /// </summary>
        [JsonPropertyName("high24Price")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// ["<c>riseFallRate</c>"] Change percentage
        /// </summary>
        [JsonPropertyName("riseFallRate")]
        public decimal ChangePercentage { get; set; }
        /// <summary>
        /// ["<c>riseFallValue</c>"] Change value
        /// </summary>
        [JsonPropertyName("riseFallValue")]
        public decimal ChangeValue { get; set; }
        /// <summary>
        /// ["<c>indexPrice</c>"] Index price
        /// </summary>
        [JsonPropertyName("indexPrice")]
        public decimal IndexPrice { get; set; }
        /// <summary>
        /// ["<c>fairPrice</c>"] Mark price
        /// </summary>
        [JsonPropertyName("fairPrice")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// ["<c>fundingRate</c>"] Funding rate
        /// </summary>
        [JsonPropertyName("fundingRate")]
        public decimal FundingRate { get; set; }
        /// <summary>
        /// ["<c>maxBidPrice</c>"] Max bid price
        /// </summary>
        [JsonPropertyName("maxBidPrice")]
        public decimal MaxBidPrice { get; set; }
        /// <summary>
        /// ["<c>minAskPrice</c>"] Min ask price
        /// </summary>
        [JsonPropertyName("minAskPrice")]
        public decimal MinAskPrice { get; set; }
        /// <summary>
        /// ["<c>timestamp</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }


}
