using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Futures;
using Mexc.Net.Objects.Models;
using Mexc.Net.Interfaces.Clients.FuturesApi;

namespace Mexc.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    internal class MexcRestClientFuturesApiAccount : IMexcRestClientFuturesApiAccount
    {
        private readonly MexcRestClientFuturesApi _baseClient;
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        internal MexcRestClientFuturesApiAccount(MexcRestClientFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }


    }
}
