// 03-websocket.cs
//
// Demonstrates: WebSocket subscriptions - public spot ticker, spot klines,
// spot authenticated listen-key streams, and futures authenticated stream pattern.
// Includes proper teardown.
//
// Setup: dotnet add package JK.Mexc.Net

using Mexc.Net;
using Mexc.Net.Clients;
using Mexc.Net.Enums;

// ---- 1. PUBLIC SOCKET CLIENT - for market data streams ----
var publicSocket = new MexcSocketClient();

var tickerSub = await publicSocket.SpotApi.SubscribeToMiniTickerUpdatesAsync(
    "BTCUSDT",
    update =>
    {
        // Keep handlers fast; offload heavy work to a queue or channel.
        Console.WriteLine($"BTC: {update.Data.LastPrice} (24h vol {update.Data.Volume:F2})");
    });

if (!tickerSub.Success)
{
    Console.WriteLine($"Failed to subscribe ticker: {tickerSub.Error}");
    return;
}

var klineSub = await publicSocket.SpotApi.SubscribeToKlineUpdatesAsync(
    "ETHUSDT",
    KlineInterval.OneMinute,
    update =>
    {
        Console.WriteLine($"ETH 1m: O={update.Data.OpenPrice} H={update.Data.HighPrice} L={update.Data.LowPrice} C={update.Data.ClosePrice}");
    });

if (!klineSub.Success)
{
    Console.WriteLine($"Failed to subscribe klines: {klineSub.Error}");
    await publicSocket.UnsubscribeAsync(tickerSub.Data);
    return;
}

// ---- 2. AUTHENTICATED SPOT STREAMS - listen key required ----
var restClient = new MexcRestClient(options =>
{
    options.ApiCredentials = new MexcCredentials("API_KEY", "API_SECRET");
});

var listenKey = await restClient.SpotApi.Account.StartUserStreamAsync();
if (!listenKey.Success)
{
    Console.WriteLine($"Failed to start user stream: {listenKey.Error}");
    await publicSocket.UnsubscribeAsync(tickerSub.Data);
    await publicSocket.UnsubscribeAsync(klineSub.Data);
    return;
}

var authSocket = new MexcSocketClient(options =>
{
    options.ApiCredentials = new MexcCredentials("API_KEY", "API_SECRET");
});

var orderSub = await authSocket.SpotApi.SubscribeToOrderUpdatesAsync(
    listenKey.Data,
    update =>
    {
        Console.WriteLine($"Spot order {update.Data.OrderId}: {update.Data.Status}");
    });

if (!orderSub.Success)
{
    Console.WriteLine($"Failed to subscribe spot orders: {orderSub.Error}");
    await publicSocket.UnsubscribeAsync(tickerSub.Data);
    await publicSocket.UnsubscribeAsync(klineSub.Data);
    return;
}

// ---- 3. FUTURES AUTHENTICATED STREAM ----
// Futures private streams use the authenticated socket client directly.
var futuresSub = await authSocket.FuturesApi.SubscribeToUserDataUpdatesAsync(
    orderUpdateHandler: update => Console.WriteLine($"Futures order {update.Data.OrderId}: {update.Data.Status}"),
    positionUpdateHandler: update => Console.WriteLine($"Position {update.Data.Symbol}: {update.Data.PositionSize}"));

if (!futuresSub.Success)
{
    Console.WriteLine($"Failed to subscribe futures user data: {futuresSub.Error}");
    await publicSocket.UnsubscribeAsync(tickerSub.Data);
    await publicSocket.UnsubscribeAsync(klineSub.Data);
    await authSocket.UnsubscribeAsync(orderSub.Data);
    return;
}

Console.WriteLine("All subscriptions active. Press Enter to teardown...");
Console.ReadLine();

// ---- 4. TEARDOWN - IMPORTANT ----
await publicSocket.UnsubscribeAsync(tickerSub.Data);
await publicSocket.UnsubscribeAsync(klineSub.Data);
await authSocket.UnsubscribeAsync(orderSub.Data);
await authSocket.UnsubscribeAsync(futuresSub.Data);

await restClient.SpotApi.Account.StopUserStreamAsync(listenKey.Data);

Console.WriteLine("Clean shutdown complete.");

// Common variations:
//   Spot trades:        publicSocket.SpotApi.SubscribeToTradeUpdatesAsync(symbol, handler)
//   Spot order book:    publicSocket.SpotApi.SubscribeToOrderBookUpdatesAsync(symbol, handler)
//   Futures ticker:     publicSocket.FuturesApi.SubscribeToTickerUpdatesAsync("ETH_USDT", handler)
//   Futures funding:    publicSocket.FuturesApi.SubscribeToFundingRateUpdatesAsync("ETH_USDT", handler)
