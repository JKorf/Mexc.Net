using Mexc.Net.Enums;
using Mexc.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis.Interfaces;
using CryptoExchange.Net.SharedApis.Models.Rest;
using CryptoExchange.Net.SharedApis.RequestModels;
using CryptoExchange.Net.SharedApis.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.SharedApis.Enums;
using CryptoExchange.Net.SharedApis.Models;

namespace Mexc.Net.Clients.SpotApi
{
    internal partial class MexcRestClientSpotApi : IMexcRestClientSpotApiShared
    {
        public string Exchange => MexcExchange.ExchangeName;

        public IEnumerable<SharedOrderType> SupportedOrderType { get; } = new[]
        {
            SharedOrderType.Limit,
            SharedOrderType.Market,
            SharedOrderType.LimitMaker
        };

        public IEnumerable<SharedTimeInForce> SupportedTimeInForce { get; } = new[]
        {
            SharedTimeInForce.GoodTillCanceled,
            SharedTimeInForce.ImmediateOrCancel,
            SharedTimeInForce.FillOrKill
        };

        public SharedQuantitySupport OrderQuantitySupport { get; } =
            new SharedQuantitySupport(
                SharedQuantityType.BaseAssetQuantity,
                SharedQuantityType.BaseAssetQuantity,
                SharedQuantityType.Both,
                SharedQuantityType.Both);

        async Task<ExchangeWebResult<IEnumerable<SharedKline>>> IKlineRestClient.GetKlinesAsync(GetKlinesRequest request, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeWebResult<IEnumerable<SharedKline>>(Exchange, new ArgumentError("Interval not supported"));

            var result = await ExchangeData.GetKlinesAsync(
                FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType),
                interval,
                request.StartTime,
                request.EndTime,
                request.Limit,
                ct: ct
                ).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedKline>>(Exchange, default);

            return result.AsExchangeResult(Exchange, result.Data.Select(x => new SharedKline(x.OpenTime, x.ClosePrice, x.HighPrice, x.LowPrice, x.OpenPrice, x.Volume)));
        }

        async Task<ExchangeWebResult<IEnumerable<SharedSpotSymbol>>> ISpotSymbolRestClient.GetSymbolsAsync(SharedRequest request, CancellationToken ct)
        {
            var result = await ExchangeData.GetExchangeInfoAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedSpotSymbol>>(Exchange, default);

            return result.AsExchangeResult(Exchange, result.Data.Symbols.Select(s => new SharedSpotSymbol(s.BaseAsset, s.QuoteAsset, s.Name)
            {
                PriceDecimals = s.QuoteAssetPrecision,
                QuantityDecimals = s.BaseAssetPrecision,
                QuantityStep = s.BaseQuantityPrecision
            }));
        }

        async Task<ExchangeWebResult<SharedTicker>> ITickerRestClient.GetTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var symbol = FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType);
            var result = await ExchangeData.GetTickerAsync(symbol, ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedTicker>(Exchange, default);

            return result.AsExchangeResult(Exchange, new SharedTicker(symbol, result.Data.LastPrice, result.Data.HighPrice, result.Data.LowPrice));
        }

        async Task<ExchangeWebResult<IEnumerable<SharedTicker>>> ITickerRestClient.GetTickersAsync(SharedRequest request, CancellationToken ct)
        {
            var result = await ExchangeData.GetTickersAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedTicker>>(Exchange, default);

            return result.AsExchangeResult<IEnumerable<SharedTicker>>(Exchange, result.Data.Select(x => new SharedTicker(x.Symbol, x.LastPrice, x.HighPrice, x.LowPrice)));
        }

        async Task<ExchangeWebResult<IEnumerable<SharedTrade>>> IRecentTradeRestClient.GetRecentTradesAsync(GetRecentTradesRequest request, CancellationToken ct)
        {
            var result = await ExchangeData.GetAggregatedTradeHistoryAsync(
                FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType),
                limit: request.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedTrade>>(Exchange, default);

            return result.AsExchangeResult(Exchange, result.Data.Select(x => new SharedTrade(x.Quantity, x.Price, x.Timestamp)));
        }

        async Task<ExchangeWebResult<IEnumerable<SharedBalance>>> IBalanceRestClient.GetBalancesAsync(SharedRequest request, CancellationToken ct)
        {
            var result = await Account.GetAccountInfoAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedBalance>>(Exchange, default);

            return result.AsExchangeResult(Exchange, result.Data.Balances.Select(x => new SharedBalance(x.Asset, x.Available, x.Total)));
        }

        async Task<ExchangeWebResult<SharedOrderId>> ISpotOrderRestClient.PlaceOrderAsync(PlaceSpotOrderRequest request, CancellationToken ct)
        {
            var result = await Trading.PlaceOrderAsync(
                FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType),
                request.Side == SharedOrderSide.Buy ? Enums.OrderSide.Buy : Enums.OrderSide.Sell,
                GetPlaceOrderType(request.OrderType, request.TimeInForce),
                request.Quantity,
                request.QuoteQuantity,
                request.Price,
                clientOrderId: request.ClientOrderId).ConfigureAwait(false);

            if (!result)
                return result.AsExchangeResult<SharedOrderId>(Exchange, default);

            return result.AsExchangeResult(Exchange, new SharedOrderId(result.Data.OrderId.ToString()));
        }

        async Task<ExchangeWebResult<SharedSpotOrder>> ISpotOrderRestClient.GetOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var order = await Trading.GetOrderAsync(FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType), request.OrderId).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedSpotOrder>(Exchange, default);

            return order.AsExchangeResult(Exchange, new SharedSpotOrder(
                order.Data.Symbol,
                order.Data.OrderId,
                ParseOrderType(order.Data.OrderType),
                order.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data.Status),
                order.Data.Timestamp)
            {
                ClientOrderId = order.Data.ClientOrderId,
                Price = order.Data.Price,
                Quantity = order.Data.Quantity,
                QuantityFilled = order.Data.QuantityFilled,
                QuoteQuantity = order.Data.QuoteQuantity,
                QuoteQuantityFilled = order.Data.QuoteQuantityFilled,
                TimeInForce = ParseTimeInForce(order.Data.OrderType),
                UpdateTime = order.Data.UpdateTime
            });
        }

        async Task<ExchangeWebResult<IEnumerable<SharedSpotOrder>>> ISpotOrderRestClient.GetOpenOrdersAsync(GetSpotOpenOrdersRequest request, CancellationToken ct)
        {
            if (request.BaseAsset == null || request.QuoteAsset == null)
                return new ExchangeWebResult<IEnumerable<SharedSpotOrder>>(Exchange, new ArgumentError("Symbol is required for Mexc GetOpenOrdersAsync"));

            var symbol = FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType);
            var order = await Trading.GetOpenOrdersAsync(symbol: symbol).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<IEnumerable<SharedSpotOrder>>(Exchange, default);

            return order.AsExchangeResult(Exchange, order.Data.Select(x => new SharedSpotOrder(
                x.Symbol,
                x.OrderId,
                ParseOrderType(x.OrderType),
                x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x.Status),
                x.Timestamp)
            {
                ClientOrderId = x.ClientOrderId,
                Price = x.Price,
                Quantity = x.Quantity,
                QuantityFilled = x.QuantityFilled,
                QuoteQuantity = x.QuoteQuantity,
                QuoteQuantityFilled = x.QuoteQuantityFilled,
                TimeInForce = ParseTimeInForce(x.OrderType),
                UpdateTime = x.UpdateTime
            }));
        }
        
        async Task<ExchangeWebResult<IEnumerable<SharedSpotOrder>>> ISpotOrderRestClient.GetClosedOrdersAsync(GetSpotClosedOrdersRequest request, CancellationToken ct)
        {
            var symbol = FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType);
            var order = await Trading.GetOrdersAsync(symbol: symbol, startTime: request.StartTime, endTime: request.EndTime, limit: request.Limit).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<IEnumerable<SharedSpotOrder>>(Exchange, default);

            return order.AsExchangeResult(Exchange, order.Data.Where(x => x.Status == OrderStatus.Filled || x.Status == OrderStatus.Canceled).Select(x => new SharedSpotOrder(
                x.Symbol,
                x.OrderId,
                ParseOrderType(x.OrderType),
                x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x.Status),
                x.Timestamp)
            {
                ClientOrderId = x.ClientOrderId,
                Price = x.Price,
                Quantity = x.Quantity,
                QuantityFilled = x.QuantityFilled,
                QuoteQuantity = x.QuoteQuantity,
                QuoteQuantityFilled = x.QuoteQuantityFilled,
                TimeInForce = ParseTimeInForce(x.OrderType),
                UpdateTime = x.UpdateTime
            }));
        }

        async Task<ExchangeWebResult<IEnumerable<SharedUserTrade>>> ISpotOrderRestClient.GetOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            var symbol = FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType);
            var order = await Trading.GetUserTradesAsync(symbol: symbol, request.OrderId).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<IEnumerable<SharedUserTrade>>(Exchange, default);

            return order.AsExchangeResult(Exchange, order.Data.Select(x => new SharedUserTrade(
                x.Symbol,
                x.OrderId.ToString(),
                x.Id.ToString(),
                x.Quantity,
                x.Price,
                x.Timestamp)
            {
                Fee = x.Fee,
                FeeAsset = x.FeeAsset,
                Role = x.IsMaker ? SharedRole.Maker : SharedRole.Taker
            }));
        }

        async Task<ExchangeWebResult<IEnumerable<SharedUserTrade>>> ISpotOrderRestClient.GetUserTradesAsync(GetUserTradesRequest request, INextPageToken? nextPageToken, CancellationToken ct)
        {
            // Determine page token
            DateTime? fromTimestamp = null;
            if (nextPageToken is DateTimeToken dateTimeToken)
                fromTimestamp = dateTimeToken.LastTime;

            // Get data
            var symbol = FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType);
            var order = await Trading.GetUserTradesAsync(
                symbol: symbol,
                startTime: fromTimestamp ?? request.StartTime,
                endTime: request.EndTime,
                limit : request.Limit ?? 100).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<IEnumerable<SharedUserTrade>>(Exchange, default);

            // Get next token
            DateTimeToken? nextToken = null;
            if (order.Data.Count() == (request.Limit ?? 100))
                nextToken = new DateTimeToken(order.Data.Max(o => o.Timestamp));
            
            return order.AsExchangeResult(Exchange, order.Data.Select(x => new SharedUserTrade(
                x.Symbol,
                x.OrderId.ToString(),
                x.Id.ToString(),
                x.Quantity,
                x.Price,
                x.Timestamp)
            {
                Fee = x.Fee,
                FeeAsset = x.FeeAsset,
                Role = x.IsMaker ? SharedRole.Maker : SharedRole.Taker
            }), nextToken);
        }

        async Task<ExchangeWebResult<SharedOrderId>> ISpotOrderRestClient.CancelOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var order = await Trading.CancelOrderAsync(FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType), request.OrderId).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedOrderId>(Exchange, default);

            return order.AsExchangeResult(Exchange, new SharedOrderId(request.OrderId));
        }

        private SharedOrderStatus ParseOrderStatus(OrderStatus status)
        {
            if (status == OrderStatus.New || status == OrderStatus.PartiallyFilled) return SharedOrderStatus.Open;
            if (status == OrderStatus.Canceled || status == OrderStatus.PartiallyCanceled) return SharedOrderStatus.Open;
            return SharedOrderStatus.Filled;
        }

        private SharedOrderType ParseOrderType(OrderType type)
        {
            if (type == OrderType.Market) return SharedOrderType.Market;
            if (type == OrderType.LimitMaker) return SharedOrderType.LimitMaker;
            if (type == OrderType.Limit) return SharedOrderType.Limit;

            return SharedOrderType.Other;
        }

        private SharedTimeInForce? ParseTimeInForce(OrderType type)
        {
            if (type == OrderType.ImmediateOrCancel) return SharedTimeInForce.ImmediateOrCancel;
            if (type == OrderType.FillOrKill) return SharedTimeInForce.FillOrKill;

            return null;
        }

        private OrderType GetPlaceOrderType(SharedOrderType type, SharedTimeInForce? tif)
        {
            if (type == SharedOrderType.Market) return OrderType.Market;
            if (type == SharedOrderType.LimitMaker) return OrderType.LimitMaker;
            if (tif == SharedTimeInForce.ImmediateOrCancel) return OrderType.ImmediateOrCancel;
            if (tif == SharedTimeInForce.FillOrKill) return OrderType.FillOrKill;

            return OrderType.Limit;
        }
    }
}
