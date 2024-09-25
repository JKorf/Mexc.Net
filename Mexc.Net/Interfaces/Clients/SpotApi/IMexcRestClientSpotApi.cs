using CryptoExchange.Net.Interfaces.CommonClients;

namespace Mexc.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Mexc Spot API endpoints
    /// </summary>
    public interface IMexcRestClientSpotApi: IRestApiClient
    {
        /// <summary>
        /// DEPRECATED; use <see cref="CryptoExchange.Net.SharedApis.ISharedClient" /> instead for common/shared functionality. See <see href="SHAREDDOCSURL" /> for more info.
        /// </summary>
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
