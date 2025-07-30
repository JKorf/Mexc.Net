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
    /// Risk fund balance history page
    /// </summary>
    public record MexcRiskFundBalanceHistoryPage
    {
        /// <summary>
        /// Page size
        /// </summary>
        [JsonPropertyName("pageSize")]
        public decimal PageSize { get; set; }
        /// <summary>
        /// Total number of results
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
        public MexcRiskFundBalanceHistory[] Data { get; set; } = [];
    }

    /// <summary>
    /// Risk fund balance item
    /// </summary>
    public record MexcRiskFundBalanceHistory
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Available
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// Snapshot time
        /// </summary>
        [JsonPropertyName("snapshotTime")]
        public DateTime SnapshotTime { get; set; }
    }


}
