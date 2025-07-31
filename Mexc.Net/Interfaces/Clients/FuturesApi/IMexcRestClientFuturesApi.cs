
namespace Mexc.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Mexc Futures API endpoints
    /// </summary>
    public interface IMexcRestClientFuturesApi: IRestApiClient
    {
        /// <summary>
        /// Get the shared rest requests client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        IMexcRestClientFuturesApiShared SharedClient { get; }

        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        /// <see cref="IMexcRestClientFuturesApiAccount"/>
        IMexcRestClientFuturesApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        /// <see cref="IMexcRestClientFuturesApiExchangeData"/>
        IMexcRestClientFuturesApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        /// <see cref="IMexcRestClientFuturesApiTrading"/>
        IMexcRestClientFuturesApiTrading Trading { get; }
    }
}
