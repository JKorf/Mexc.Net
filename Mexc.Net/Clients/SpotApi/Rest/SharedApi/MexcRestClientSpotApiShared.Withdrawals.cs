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
        #region Withdrawal client


        Task<HttpResult<SharedWithdrawal[]>> IWithdrawalRestClient.GetWithdrawalsAsync(GetWithdrawalsRequest request, PageRequest? pageRequest, CancellationToken ct)
            => GetWithdrawalHistoryAsync(request, pageRequest, ct);
        GetWithdrawalHistoryOptions IWithdrawalRestClient.GetWithdrawalsOptions => GetWithdrawalHistoryOptions;

        public GetWithdrawalHistoryOptions GetWithdrawalHistoryOptions { get; } = new GetWithdrawalHistoryOptions(_exchangeName, false, true, true, 1000)
        {
            MaxAge = TimeSpan.FromDays(83)
        };
        public async Task<HttpResult<SharedWithdrawal[]>> GetWithdrawalHistoryAsync(GetWithdrawalsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetWithdrawalHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedWithdrawal[]>(Exchange, validationError);

            // Determine page token
            int limit = request.Limit ?? 1000;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, maxPeriod: TimeSpan.FromDays(7));

            // Get data
            var result = await _api.Account.GetWithdrawHistoryAsync(
                request.Asset,
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedWithdrawal[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                    () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.ApplyTime)),
                    result.Data.Length,
                    result.Data.Select(x => x.ApplyTime),
                    request.StartTime,
                    request.EndTime ?? DateTime.UtcNow,
                    pageParams,
                    TimeSpan.FromDays(7),
                    TimeSpan.FromDays(83));

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.ApplyTime, request.StartTime, request.EndTime, direction)
                .Select(x =>
                    new SharedWithdrawal(
                        x.Asset,
                        x.Address ?? string.Empty,
                        x.Quantity,
                        x.Status == WithdrawStatus.Success,
                        x.ApplyTime,
                        GetWithdrawalStatus(x))
                    {
                        Id = x.Id,
                        Confirmations = x.Confirmations,
                        Network = x.Network,
                        Tag = x.Memo,
                        TransactionId = x.TransactionId,
                        Fee = x.TransactionFee
                    })
                .ToArray(), nextPageRequest);
        }

        private SharedTransferStatus GetWithdrawalStatus(MexcWithdrawal x)
        {
            if (x.Status == WithdrawStatus.Applied
                || x.Status == WithdrawStatus.Auditing
                || x.Status == WithdrawStatus.Manual
                || x.Status == WithdrawStatus.Processing
                || x.Status == WithdrawStatus.Waiting
                || x.Status == WithdrawStatus.WaitConfirmations
                || x.Status == WithdrawStatus.WaitPackaging)
            {
                return SharedTransferStatus.InProgress;
            }

            if (x.Status == WithdrawStatus.Success)
                return SharedTransferStatus.Completed;

            if (x.Status == WithdrawStatus.Canceled
                || x.Status == WithdrawStatus.Failed)
            {
                return SharedTransferStatus.Failed;
            }

            return SharedTransferStatus.Unknown;
        }

        #endregion

        #region Withdraw client

        public WithdrawOptions WithdrawOptions { get; } = new WithdrawOptions(_exchangeName);
        public async Task<HttpResult<SharedId>> WithdrawAsync(WithdrawRequest request, CancellationToken ct)
        {
            var validationError = WithdrawOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var withdrawal = await _api.Account.WithdrawAsync(
                request.Asset,
                request.Address,
                request.Quantity,
                network: request.Network,
                memo: request.AddressTag,
                ct: ct).ConfigureAwait(false);
            if (!withdrawal.Success)
                return HttpResult.Fail<SharedId>(withdrawal);

            return HttpResult.Ok(withdrawal, new SharedId(withdrawal.Data.Id));
        }

        #endregion
    }
}
