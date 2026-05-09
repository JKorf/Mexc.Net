// 04-multi-exchange.cs
//
// Demonstrates: writing exchange-agnostic code using CryptoExchange.Net.SharedApis.
// Same code works against MEXC, Binance, OKX, Bybit, Kraken, and other exchanges
// from the CryptoExchange.Net family.
//
// Setup:
//   dotnet add package JK.Mexc.Net
//   dotnet add package Binance.Net  // optional, for comparison
//   dotnet add package JK.OKX.Net    // optional, for comparison

using CryptoExchange.Net.SharedApis;
using Mexc.Net.Clients;

// ---- THE PATTERN ----
// Each exchange client exposes a `.SharedClient` property on its API surfaces.
// SharedClient implements interfaces like ISpotTickerRestClient, ISpotOrderRestClient,
// IBalanceRestClient, IFuturesOrderRestClient, and others.

ISpotTickerRestClient mexcShared = new MexcRestClient().SpotApi.SharedClient;

// Common symbol type. SharedSymbol handles exchange formatting differences.
// MEXC spot uses "BTCUSDT"; futures uses "BTC_USDT"; other exchanges differ.
var btcusdt = new SharedSymbol(TradingMode.Spot, "BTC", "USDT");

await PrintTicker(mexcShared, btcusdt);

// ---- AGNOSTIC METHOD - works against any exchange ----
async Task PrintTicker(ISpotTickerRestClient client, SharedSymbol symbol)
{
    var result = await client.GetSpotTickerAsync(new GetTickerRequest(symbol));
    if (!result.Success)
    {
        Console.WriteLine($"[{client.Exchange}] Failed: {result.Error}");
        return;
    }

    Console.WriteLine($"[{client.Exchange}] {result.Data.Symbol}: {result.Data.LastPrice}");
}

// ---- WHY THIS MATTERS ----
// You can build:
//   - Multi-exchange arbitrage scanners
//   - Best-execution routers
//   - Unified portfolio dashboards
//   - Exchange comparison tools
// without writing per-exchange branches everywhere.

// ---- AVAILABLE SHARED INTERFACES ----
// REST:
//   ISpotTickerRestClient, ISpotSymbolRestClient, ISpotOrderRestClient
//   IFuturesOrderRestClient, IFuturesSymbolRestClient, IFuturesTriggerOrderRestClient
//   IBalanceRestClient, IPositionRestClient, IFeeRestClient
//   IOrderBookRestClient, IRecentTradeRestClient, IKlineRestClient
//   IDepositRestClient, IWithdrawalRestClient, ITransferRestClient
// WebSocket:
//   ITickerSocketClient, IBookTickerSocketClient
//   IOrderBookSocketClient, ITradeSocketClient, IKlineSocketClient
//   IUserTradeSocketClient, IBalanceSocketClient, ISpotOrderSocketClient,
//   IFuturesOrderSocketClient

// ---- WEBSOCKET EXAMPLE - SHARED SUBSCRIPTION ----
var mexcSocket = new MexcSocketClient();
ITickerSocketClient mexcTickerSocket = mexcSocket.SpotApi.SharedClient;

var sub = await mexcTickerSocket.SubscribeToTickerUpdatesAsync(
    new SubscribeTickerRequest(btcusdt),
    update => Console.WriteLine($"[{mexcTickerSocket.Exchange}] {update.Data.Symbol}: {update.Data.LastPrice}"));

if (!sub.Success)
{
    Console.WriteLine($"Subscribe failed: {sub.Error}");
    return;
}

Console.WriteLine("Press Enter to exit");
Console.ReadLine();

await mexcSocket.UnsubscribeAsync(sub.Data);

// Common variations:
//   Futures shared REST:     new MexcRestClient().FuturesApi.SharedClient
//   Futures shared socket:   new MexcSocketClient().FuturesApi.SharedClient
//   Cross-exchange books:    IOrderBookSocketClient on each exchange
//   Best execution:          ISpotOrderRestClient or IFuturesOrderRestClient on N exchanges
