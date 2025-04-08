
namespace Mexc.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Mexc Spot API endpoints
    /// </summary>
    public interface IMexcRestClientSpotApi: IRestApiClient
    {
        /// <summary>
        /// Get the shared rest requests client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        IMexcRestClientSpotApiShared SharedClient { get; }

        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        /// <see cref="IMexcRestClientSpotApiAccount"/>
        IMexcRestClientSpotApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        /// <see cref="IMexcRestClientSpotApiExchangeData"/>
        IMexcRestClientSpotApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        /// <see cref="IMexcRestClientSpotApiTrading"/>
        IMexcRestClientSpotApiTrading Trading { get; }
    }
}
