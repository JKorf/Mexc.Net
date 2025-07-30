using CryptoExchange.Net.Converters;
using Mexc.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Funding history page
    /// </summary>
    public record MexcFundingRateHistoryPage
    {
        /// <summary>
        /// Page size
        /// </summary>
        [JsonPropertyName("pageSize")]
        public decimal PageSize { get; set; }
        /// <summary>
        /// Total count
        /// </summary>
        [JsonPropertyName("totalCount")]
        public decimal TotalCount { get; set; }
        /// <summary>
        /// Total pages
        /// </summary>
        [JsonPropertyName("totalPage")]
        public decimal TotalPages { get; set; }
        /// <summary>
        /// Current page
        /// </summary>
        [JsonPropertyName("currentPage")]
        public decimal CurrentPage { get; set; }
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("resultList")]
        public MexcFundingRateHistory[] Data { get; set; } = [];
    }

    /// <summary>
    /// Funding rate
    /// </summary>
    public record MexcFundingRateHistory
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Funding rate
        /// </summary>
        [JsonPropertyName("fundingRate")]
        public decimal FundingRate { get; set; }
        /// <summary>
        /// Settle time
        /// </summary>
        [JsonPropertyName("settleTime")]
        public DateTime SettleTime { get; set; }
    }


}
