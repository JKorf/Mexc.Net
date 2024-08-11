using Mexc.Net;
using Mexc.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis.Interfaces;
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
using CryptoExchange.Net.SharedApis.SubscribeModels;

namespace Mexc.Net.Clients.SpotApi
{
    internal partial class MexcSocketClientSpotApi : IMexcSocketClientSpotApiShared
    {
        public string Exchange => MexcExchange.ExchangeName;

        async Task<CallResult<UpdateSubscription>> ITickerSocketClient.SubscribeToTickerUpdatesAsync(TickerSubscribeRequest request, Action<DataEvent<SharedTicker>> handler, CancellationToken ct)
        {
            var symbol = FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType);
            var result = await SubscribeToMiniTickerUpdatesAsync(symbol, update => handler(update.As(new SharedTicker
            {
                Symbol = update.Data.Symbol,
                HighPrice = update.Data.HighPrice,
                LastPrice = update.Data.LastPrice,
                LowPrice = update.Data.LowPrice
            })), ct: ct).ConfigureAwait(false);

            return result;
        }
    }
}
