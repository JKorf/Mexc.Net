using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using Mexc.Net.Enums;
using Mexc.Net.Interfaces.Clients.SpotApi;
using Mexc.Net.Objects.Models.Spot;
using System.Linq;

namespace Mexc.Net.Clients.SpotApi
{
    internal partial class MexcRestClientSpotSharedApi
    {
        #region Get Order Book

        async Task<ICallResult<SharedOrderBook>> IGetOrderBook.GetOrderBookAsync(GetOrderBookRequest request, CancellationToken ct)
            => await GetOrderBookAsync(request, ct).ConfigureAwait(false);

        public GetOrderBookOptions GetOrderBookOptions { get; } = new GetOrderBookOptions(_exchangeName, 1, 5000, false);
        public async Task<HttpResult<SharedOrderBook>> GetOrderBookAsync(GetOrderBookRequest request, CancellationToken ct)
        {
            var validationError = GetOrderBookOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedOrderBook>(Exchange, validationError);

            var result = await _api.ExchangeData.GetOrderBookAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                limit: request.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedOrderBook>(result);

            return HttpResult.Ok(result, new SharedOrderBook(SharedQuantityType.BaseAsset, result.Data.LastUpdateId, result.Data.Asks, result.Data.Bids));
        }

        #endregion

    }
}
