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
        /// Url to the main website
        /// </summary>
        public static string Url { get; } = "https://www.mexc.com";

        /// <summary>
        /// Urls to the API documentation
        /// </summary>
        public static string[] ApiDocsUrl { get; } = new[] {
            "https://mexcdevelop.github.io/apidocs/spot_v3_en/#introduction"
            };
    }
}
