using Mexc.Net.Enums;

namespace Mexc.Net.Objects.Models.Futures
{
    /// <summary>
    /// Futures public trade update
    /// </summary>
    public record MexcFuturesTradeUpdate
    {
        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Quantity
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Order side
        /// </summary>
        public OrderSide Side { get; set; }

        /// <summary>
        /// Self transaction
        /// </summary>
        public FuturesBool SelfTransaction { get; set; }

        /// <summary>
        /// Timestamp
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Trade ID
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// Position change code. 1 increases, 2 decreases, and 3 leaves the position unchanged
        /// </summary>
        public int? PositionChange { get; set; }

        /// <summary>
        /// Trade matching time
        /// </summary>
        public DateTime? MatchTime { get; set; }
    }
}
