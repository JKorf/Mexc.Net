using System;
using System.Collections.Generic;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Mexc.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using System.Threading.Tasks;
using System.Threading;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Mexc.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.Interfaces.CommonClients;
using System.Globalization;
using CryptoExchange.Net.CommonObjects;
using System.Linq;
using Mexc.Net.Enums;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;

namespace Mexc.Net.Clients.SpotApi
{
    /// <inheritdoc />
    public class MexcRestClientSpotApi : RestApiClient, IMexcRestClientSpotApi, ISpotClient
    {
        internal static TimeSyncState _timeSyncState = new TimeSyncState("Spot Api");

        /// <inheritdoc />
        public IMexcRestClientSpotApiAccount Account { get; }
        /// <inheritdoc />
        public IMexcRestClientSpotApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IMexcRestClientSpotApiTrading Trading { get; }

        /// <inheritdoc />
        public ISpotClient CommonSpotClient => this;

        /// <inheritdoc />
        public string ExchangeName => "Mexc";

        /// <summary>
        /// Event triggered when an order is placed via this client. Only available for Spot orders
        /// </summary>
        public event Action<OrderId>? OnOrderPlaced;
        /// <summary>
        /// Event triggered when an order is canceled via this client. Note that this does not trigger when using CancelAllOrdersAsync. Only available for Spot orders
        /// </summary>
        public event Action<OrderId>? OnOrderCanceled;

        internal readonly string _brokerId;

        #region constructor/destructor
        internal MexcRestClientSpotApi(ILogger logger, HttpClient? httpClient, MexcRestOptions options)
            : base(logger, httpClient, options.Environment.SpotRestAddress, options, options.SpotOptions)
        {
            Account = new MexcRestClientSpotApiAccount(logger, this);
            ExchangeData = new MexcRestClientSpotApiExchangeData(logger, this);
            Trading = new MexcRestClientSpotApiTrading(logger, this);

            RequestBodyEmptyContent = "";
            RequestBodyFormat = RequestBodyFormat.FormData;
            ArraySerialization = ArrayParametersSerialization.MultipleValues;

            ParameterPositions[HttpMethod.Post] = HttpMethodParameterPosition.InUri;
            ParameterPositions[HttpMethod.Delete] = HttpMethodParameterPosition.InUri;
            ParameterPositions[HttpMethod.Put] = HttpMethodParameterPosition.InUri;

            _brokerId = !string.IsNullOrEmpty(options.BrokerId) ? options.BrokerId! : "EASYT";
            StandardRequestHeaders = new Dictionary<string, string>()
            {
                { "source", _brokerId }
            };
        }
        #endregion

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new MexcAuthenticationProvider(credentials);

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset) => baseAsset.ToUpperInvariant() + quoteAsset.ToUpperInvariant();

        internal async Task<WebCallResult<T>> SendRequestInternal<T>(string path, HttpMethod method, CancellationToken cancellationToken,
            Dictionary<string, object>? parameters = null, bool signed = false, HttpMethodParameterPosition? postPosition = null,
            ArrayParametersSerialization? arraySerialization = null) where T : class
        {
            var result = await SendRequestAsync<T>(new Uri(BaseAddress.AppendPath(path)), method, cancellationToken, parameters, signed, null, postPosition, arraySerialization, 0).ConfigureAwait(false);
            if (!result && result.Error!.Code == 700003 && (ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp))
            {
                _logger.Log(LogLevel.Debug, "Received Invalid Timestamp error, triggering new time sync");
                _timeSyncState.LastSyncTime = DateTime.MinValue;
            }
            return result;
        }

        /// <inheritdoc />
        protected override Error ParseErrorResponse(int httpStatusCode, IEnumerable<KeyValuePair<string, IEnumerable<string>>> responseHeaders, IMessageAccessor accessor)
        {
            if (!accessor.IsJson)
                return new ServerError(accessor.GetOriginalString());

            var code = accessor.GetValue<int?>(MessagePath.Get().Property("code"));
            var msg = accessor.GetValue<string>(MessagePath.Get().Property("msg"));
            if (msg == null)
                return new ServerError(accessor.GetOriginalString());

            if (code == null)
                return new ServerError(msg);

            return new ServerError(code.Value, msg);
        }

        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        public override TimeSyncInfo? GetTimeSyncInfo()
            => new TimeSyncInfo(_logger, (ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp), (ApiOptions.TimestampRecalculationInterval ?? ClientOptions.TimestampRecalculationInterval), _timeSyncState);

        /// <inheritdoc />
        public override TimeSpan? GetTimeOffset()
            => _timeSyncState.TimeOffset;

        /// <inheritdoc />
        public string GetSymbolName(string baseAsset, string quoteAsset) =>
            (baseAsset + quoteAsset).ToUpper(CultureInfo.InvariantCulture);

        internal void InvokeOrderPlaced(OrderId id)
        {
            OnOrderPlaced?.Invoke(id);
        }

        internal void InvokeOrderCanceled(OrderId id)
        {
            OnOrderCanceled?.Invoke(id);
        }

        async Task<WebCallResult<OrderId>> ISpotClient.PlaceOrderAsync(string symbol, CommonOrderSide side, CommonOrderType type, decimal quantity, decimal? price, string? accountId, string? clientOrderId, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException(nameof(symbol) + " required for Mexc " + nameof(ISpotClient.PlaceOrderAsync), nameof(symbol));

            var order = await Trading.PlaceOrderAsync(symbol, GetOrderSide(side), GetOrderType(type), quantity, price: price, clientOrderId: clientOrderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.As<OrderId>(null);

            return order.As(new OrderId
            {
                SourceObject = order,
                Id = order.Data.OrderId
            });
        }

        async Task<WebCallResult<Order>> IBaseRestClient.GetOrderAsync(string orderId, string? symbol, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException(nameof(symbol) + " required for Mexc " + nameof(ISpotClient.GetOrderAsync), nameof(symbol));

            var order = await Trading.GetOrderAsync(symbol!, orderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.As<Order>(null);

            return order.As(new Order
            {
                SourceObject = order,
                Id = order.Data.OrderId,
                Symbol = order.Data.Symbol,
                Price = order.Data.Price,
                Quantity = order.Data.Quantity,
                QuantityFilled = order.Data.QuantityFilled,
                Side = order.Data.Side == Enums.OrderSide.Buy ? CommonOrderSide.Buy : CommonOrderSide.Sell,
                Type = GetOrderType(order.Data.OrderType),
                Status = GetOrderStatus(order.Data.Status),
                Timestamp = order.Data.Timestamp
            });
        }

        async Task<WebCallResult<IEnumerable<UserTrade>>> IBaseRestClient.GetOrderTradesAsync(string orderId, string? symbol, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException(nameof(symbol) + " required for Mexc " + nameof(ISpotClient.GetOrderTradesAsync), nameof(symbol));

            var trades = await Trading.GetUserTradesAsync(symbol!, orderId, ct: ct).ConfigureAwait(false);
            if (!trades)
                return trades.As<IEnumerable<UserTrade>>(null);

            return trades.As(trades.Data.Select(t =>
                new UserTrade
                {
                    SourceObject = t,
                    Id = t.Id.ToString(CultureInfo.InvariantCulture),
                    OrderId = t.OrderId.ToString(CultureInfo.InvariantCulture),
                    Symbol = t.Symbol,
                    Price = t.Price,
                    Quantity = t.Quantity,
                    Fee = t.Fee,
                    FeeAsset = t.FeeAsset,
                    Timestamp = t.Timestamp
                }));
        }

        async Task<WebCallResult<IEnumerable<Order>>> IBaseRestClient.GetOpenOrdersAsync(string? symbol, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException(nameof(symbol) + " required for Mexc " + nameof(ISpotClient.GetOpenOrdersAsync), nameof(symbol));

            var orderInfo = await Trading.GetOpenOrdersAsync(symbol!, ct: ct).ConfigureAwait(false);
            if (!orderInfo)
                return orderInfo.As<IEnumerable<Order>>(null);

            return orderInfo.As(orderInfo.Data.Select(s =>
                new Order
                {
                    SourceObject = s,
                    Id = s.OrderId,
                    Symbol = s.Symbol,
                    Side = s.Side == Enums.OrderSide.Buy ? CommonOrderSide.Buy : CommonOrderSide.Sell,
                    Price = s.Price,
                    Quantity = s.Quantity,
                    QuantityFilled = s.QuantityFilled,
                    Type = GetOrderType(s.OrderType),
                    Status = GetOrderStatus(s.Status),
                    Timestamp = s.Timestamp
                }));
        }

        async Task<WebCallResult<IEnumerable<Order>>> IBaseRestClient.GetClosedOrdersAsync(string? symbol, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException(nameof(symbol) + " required for Mexc " + nameof(ISpotClient.GetClosedOrdersAsync), nameof(symbol));

            var orderInfo = await Trading.GetOrdersAsync(symbol!, ct: ct).ConfigureAwait(false);
            if (!orderInfo)
                return orderInfo.As<IEnumerable<Order>>(null);

            return orderInfo.As(orderInfo.Data.Where(o => o.Status == Enums.OrderStatus.Canceled || o.Status == Enums.OrderStatus.Filled).Select(s =>
                new Order
                {
                    SourceObject = s,
                    Id = s.OrderId,
                    Symbol = s.Symbol,
                    Price = s.Price,
                    Quantity = s.Quantity,
                    QuantityFilled = s.QuantityFilled,
                    Side = s.Side == Enums.OrderSide.Buy ? CommonOrderSide.Buy : CommonOrderSide.Sell,
                    Type = GetOrderType(s.OrderType),
                    Status = GetOrderStatus(s.Status),
                    Timestamp = s.Timestamp
                }));
        }

        async Task<WebCallResult<OrderId>> IBaseRestClient.CancelOrderAsync(string orderId, string? symbol, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException(nameof(symbol) + " required for Mexc " + nameof(ISpotClient.CancelOrderAsync), nameof(symbol));

            var order = await Trading.CancelOrderAsync(symbol!, orderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.As<OrderId>(null);

            return order.As(new OrderId
            {
                SourceObject = order,
                Id = order.Data.OrderId
            });
        }

        async Task<WebCallResult<IEnumerable<Symbol>>> IBaseRestClient.GetSymbolsAsync(CancellationToken ct)
        {
            var exchangeInfo = await ExchangeData.GetExchangeInfoAsync(ct: ct).ConfigureAwait(false);
            if (!exchangeInfo)
                return exchangeInfo.As<IEnumerable<Symbol>>(null);

            return exchangeInfo.As(exchangeInfo.Data.Symbols.Select(s =>
                new Symbol
                {
                    SourceObject = s,
                    Name = s.Name,
                    QuantityDecimals = s.BaseAssetPrecision,
                    PriceDecimals = s.QuoteAssetPrecision,
                    QuantityStep = s.BaseQuantityPrecision,
                    PriceStep = s.QuoteQuantityPrecision
                }));
        }

        async Task<WebCallResult<Ticker>> IBaseRestClient.GetTickerAsync(string symbol, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException(nameof(symbol) + " required for Mexc " + nameof(ISpotClient.GetTickerAsync), nameof(symbol));

            var ticker = await ExchangeData.GetTickerAsync(symbol, ct).ConfigureAwait(false);
            if (!ticker)
                return ticker.As<Ticker>(null);

            return ticker.As(new Ticker
            {
                SourceObject = ticker.Data,
                Symbol = ticker.Data.Symbol,
                HighPrice = ticker.Data.HighPrice,
                LowPrice = ticker.Data.LowPrice,
                Price24H = ticker.Data.PrevDayClosePrice,
                LastPrice = ticker.Data.LastPrice,
                Volume = ticker.Data.Volume
            });
        }

        async Task<WebCallResult<IEnumerable<Ticker>>> IBaseRestClient.GetTickersAsync(CancellationToken ct)
        {
            var tickers = await ExchangeData.GetTickersAsync(ct: ct).ConfigureAwait(false);
            if (!tickers)
                return tickers.As<IEnumerable<Ticker>>(null);

            return tickers.As(tickers.Data.Select(t => new Ticker
            {
                SourceObject = t,
                Symbol = t.Symbol,
                HighPrice = t.HighPrice,
                LowPrice = t.LowPrice,
                Price24H = t.PrevDayClosePrice,
                LastPrice = t.LastPrice,
                Volume = t.Volume
            }));
        }

        async Task<WebCallResult<IEnumerable<Kline>>> IBaseRestClient.GetKlinesAsync(string symbol, TimeSpan timespan, DateTime? startTime, DateTime? endTime, int? limit, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException(nameof(symbol) + " required for Mexc " + nameof(ISpotClient.GetKlinesAsync), nameof(symbol));

            var klines = await ExchangeData.GetKlinesAsync(symbol, GetKlineIntervalFromTimespan(timespan), startTime, endTime, limit, ct: ct).ConfigureAwait(false);
            if (!klines)
                return klines.As<IEnumerable<Kline>>(null);

            return klines.As(klines.Data.Select(t => new Kline
            {
                SourceObject = t,
                HighPrice = t.HighPrice,
                LowPrice = t.LowPrice,
                OpenTime = t.OpenTime,
                ClosePrice = t.ClosePrice,
                OpenPrice = t.OpenPrice,
                Volume = t.Volume
            }));
        }

        async Task<WebCallResult<OrderBook>> IBaseRestClient.GetOrderBookAsync(string symbol, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException(nameof(symbol) + " required for Mexc " + nameof(ISpotClient.GetOrderBookAsync), nameof(symbol));

            var orderbook = await ExchangeData.GetOrderBookAsync(symbol, ct: ct).ConfigureAwait(false);
            if (!orderbook)
                return orderbook.As<OrderBook>(null);

            return orderbook.As(new OrderBook
            {
                SourceObject = orderbook.Data,
                Asks = orderbook.Data.Asks.Select(a => new OrderBookEntry { Price = a.Price, Quantity = a.Quantity }),
                Bids = orderbook.Data.Bids.Select(b => new OrderBookEntry { Price = b.Price, Quantity = b.Quantity })
            });
        }

        async Task<WebCallResult<IEnumerable<Trade>>> IBaseRestClient.GetRecentTradesAsync(string symbol, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException(nameof(symbol) + " required for Mexc " + nameof(ISpotClient.GetRecentTradesAsync), nameof(symbol));

            var trades = await ExchangeData.GetRecentTradesAsync(symbol, ct: ct).ConfigureAwait(false);
            if (!trades)
                return trades.As<IEnumerable<Trade>>(null);

            return trades.As(trades.Data.Select(t => new Trade
            {
                SourceObject = t,
                Symbol = symbol,
                Price = t.Price,
                Quantity = t.Quantity,
                Timestamp = t.Timestamp
            }));
        }

        async Task<WebCallResult<IEnumerable<Balance>>> IBaseRestClient.GetBalancesAsync(string? accountId, CancellationToken ct)
        {
            var balances = await Account.GetAccountInfoAsync(ct: ct).ConfigureAwait(false);
            if (!balances)
                return balances.As<IEnumerable<Balance>>(null);

            return balances.As(balances.Data.Balances.Select(t => new Balance
            {
                SourceObject = t,
                Asset = t.Asset,
                Available = t.Available,
                Total = t.Total
            }));
        }

        private static CommonOrderType GetOrderType(OrderType orderType)
        {
            if (orderType == OrderType.Limit)
                return CommonOrderType.Limit;
            if (orderType == OrderType.Market)
                return CommonOrderType.Market;
            return CommonOrderType.Other;
        }

        private static CommonOrderStatus GetOrderStatus(OrderStatus orderStatus)
        {
            if (orderStatus == OrderStatus.New || orderStatus == OrderStatus.PartiallyFilled)
                return CommonOrderStatus.Active;
            if (orderStatus == OrderStatus.Filled)
                return CommonOrderStatus.Filled;
            return CommonOrderStatus.Canceled;
        }

        private static OrderSide GetOrderSide(CommonOrderSide side)
        {
            if (side == CommonOrderSide.Sell) return OrderSide.Sell;
            if (side == CommonOrderSide.Buy) return OrderSide.Buy;

            throw new ArgumentException("Unsupported order side for Mexc order: " + side);
        }

        private static OrderType GetOrderType(CommonOrderType type)
        {
            if (type == CommonOrderType.Limit) return OrderType.Limit;
            if (type == CommonOrderType.Market) return OrderType.Market;

            throw new ArgumentException("Unsupported order type for Mexc order: " + type);
        }

        private static KlineInterval GetKlineIntervalFromTimespan(TimeSpan timeSpan)
        {
            if (timeSpan == TimeSpan.FromMinutes(1)) return KlineInterval.OneMinute;
            if (timeSpan == TimeSpan.FromMinutes(5)) return KlineInterval.FiveMinutes;
            if (timeSpan == TimeSpan.FromMinutes(15)) return KlineInterval.FifteenMinutes;
            if (timeSpan == TimeSpan.FromMinutes(30)) return KlineInterval.ThirtyMinutes;
            if (timeSpan == TimeSpan.FromHours(1)) return KlineInterval.OneHour;
            if (timeSpan == TimeSpan.FromHours(4)) return KlineInterval.FourHours;
            if (timeSpan == TimeSpan.FromDays(1)) return KlineInterval.OneDay;
            if (timeSpan == TimeSpan.FromDays(7)) return KlineInterval.OneWeek;
            if (timeSpan == TimeSpan.FromDays(30) || timeSpan == TimeSpan.FromDays(31)) return KlineInterval.OneMonth;

            throw new ArgumentException("Unsupported timespan for Mexc Klines, check supported intervals using Mexc.Net.Enums.KlineInterval");
        }

    }
}
