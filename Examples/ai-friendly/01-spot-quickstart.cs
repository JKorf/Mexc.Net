// 01-spot-quickstart.cs
//
// Demonstrates: client setup, public market data, authenticated balance,
// limit order placement, order status check, and cancellation.
//
// Setup:
//   dotnet new console -n SpotQuickstart && cd SpotQuickstart
//   dotnet add package JK.Mexc.Net
//   Copy this file content into Program.cs
//   Substitute API_KEY / API_SECRET below
//   dotnet run

using Mexc.Net;
using Mexc.Net.Clients;
using Mexc.Net.Enums;

// ---- 1. PUBLIC CLIENT (no credentials needed for market data) ----
// Reuse this client across the application; do not create per-request.
var publicClient = new MexcRestClient();

var ticker = await publicClient.SpotApi.ExchangeData.GetTickerAsync("BTCUSDT");
if (!ticker.Success)
{
    // .Error contains Code, Message, ErrorType, and IsTransient.
    Console.WriteLine($"Failed to get ticker: {ticker.Error}");
    return;
}

Console.WriteLine($"BTC/USDT last price: {ticker.Data.LastPrice}");
Console.WriteLine($"24h volume: {ticker.Data.Volume} BTC");

// ---- 2. AUTHENTICATED CLIENT (for account / trading) ----
var tradingClient = new MexcRestClient(options =>
{
    options.ApiCredentials = new MexcCredentials("API_KEY", "API_SECRET");
});

var account = await tradingClient.SpotApi.Account.GetAccountInfoAsync();
if (!account.Success)
{
    Console.WriteLine($"Failed to get account: {account.Error}");
    return;
}

foreach (var balance in account.Data.Balances.Where(b => b.Total > 0))
{
    Console.WriteLine($"{balance.Asset}: {balance.Available} available, {balance.Locked} locked");
}

// ---- 3. PLACE A LIMIT BUY ORDER ----
// Limit buy 0.001 BTC at a price 5% below current. This is likely not filled immediately.
// Let Mexc.Net omit/generate the clientOrderId unless you need external correlation.
var safePrice = Math.Round(ticker.Data.LastPrice * 0.95m, 2);

var order = await tradingClient.SpotApi.Trading.PlaceOrderAsync(
    symbol: "BTCUSDT",
    side: OrderSide.Buy,
    type: OrderType.Limit,
    quantity: 0.001m,
    price: safePrice);

if (!order.Success)
{
    Console.WriteLine($"Failed to place order: {order.Error}");
    return;
}

Console.WriteLine($"Placed order {order.Data.OrderId} at {safePrice}, status: {order.Data.Status}");

// ---- 4. CHECK ORDER STATUS ----
var status = await tradingClient.SpotApi.Trading.GetOrderAsync("BTCUSDT", orderId: order.Data.OrderId);
if (status.Success)
{
    Console.WriteLine($"Order status: {status.Data.Status}, filled: {status.Data.QuantityFilled}");
}

// ---- 5. CANCEL THE ORDER (cleanup for this example) ----
var cancel = await tradingClient.SpotApi.Trading.CancelOrderAsync("BTCUSDT", orderId: order.Data.OrderId);
if (cancel.Success)
{
    Console.WriteLine($"Cancelled order {order.Data.OrderId}");
}

// Common variations:
//   Market order: type: OrderType.Market, omit price
//   Quote amount: use quoteQuantity instead of quantity for market buys
//   Test order:   tradingClient.SpotApi.Trading.PlaceTestOrderAsync(...)
//   Open orders:  tradingClient.SpotApi.Trading.GetOpenOrdersAsync("BTCUSDT")
