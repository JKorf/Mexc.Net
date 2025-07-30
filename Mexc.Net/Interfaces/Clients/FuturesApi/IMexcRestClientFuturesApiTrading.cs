using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Futures;

namespace Mexc.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Mexc Futures trading endpoints, placing and managing orders.
    /// </summary>
    public interface IMexcRestClientFuturesApiTrading
    {
        /// <summary>
        /// Get open orders
        /// <para><a href="https://mexcdevelop.github.io/apidocs/contract_v1_en/#get-the-user-39-s-current-pending-order" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<MexcFuturesOrder[]>> GetOpenOrdersAsync(string symbol, int? page = null, int? pageSize = null, CancellationToken ct = default);

    }
}
