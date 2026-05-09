// 02-futures.cs
//
// Demonstrates: MEXC futures - symbol format, set leverage, place market order,
// retrieve open position, close position.
//
// Setup: dotnet add package JK.Mexc.Net
// Substitute API_KEY / API_SECRET. The API key must have Futures trading enabled.

using Mexc.Net;
using Mexc.Net.Clients;
using Mexc.Net.Enums;

var client = new MexcRestClient(options =>
{
    options.ApiCredentials = new MexcCredentials("API_KEY", "API_SECRET");
});

// MEXC spot symbols use ETHUSDT; futures symbols use ETH_USDT.
const string symbol = "ETH_USDT";

// ---- 1. SET LEVERAGE ----
// When there is no open position, MEXC requires symbol, margin type, and position side.
var leverage = await client.FuturesApi.Account.SetLeverageAsync(
    leverage: 5,
    marginType: MarginType.Isolated,
    symbol: symbol,
    positionSide: PositionSide.Long);

if (!leverage.Success)
{
    Console.WriteLine($"Failed to set leverage: {leverage.Error}");
    return;
}

Console.WriteLine($"Leverage request accepted for {symbol}");

// ---- 2. PLACE MARKET ORDER (open long position) ----
// FuturesOrderSide encodes both direction and open/close action.
// Quantity is contract volume for MEXC futures.
var openOrder = await client.FuturesApi.Trading.PlaceOrderAsync(
    symbol: symbol,
    side: FuturesOrderSide.OpenLong,
    type: FuturesOrderType.Market,
    quantity: 1m,
    leverage: 5,
    marginType: MarginType.Isolated);

if (!openOrder.Success)
{
    Console.WriteLine($"Failed to open position: {openOrder.Error}");
    return;
}

Console.WriteLine($"Opened position via order {openOrder.Data.OrderId}");

// ---- 3. GET CURRENT POSITION ----
var positions = await client.FuturesApi.Trading.GetPositionsAsync(symbol);
if (!positions.Success)
{
    Console.WriteLine($"Failed to get positions: {positions.Error}");
    return;
}

var position = positions.Data.FirstOrDefault(p => p.PositionSize != 0);
if (position == null)
{
    Console.WriteLine("No open position found; the order may not have filled yet.");
    return;
}

Console.WriteLine($"Position: {position.PositionSize} contracts {position.Symbol}, side {position.PositionSide}");
Console.WriteLine($"Average open price: {position.OpenAveragePrice}");
Console.WriteLine($"Liquidation price: {position.LiquidationPrice}");

// ---- 4. CLOSE THE POSITION ----
// CloseLong closes a long; CloseShort closes a short. reduceOnly helps prevent a flip.
var closeOrder = await client.FuturesApi.Trading.PlaceOrderAsync(
    symbol: symbol,
    side: FuturesOrderSide.CloseLong,
    type: FuturesOrderType.Market,
    quantity: Math.Abs(position.PositionSize),
    leverage: position.Leverage,
    marginType: position.MarginType,
    reduceOnly: true);

if (closeOrder.Success)
{
    Console.WriteLine($"Closed position via order {closeOrder.Data.OrderId}");
}

// Common variations:
//   Limit order:      type: FuturesOrderType.Limit, add price
//   Open short:       side: FuturesOrderSide.OpenShort
//   Close short:      side: FuturesOrderSide.CloseShort
//   Cross margin:     marginType: MarginType.Cross
//   Position mode:    client.FuturesApi.Account.SetPositionModeAsync(...)
//   Trigger order:    client.FuturesApi.Trading.PlacePlanOrderAsync(...)
