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
    /// Funding record page
    /// </summary>
    public record MexcFundingRecordPage
    {
        /// <summary>
        /// Page quantity
        /// </summary>
        [JsonPropertyName("pageSize")]
        public int PageQuantity { get; set; }
        /// <summary>
        /// Total count
        /// </summary>
        [JsonPropertyName("totalCount")]
        public int TotalCount { get; set; }
        /// <summary>
        /// Total page
        /// </summary>
        [JsonPropertyName("totalPage")]
        public int TotalPage { get; set; }
        /// <summary>
        /// Current page
        /// </summary>
        [JsonPropertyName("currentPage")]
        public int CurrentPage { get; set; }
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("resultList")]
        public MexcFundingRecord[] Data { get; set; } = [];
    }

    /// <summary>
    /// Funding record
    /// </summary>
    public record MexcFundingRecord
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Position side
        /// </summary>
        [JsonPropertyName("positionType")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// Position value
        /// </summary>
        [JsonPropertyName("positionValue")]
        public decimal PositionValue { get; set; }
        /// <summary>
        /// Funding
        /// </summary>
        [JsonPropertyName("funding")]
        public decimal Funding { get; set; }
        /// <summary>
        /// Rate
        /// </summary>
        [JsonPropertyName("rate")]
        public decimal Rate { get; set; }
        /// <summary>
        /// Settle time
        /// </summary>
        [JsonPropertyName("settleTime")]
        public DateTime SettleTime { get; set; }
    }


}
