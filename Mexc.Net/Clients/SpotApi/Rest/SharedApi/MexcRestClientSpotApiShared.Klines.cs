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
        #region Kline client

        public GetKlinesOptions GetKlinesOptions { get; } = new GetKlinesOptions(_exchangeName, true, false, true, 500, false,
            SharedKlineInterval.OneMinute,
            SharedKlineInterval.FiveMinutes,
            SharedKlineInterval.FifteenMinutes,
            SharedKlineInterval.ThirtyMinutes,
            SharedKlineInterval.OneHour,
            SharedKlineInterval.FourHours,
            SharedKlineInterval.OneDay,
            SharedKlineInterval.OneWeek,
            SharedKlineInterval.OneMonth);
        public async Task<HttpResult<SharedKline[]>> GetKlinesAsync(GetKlinesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;

            var validationError = GetKlinesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedKline[]>(Exchange, validationError);

            int limit = request.Limit ?? 500;
            var direction = DataDirection.Ascending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, true);

            // Get data
            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.ExchangeData.GetKlinesAsync(
                symbol,
                interval,
                pageParams.StartTime,
                pageParams.EndTime,
                pageParams.Limit,
                ct: ct
                ).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedKline[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromTime(pageParams, result.Data.Max(x => x.OpenTime)),
                     result.Data.Length,
                     result.Data.Select(x => x.OpenTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.OpenTime, request.StartTime, request.EndTime, direction)
                    .Select(x => 
                        new SharedKline(
                            request.Symbol,
                            symbol,
                            x.OpenTime,
                            x.ClosePrice,
                            x.HighPrice, 
                            x.LowPrice, 
                            x.OpenPrice,
                            new SharedOrderQuantity(x.Volume, x.QuoteVolume)))
                    .ToArray(), nextPageRequest);
        }

        #endregion
    }
}
