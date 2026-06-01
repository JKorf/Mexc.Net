using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Commission info
    /// </summary>
    public record MexcAffiliateCommissions
    {
        /// <summary>
        /// ["<c>pageSize</c>"] Page size
        /// </summary>
        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }
        /// <summary>
        /// ["<c>totalCount</c>"] Total number of records
        /// </summary>
        [JsonPropertyName("totalCount")]
        public int Total { get; set; }
        /// <summary>
        /// ["<c>totalPage</c>"] Total pages
        /// </summary>
        [JsonPropertyName("totalPage")]
        public int TotalPages { get; set; }
        /// <summary>
        /// ["<c>currentPage</c>"] Current page
        /// </summary>
        [JsonPropertyName("currentPage")]
        public int CurrentPage { get; set; }
        /// <summary>
        /// ["<c>resultList</c>"] Data
        /// </summary>
        [JsonPropertyName("resultList")]
        public MexcAffiliateCommission[] Commissions { get; set; } = [];
    }

    /// <summary>
    /// Commission info
    /// </summary>
    public record MexcAffiliateCommission
    {
        /// <summary>
        /// ["<c>spot</c>"] Spot
        /// </summary>
        [JsonPropertyName("spot")]
        public decimal Spot { get; set; }
        /// <summary>
        /// ["<c>futures</c>"] Futures
        /// </summary>
        [JsonPropertyName("futures")]
        public decimal Futures { get; set; }
        /// <summary>
        /// ["<c>etf</c>"] ETF
        /// </summary>
        [JsonPropertyName("etf")]
        public decimal Etf { get; set; }
        /// <summary>
        /// ["<c>total</c>"] Total
        /// </summary>
        [JsonPropertyName("total")]
        public decimal Total { get; set; }
        /// <summary>
        /// ["<c>deposit</c>"] Deposit
        /// </summary>
        [JsonPropertyName("deposit")]
        public decimal? Deposit { get; set; }
        /// <summary>
        /// ["<c>uid</c>"] User id
        /// </summary>
        [JsonPropertyName("uid")]
        public string Uid { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>account</c>"] Account
        /// </summary>
        [JsonPropertyName("account")]
        public string Account { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>inviteTime</c>"] Invite time
        /// </summary>
        [JsonPropertyName("inviteTime")]
        public DateTime InviteTime { get; set; }
        /// <summary>
        /// ["<c>inviteCode</c>"] Invite code
        /// </summary>
        [JsonPropertyName("inviteCode")]
        public string InviteCode { get; set; } = string.Empty;
    }
}
