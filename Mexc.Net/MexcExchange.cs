using CryptoExchange.Net.RateLimiting;
using CryptoExchange.Net.RateLimiting.Filters;
using CryptoExchange.Net.RateLimiting.Guards;
using CryptoExchange.Net.RateLimiting.Interfaces;
using CryptoExchange.Net.SharedApis;

namespace Mexc.Net
{
    /// <summary>
    /// Mexc exchange information and configuration
    /// </summary>
    public static class MexcExchange
    {
        /// <summary>
        /// Exchange name
        /// </summary>
        public static string ExchangeName => "Mexc";

        /// <summary>
        /// Exchange name
        /// </summary>
        public static string DisplayName => "Mexc";

        /// <summary>
        /// Url to exchange image
        /// </summary>
        public static string ImageUrl { get; } = "https://raw.githubusercontent.com/JKorf/Mexc.Net/master/Mexc.Net/Icon/icon.png";

        /// <summary>
        /// Url to the main website
        /// </summary>
        public static string Url { get; } = "https://www.mexc.com";

        /// <summary>
        /// Urls to the API documentation
        /// </summary>
        public static string[] ApiDocsUrl { get; } = new[] {
            "https://mexcdevelop.github.io/apidocs/spot_v3_en/#introduction"
            };

        /// <summary>
        /// Type of exchange
        /// </summary>
        public static ExchangeType Type { get; } = ExchangeType.CEX;

        /// <summary>
        /// Format a base and quote asset to a Mexc recognized symbol 
        /// </summary>
        /// <param name="baseAsset">Base asset</param>
        /// <param name="quoteAsset">Quote asset</param>
        /// <param name="tradingMode">Trading mode</param>
        /// <param name="deliverTime">Delivery time for delivery futures</param>
        /// <returns></returns>
        public static string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null) 
            => baseAsset.ToUpperInvariant() + quoteAsset.ToUpperInvariant();


        /// <summary>
        /// Rate limiter configuration for the Mexc API
        /// </summary>
        public static MexcRateLimiters RateLimiter { get; } = new MexcRateLimiters();
    }

    /// <summary>
    /// Rate limiter configuration for the Mexc API
    /// </summary>
    public class MexcRateLimiters
    {
        /// <summary>
        /// Event for when a rate limit is triggered
        /// </summary>
        public event Action<RateLimitEvent> RateLimitTriggered;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        internal MexcRateLimiters()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Initialize();
        }

        private void Initialize()
        {
            SpotSocket = new RateLimitGate("Spot Socket")
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerHost, new IGuardFilter[] { new LimitItemTypeFilter(RateLimitItemType.Connection) }, 100, TimeSpan.FromSeconds(1), RateLimitWindowType.Fixed)); // 100 connections per second per host

            SpotRest = new RateLimitGate("Spot Rest")
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerEndpoint, [], 500, TimeSpan.FromSeconds(10), RateLimitWindowType.Sliding)); // 500 request per 10 seconds per IP for each endpoint

            SpotSocket.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            SpotRest.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
        }

        internal IRateLimitGate SpotSocket { get; private set; }
        internal IRateLimitGate SpotRest { get; private set; }
    }
}
