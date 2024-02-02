using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Spot;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mexc.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Spot API socket subscriptions and requests
    /// </summary>
    public interface IMexcSocketClientSpotApi: ISocketApiClient
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="handler"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<IEnumerable<MexcStreamTrade>>> handler, CancellationToken ct = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbols"></param>
        /// <param name="handler"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<IEnumerable<MexcStreamTrade>>> handler, CancellationToken ct = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="interval"></param>
        /// <param name="handler"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<MexcStreamKline>> handler, CancellationToken ct = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbols"></param>
        /// <param name="interval"></param>
        /// <param name="handler"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<MexcStreamKline>> handler, CancellationToken ct = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="handler"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, Action<DataEvent<MexcStreamOrderBook>> handler, CancellationToken ct = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbols"></param>
        /// <param name="handler"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<MexcStreamOrderBook>> handler, CancellationToken ct = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="depth"></param>
        /// <param name="handler"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int depth, Action<DataEvent<MexcStreamOrderBook>> handler, CancellationToken ct = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbols"></param>
        /// <param name="depth"></param>
        /// <param name="handler"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(IEnumerable<string> symbols, int depth, Action<DataEvent<MexcStreamOrderBook>> handler, CancellationToken ct = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="handler"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol, Action<DataEvent<MexcStreamBookTick>> handler, CancellationToken ct = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbols"></param>
        /// <param name="handler"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<MexcStreamBookTick>> handler, CancellationToken ct = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="timezone"></param>
        /// <param name="handler"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(string symbol, string timezone, Action<DataEvent<MexcStreamMiniTick>> handler, CancellationToken ct = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbols"></param>
        /// <param name="timezone"></param>
        /// <param name="handler"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(IEnumerable<string> symbols, string timezone, Action<DataEvent<MexcStreamMiniTick>> handler, CancellationToken ct = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timezone"></param>
        /// <param name="handler"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(string timezone, Action<DataEvent<IEnumerable<MexcStreamMiniTick>>> handler, CancellationToken ct = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listenKey"></param>
        /// <param name="handler"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAccountUpdatesAsync(string listenKey, Action<DataEvent<MexcAccountUpdate>> handler, CancellationToken ct = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listenKey"></param>
        /// <param name="handler"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string listenKey, Action<DataEvent<MexcUserOrderUpdate>> handler, CancellationToken ct = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listenKey"></param>
        /// <param name="handler"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(string listenKey, Action<DataEvent<MexcUserTradeUpdate>> handler, CancellationToken ct = default);
    }
}
