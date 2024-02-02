using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.MessageParsing;
using CryptoExchange.Net.Sockets.MessageParsing.Interfaces;
using Mexc.Net.Enums;
using Mexc.Net.Interfaces.Clients.SpotApi;
using Mexc.Net.Objects.Models.Spot;
using Mexc.Net.Objects.Options;
using Mexc.Net.Objects.Sockets.Models;
using Mexc.Net.Objects.Sockets.Subscriptions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mexc.Net.Clients.SpotApi
{
    /// <inheritdoc />
    public class MexcSocketClientSpotApi : SocketApiClient, IMexcSocketClientSpotApi
    {
        private static readonly MessagePath _idPath = MessagePath.Get().Property("id");
        private static readonly MessagePath _channelPath = MessagePath.Get().Property("c");

        #region constructor/destructor

        internal MexcSocketClientSpotApi(ILogger logger, MexcSocketOptions options) :
            base(logger, options.Environment.SpotSocketAddress, options, options.SpotOptions)
        {
        }

        #endregion

        /// <inheritdoc />
        public override string? GetListenerIdentifier(IMessageAccessor messageAccessor)
        {
            var id = messageAccessor.GetValue<int?>(_idPath);
            if (id != null)
                return id.Value.ToString();

            return messageAccessor.GetValue<string>(_channelPath);
        }

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new MexcAuthenticationProvider(credentials);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<IEnumerable<MexcStreamTrade>>> handler, CancellationToken ct = default)
            => await SubscribeToTradeUpdatesAsync(new[] { symbol }, handler, ct).ConfigureAwait(false);

            /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<IEnumerable<MexcStreamTrade>>> handler, CancellationToken ct = default)
        {
            var subscription = new MexcSubscription<MexcTradeUpdate>(_logger, symbols.Select(s => "spot@public.deals.v3.api@" + s), x => handler(x.As(x.Data.Data)), false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<MexcStreamKline>> handler, CancellationToken ct = default)
            => await SubscribeToKlineUpdatesAsync(new[] { symbol }, interval, handler, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<MexcStreamKline>> handler, CancellationToken ct = default)
        {
            var subscription = new MexcSubscription<MexcKlineUpdate>(_logger, symbols.Select(s => "spot@public.kline.v3.api@" + s + "@" + GetIntervalString(interval)), x => handler(x.As(x.Data.Data)), false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, Action<DataEvent<MexcStreamOrderBook>> handler, CancellationToken ct = default)
            => await SubscribeToOrderBookUpdatesAsync(new[] { symbol }, handler, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<MexcStreamOrderBook>> handler, CancellationToken ct = default)
        {
            var subscription = new MexcSubscription<MexcStreamOrderBook>(_logger, symbols.Select(s => "spot@public.increase.depth.v3.api@" + s), handler, false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int depth, Action<DataEvent<MexcStreamOrderBook>> handler, CancellationToken ct = default)
            => await SubscribeToPartialOrderBookUpdatesAsync(new[] { symbol }, depth, handler, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(IEnumerable<string> symbols, int depth, Action<DataEvent<MexcStreamOrderBook>> handler, CancellationToken ct = default)
        {
            var subscription = new MexcSubscription<MexcStreamOrderBook>(_logger, symbols.Select(s => "spot@public.limit.depth.v3.api@" + s + "@" + depth), handler, false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol, Action<DataEvent<MexcStreamBookTick>> handler, CancellationToken ct = default)
            => await SubscribeToBookTickerUpdatesAsync(new[] { symbol }, handler, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<MexcStreamBookTick>> handler, CancellationToken ct = default)
        {
            var subscription = new MexcSubscription<MexcStreamBookTick>(_logger, symbols.Select(s => "spot@public.bookTicker.v3.api@" + s), handler, false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(string symbol, string timezone, Action<DataEvent<MexcStreamMiniTick>> handler, CancellationToken ct = default)
            => await SubscribeToMiniTickerUpdatesAsync(new[] { symbol }, timezone, handler, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(IEnumerable<string> symbols, string timezone, Action<DataEvent<MexcStreamMiniTick>> handler, CancellationToken ct = default)
        {
            var subscription = new MexcSubscription<MexcStreamMiniTick>(_logger, symbols.Select(s => "spot@public.miniTicker.v3.api@" + s + "@" + timezone), handler, false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(string timezone, Action<DataEvent<IEnumerable<MexcStreamMiniTick>>> handler, CancellationToken ct = default)
        {
            var subscription = new MexcSubscription<IEnumerable<MexcStreamMiniTick>>(_logger, new[] { "spot@public.miniTickers.v3.api@" + timezone }, handler, false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAccountUpdatesAsync(string listenKey, Action<DataEvent<MexcAccountUpdate>> handler, CancellationToken ct = default)
        {
            var subscription = new MexcSubscription<MexcAccountUpdate>(_logger, new[] { "spot@private.account.v3.api" }, handler, false);
            return await SubscribeAsync(BaseAddress + "?listenKey=" + listenKey, subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string listenKey, Action<DataEvent<MexcUserOrderUpdate>> handler, CancellationToken ct = default)
        {
            var subscription = new MexcSubscription<MexcUserOrderUpdate>(_logger, new[] { "spot@private.orders.v3.api" }, handler, false);
            return await SubscribeAsync(BaseAddress + "?listenKey=" + listenKey, subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(string listenKey, Action<DataEvent<MexcUserTradeUpdate>> handler, CancellationToken ct = default)
        {
            var subscription = new MexcSubscription<MexcUserTradeUpdate>(_logger, new[] { "spot@private.deals.v3.api" }, handler, false);
            return await SubscribeAsync(BaseAddress + "?listenKey=" + listenKey, subscription, ct).ConfigureAwait(false);
        }

        private static string GetIntervalString(KlineInterval interval)
            => interval switch
            {
                KlineInterval.OneMinute => "Min1",
                KlineInterval.FiveMinutes => "Min5",
                KlineInterval.FifteenMinutes => "Min15",
                KlineInterval.ThirtyMinutes => "Min30",
                KlineInterval.OneHour => "Min60",
                KlineInterval.FourHours => "Hour4",
                KlineInterval.OneDay => "Day1",
                KlineInterval.OneWeek => "Week1",
                KlineInterval.OneMonth => "Month1",
                _ => throw new InvalidOperationException()
            };
    }
}
