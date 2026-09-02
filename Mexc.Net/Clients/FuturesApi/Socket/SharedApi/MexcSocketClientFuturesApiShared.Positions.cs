using Mexc.Net.Interfaces.Clients.FuturesApi;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using Mexc.Net.Enums;
using CryptoExchange.Net;

namespace Mexc.Net.Clients.FuturesApi
{
    internal partial class MexcSocketClientFuturesSharedApi
    {
        #region Position client
        public SubscribePositionOptions SubscribePositionOptions { get; } = new SubscribePositionOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(SubscribePositionRequest request, Action<DataEvent<SharedPosition[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribePositionOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await _api.SubscribeToUserDataUpdatesAsync(
                positionUpdateHandler: update => handler(update.ToType<SharedPosition[]>([
                    new SharedPosition(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.Symbol),
                        update.Data.Symbol, 
                        new SharedOrderQuantity(contractQuantity: update.Data.PositionSize), 
                        update.Data.UpdateTime)
                    {
                        Id = update.Data.PositionId.ToString(),
                        AverageOpenPrice = update.Data.HoldAveragePrice,
                        PositionMode = SharedPositionMode.HedgeMode,
                        PositionSide = update.Data.PositionSide == Enums.PositionSide.Short ? SharedPositionSide.Short : SharedPositionSide.Long,
                        LiquidationPrice = update.Data.LiquidationPrice,
                        Leverage = update.Data.Leverage,
                        UnrealizedPnl = update.Data.Pnl
                    }])),
                ct: ct).ConfigureAwait(false);
            return result;
        }

        #endregion
    }
}
