using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.OrderBook;
using Mexc.Net.Clients;
using Mexc.Net.Interfaces.Clients;
using Mexc.Net.Objects.Models.Futures;
using Mexc.Net.Objects.Options;

namespace Mexc.Net.SymbolOrderBooks
{
    /// <summary>
    /// Implementation for a synchronized order book. After calling Start the order book will sync itself and keep up to date with new data. It will automatically try to reconnect and resync in case of a lost/interrupted connection.
    /// Make sure to check the State property to see if the order book is synced.
    /// </summary>
    public class MexcFuturesSymbolOrderBook : SymbolOrderBook
    {
        private readonly bool _clientOwner;
        private readonly IMexcRestClient _restClient;
        private readonly IMexcSocketClient _socketClient;

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public MexcFuturesSymbolOrderBook(string symbol, Action<MexcOrderBookOptions>? optionsDelegate = null)
            : this(symbol, optionsDelegate, null, null, null)
        {
            _clientOwner = true;
        }

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        /// <param name="loggerFactory">Logger</param>
        /// <param name="restClient">Rest client instance</param>
        /// <param name="socketClient">Socket client instance</param>
        public MexcFuturesSymbolOrderBook(
            string symbol,
            Action<MexcOrderBookOptions>? optionsDelegate,
            ILoggerFactory? loggerFactory,
            IMexcRestClient? restClient,
            IMexcSocketClient? socketClient) : base(loggerFactory, "Mexc", "Futures", symbol)
        {
            var options = MexcOrderBookOptions.Default.Copy();
            if (optionsDelegate != null)
                optionsDelegate(options);
            Initialize(options);

            _strictLevels = false;
            _sequencesAreConsecutive = options?.Limit == null;
            _skipSequenceCheckFirstUpdateAfterSnapshotSet = true;

            Levels = options?.Limit;

            _clientOwner = socketClient == null;
            _socketClient = socketClient ?? new MexcSocketClient();
            _restClient = restClient ?? new MexcRestClient();
        }

        /// <inheritdoc />
        protected override async Task<CallResult<UpdateSubscription>> DoStartAsync(CancellationToken ct)
        {
            CallResult<UpdateSubscription> subResult;
            if (Levels == null)
                subResult = await _socketClient.FuturesApi.SubscribeToOrderBookUpdatesAsync(Symbol, HandleUpdate).ConfigureAwait(false);
            else
                subResult = await _socketClient.FuturesApi.SubscribeToPartialOrderBookUpdatesAsync(Symbol, Levels.Value, HandleUpdate).ConfigureAwait(false);

            if (!subResult)
                return new CallResult<UpdateSubscription>(subResult.Error!);

            if (ct.IsCancellationRequested)
            {
                await subResult.Data.CloseAsync().ConfigureAwait(false);
                return subResult.AsError<UpdateSubscription>(new CancellationRequestedError());
            }

            Status = OrderBookStatus.Syncing;
            if (Levels == null)
            {
                // Wait up to 1s until the first update has been received
                await WaitUntilFirstUpdateBufferedAsync(TimeSpan.FromMilliseconds(500), TimeSpan.FromMilliseconds(1000), ct).ConfigureAwait(false);
                var bookResult = await _restClient.FuturesApi.ExchangeData.GetOrderBookAsync(Symbol, Levels ?? 1000).ConfigureAwait(false);
                if (!bookResult)
                {
                    _logger.Log(LogLevel.Debug, $"{Api} order book {Symbol} failed to retrieve initial order book");
                    await _socketClient.UnsubscribeAsync(subResult.Data).ConfigureAwait(false);
                    return new CallResult<UpdateSubscription>(bookResult.Error!);
                }

                SetSnapshot(bookResult.Data.Version, bookResult.Data.Bids, bookResult.Data.Asks);
            }
            else
            {
                var setResult = await WaitForSetOrderBookAsync(TimeSpan.FromSeconds(5), ct).ConfigureAwait(false);
                if (!setResult)
                    await subResult.Data.CloseAsync().ConfigureAwait(false);

                return setResult ? subResult : new CallResult<UpdateSubscription>(setResult.Error!);
            }

            return new CallResult<UpdateSubscription>(subResult.Data);
        }

        private void HandleUpdate(DataEvent<MexcFuturesOrderBook> data)
        {
            if (Levels != null)
                SetSnapshot(data.Data.Version, data.Data.Bids, data.Data.Asks, data.DataTime, data.DataTimeLocal);
            else
                UpdateOrderBook(data.Data.SequenceStart!.Value, data.Data.SequenceEnd!.Value, data.Data.Bids, data.Data.Asks, data.DataTime, data.DataTimeLocal);
        }

        /// <inheritdoc />
        protected override void DoReset()
        {
        }

        /// <inheritdoc />
        protected override async Task<CallResult<bool>> DoResyncAsync(CancellationToken ct)
        {
            if (Levels != null)
                return await WaitForSetOrderBookAsync(TimeSpan.FromSeconds(5), ct).ConfigureAwait(false);

            // Wait up to 1s until the first update has been received
            await WaitUntilFirstUpdateBufferedAsync(TimeSpan.FromMilliseconds(500), TimeSpan.FromMilliseconds(1000), ct).ConfigureAwait(false);
            var bookResult = await _restClient.FuturesApi.ExchangeData.GetOrderBookAsync(Symbol, Levels ?? 1000).ConfigureAwait(false);
            if (!bookResult)
                return new CallResult<bool>(bookResult.Error!);

            SetSnapshot(bookResult.Data.Version, bookResult.Data.Bids, bookResult.Data.Asks);
            return new CallResult<bool>(true);
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            if (_clientOwner)
            {
                _restClient?.Dispose();
                _socketClient?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
