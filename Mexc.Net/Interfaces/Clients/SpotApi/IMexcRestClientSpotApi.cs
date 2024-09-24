using CryptoExchange.Net.Interfaces.CommonClients;

namespace Mexc.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Mexc Spot API endpoints
    /// </summary>
    public interface IMexcRestClientSpotApi: IRestApiClient
    {
        /// <summary>
        /// Get the ISpotClient for this client. This is a common interface which allows for some basic operations without knowing any details of the exchange.
        /// </summary>
        /// <returns></returns>
        ISpotClient CommonSpotClient { get; }

        /// <summary>
        /// Get the shared rest requests client
        /// </summary>
        IMexcRestClientSpotApiShared SharedClient { get; }

        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        IMexcRestClientSpotApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        IMexcRestClientSpotApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        IMexcRestClientSpotApiTrading Trading { get; }
    }
}
