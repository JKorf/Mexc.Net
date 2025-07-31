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
    /// Trading fee info
    /// </summary>
    public record MexcFuturesFee
    {
        /// <summary>
        /// Level
        /// </summary>
        [JsonPropertyName("level")]
        public int Level { get; set; }
        /// <summary>
        /// Trade quantity over last 30 days
        /// </summary>
        [JsonPropertyName("dealAmount")]
        public decimal TradeQuantity30Days { get; set; }
        /// <summary>
        /// Wallet balance of yesterday
        /// </summary>
        [JsonPropertyName("walletBalance")]
        public decimal WalletBalance { get; set; }
        /// <summary>
        /// Maker fee
        /// </summary>
        [JsonPropertyName("makerFee")]
        public decimal MakerFee { get; set; }
        /// <summary>
        /// Taker fee
        /// </summary>
        [JsonPropertyName("takerFee")]
        public decimal TakerFee { get; set; }
        /// <summary>
        /// Maker fee discount
        /// </summary>
        [JsonPropertyName("makerFeeDiscount")]
        public decimal MakerFeeDiscount { get; set; }
        /// <summary>
        /// Taker fee discount
        /// </summary>
        [JsonPropertyName("takerFeeDiscount")]
        public decimal TakerFeeDiscount { get; set; }
    }


}
