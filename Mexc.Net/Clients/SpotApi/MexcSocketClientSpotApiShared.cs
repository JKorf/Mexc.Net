using Mexc.Net;
using Mexc.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis.RequestModels;
using CryptoExchange.Net.SharedApis.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.SharedApis.Enums;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis.Models.Socket;
using CryptoExchange.Net.SharedApis.Interfaces.Socket;
using CryptoExchange.Net.SharedApis.SubscribeModels;
using Mexc.Net.Objects.Options;
using CryptoExchange.Net.SharedApis.Models;

namespace Mexc.Net.Clients.SpotApi
{
    internal partial class MexcSocketClientSpotApi : IMexcSocketClientSpotApiShared
    {
        public string Exchange => MexcExchange.ExchangeName;

        async Task<ExchangeResult<UpdateSubscription>> ITickerSocketClient.SubscribeToTickerUpdatesAsync(TickerSubscribeRequest request, Action<DataEvent<SharedSpotTicker>> handler, CancellationToken ct)
        {
            var symbol = FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType);
            var result = await SubscribeToMiniTickerUpdatesAsync(symbol, update => handler(update.As(new SharedSpotTicker(symbol, update.Data.LastPrice, update.Data.HighPrice, update.Data.LowPrice, update.Data.Volume)))).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }

        async Task<ExchangeResult<UpdateSubscription>> ITradeSocketClient.SubscribeToTradeUpdatesAsync(TradeSubscribeRequest request, Action<DataEvent<IEnumerable<SharedTrade>>> handler, CancellationToken ct)
        {
            var symbol = FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType);
            var result = await SubscribeToTradeUpdatesAsync(symbol, update => handler(update.As(update.Data.Select(x => new SharedTrade(x.Quantity, x.Price, x.Timestamp)))), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }

        async Task<ExchangeResult<UpdateSubscription>> IBookTickerSocketClient.SubscribeToBookTickerUpdatesAsync(BookTickerSubscribeRequest request, Action<DataEvent<SharedBookTicker>> handler, CancellationToken ct)
        {
            var symbol = FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType);
            var result = await SubscribeToBookTickerUpdatesAsync(symbol, update => handler(update.As(new SharedBookTicker(update.Data.BestAskPrice, update.Data.BestAskQuantity, update.Data.BestBidPrice, update.Data.BestBidQuantity))), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }

        async Task<ExchangeResult<UpdateSubscription>> IBalanceSocketClient.SubscribeToBalanceUpdatesAsync(ApiType? apiType, Action<DataEvent<IEnumerable<SharedBalance>>> handler, CancellationToken ct)
        {
            var restClient = new MexcRestClient(opts =>
            {
                opts.ApiCredentials = ApiOptions.ApiCredentials ?? ClientOptions.ApiCredentials;
                opts.Environment = ((MexcSocketOptions)ClientOptions).Environment;
            });

            var listenKey = await restClient.SpotApi.Account.StartUserStreamAsync().ConfigureAwait(false);
            if (!listenKey)
                return new ExchangeResult<UpdateSubscription>(Exchange, listenKey.As<UpdateSubscription>(default));

            var result = await SubscribeToAccountUpdatesAsync(listenKey.Data,
                update => handler(update.As<IEnumerable<SharedBalance>>(new[] { new SharedBalance(update.Data.Asset, update.Data.Free, update.Data.Free + update.Data.Frozen) })),
                ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }

        async Task<ExchangeResult<UpdateSubscription>> ISpotOrderSocketClient.SubscribeToSpotOrderUpdatesAsync(Action<DataEvent<IEnumerable<SharedSpotOrder>>> handler, CancellationToken ct)
        {
            var restClient = new MexcRestClient(opts =>
            {
                opts.ApiCredentials = ApiOptions.ApiCredentials ?? ClientOptions.ApiCredentials;
                opts.Environment = ((MexcSocketOptions)ClientOptions).Environment;
            });

            var listenKey = await restClient.SpotApi.Account.StartUserStreamAsync().ConfigureAwait(false);
            if (!listenKey)
                return new ExchangeResult<UpdateSubscription>(Exchange, listenKey.As<UpdateSubscription>(default));

            var result = await SubscribeToOrderUpdatesAsync(listenKey.Data,
                update => handler(update.As<IEnumerable<SharedSpotOrder>>(new[] {
                    new SharedSpotOrder(
                        update.Symbol!,
                        update.Data.OrderId!,
                        update.Data.OrderType == Enums.OrderType.Limit ? SharedOrderType.Limit : update.Data.OrderType == Enums.OrderType.Market ? SharedOrderType.Market : SharedOrderType.Other,
                        update.Data.Side == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        (update.Data.Status == Enums.OrderStatus.Canceled || update.Data.Status == Enums.OrderStatus.PartiallyCanceled) ? SharedOrderStatus.Canceled : (update.Data.Status == Enums.OrderStatus.New || update.Data.Status == Enums.OrderStatus.PartiallyFilled) ? SharedOrderStatus.Open : SharedOrderStatus.Filled,
                        update.Data.Timestamp)
                    {
                        ClientOrderId = update.Data.ClientOrderId,
                        Price = update.Data.Price,
                        Quantity = update.Data.Quantity,
                        QuantityFilled = update.Data.Quantity - update.Data.QuantityRemaining,
                        QuoteQuantity = update.Data.QuoteQuantity,
                        QuoteQuantityFilled = update.Data.QuoteQuantity - update.Data.QuoteQuantityRemaining,
                        AveragePrice = update.Data.AveragePrice,
                        UpdateTime = update.Data.Timestamp,
                        TimeInForce = update.Data.OrderType == Enums.OrderType.ImmediateOrCancel ? SharedTimeInForce.ImmediateOrCancel : update.Data.OrderType == Enums.OrderType.FillOrKill ? SharedTimeInForce.FillOrKill : null
                    }
                })),
                ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }

        async Task<ExchangeResult<UpdateSubscription>> ISpotUserTradeSocketClient.SubscribeToUserTradeUpdatesAsync(ApiType? apiType, Action<DataEvent<IEnumerable<SharedUserTrade>>> handler, CancellationToken ct)
        {
            var restClient = new MexcRestClient(opts =>
            {
                opts.ApiCredentials = ApiOptions.ApiCredentials ?? ClientOptions.ApiCredentials;
                opts.Environment = ((MexcSocketOptions)ClientOptions).Environment;
            });

#warning Listenkey needs to be kept alive, do we handle or should user?
            var listenKey = await restClient.SpotApi.Account.StartUserStreamAsync().ConfigureAwait(false);
            if (!listenKey)
                return new ExchangeResult<UpdateSubscription>(Exchange, listenKey.As<UpdateSubscription>(default));

            var result = await SubscribeToUserTradeUpdatesAsync(
                listenKey.Data,
                update => handler(update.As<IEnumerable<SharedUserTrade>>(new[] {
                    new SharedUserTrade(
                        update.Symbol!,
                        update.Data.OrderId,
                        update.Data.TradeId.ToString(),
                        update.Data.Quantity,
                        update.Data.Price,                        
                        update.Data.TradeTime)
                    {
                        Role = update.Data.IsMaker ? SharedRole.Maker : SharedRole.Taker,
                        Fee = update.Data.Fee,
                        FeeAsset = update.Data.FeeAsset
                    }
                })),
                ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
    }
}
