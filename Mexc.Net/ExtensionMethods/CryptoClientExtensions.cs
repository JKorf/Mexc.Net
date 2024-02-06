using Mexc.Net.Clients;
using Mexc.Net.Interfaces.Clients;

namespace CryptoExchange.Net.Interfaces
{
    /// <summary>
    /// Extensions for the ICryptoRestClient and ICryptoSocketClient interfaces
    /// </summary>
    public static class CryptoClientExtensions
    {
        /// <summary>
        /// Get the Mexc REST Api client
        /// </summary>
        /// <param name="baseClient"></param>
        /// <returns></returns>
        public static IMexcRestClient Mexc(this ICryptoRestClient baseClient) => baseClient.TryGet<IMexcRestClient>(() => new MexcRestClient());

        /// <summary>
        /// Get the Mexc Websocket Api client
        /// </summary>
        /// <param name="baseClient"></param>
        /// <returns></returns>
        public static IMexcSocketClient Mexc(this ICryptoSocketClient baseClient) => baseClient.TryGet<IMexcSocketClient>(() => new MexcSocketClient());
    }
}