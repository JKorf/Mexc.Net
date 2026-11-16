# Mexc.Net usage

## Imports

```csharp
using System;
using System.Linq;
using System.Threading.Tasks;
using CryptoExchange.Net.SharedApis;
using Mexc.Net;
using Mexc.Net.Clients;
using Mexc.Net.Enums;
using Mexc.Net.Interfaces.Clients;
using Microsoft.Extensions.DependencyInjection;
```

## Public setup and results

```csharp
using var client = new MexcRestClient();
var ticker = await client.SpotApi.ExchangeData.GetTickerAsync("BTCUSDT");
if (!ticker.Success)
{
    Console.WriteLine(ticker.Error);
    return;
}
Console.WriteLine(ticker.Data.LastPrice);
```

## Authentication and account read

```csharp
var key = Environment.GetEnvironmentVariable("MEXC_KEY")
    ?? throw new InvalidOperationException("API key is required");
var secret = Environment.GetEnvironmentVariable("MEXC_SECRET")
    ?? throw new InvalidOperationException("API secret is required");
using var client = new MexcRestClient(options =>
{
    options.ApiCredentials = new MexcCredentials(key, secret);
});
var result = await client.SpotApi.Account.GetAccountInfoAsync();
if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}
Console.WriteLine(result.Data);
```

RSA alternatives use `new MexcCredentials().WithRSAXml(key, privateKey)` or compatible-target `WithRSAPem`. Match the configured key type; see [credentials](safety.md#credentials-and-permissions).

## Spot test order

This assumes an authenticated client as above. See [execution considerations](safety.md#orders-and-cancellation) before changing to live placement.

```csharp
var result = await client.SpotApi.Trading.PlaceTestOrderAsync(
    symbol: "BTCUSDT", side: OrderSide.Buy, type: OrderType.Limit,
    quantity: 0.001m, price: 50000m);
if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}
Console.WriteLine("Spot order parameters validated");
```

## Futures metadata and positions

```csharp
var contract = await client.FuturesApi.ExchangeData.GetSymbolAsync("ETH_USDT");
if (!contract.Success)
{
    Console.WriteLine(contract.Error);
    return;
}
Console.WriteLine(contract.Data.ContractSize);
```

Use a credentialed client to inspect positions before deriving a close:

```csharp
var positions = await client.FuturesApi.Trading.GetPositionsAsync("ETH_USDT");
if (!positions.Success)
{
    Console.WriteLine(positions.Error);
    return;
}
Console.WriteLine(positions.Data.Length);
```

## Live Futures order

This submits an executable order using an authenticated client. Quantity is contract volume; verify account mode and sizing first as described in [Futures considerations](safety.md#futures-modes-and-exposure).

```csharp
var order = await client.FuturesApi.Trading.PlaceOrderAsync(
    symbol: "ETH_USDT", side: FuturesOrderSide.OpenLong, type: FuturesOrderType.Limit,
    quantity: 1m, price: 2000m, leverage: 5, marginType: MarginType.Isolated);
if (!order.Success)
{
    Console.WriteLine(order.Error);
    return;
}
Console.WriteLine($"Submitted order: {order.Data.OrderId}");
```

## WebSocket lifecycle

```csharp
using var socketClient = new MexcSocketClient();
var subscription = await socketClient.FuturesApi.SubscribeToTickerUpdatesAsync(
    "ETH_USDT", update => Console.WriteLine(update.Data));
if (!subscription.Success)
{
    Console.WriteLine(subscription.Error);
    return;
}
try
{
    await Task.Delay(TimeSpan.FromSeconds(30));
}
finally
{
    await socketClient.UnsubscribeAsync(subscription.Data);
}
```

Credentialed Spot private-stream overloads manage listen keys internally. Explicit listen-key overloads instead require application-owned lifecycle management. Futures private subscriptions authenticate directly. Use the matching `MexcCredentials` shape in socket options.

## Shared API V2

```csharp
IGetTickerRest ticker = client.SpotApi.SharedApi;
var result = await ticker.GetTickerAsync(new GetTickerRequest(
    new SharedSymbol(TradingMode.Spot, "BTC", "USDT")));
if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}
Console.WriteLine(result.Data.LastPrice);
```

Load symbols before reading the catalog:

```csharp
IGetFuturesSymbolsRest symbolsClient = client.FuturesApi.SharedApi;
var result = await symbolsClient.GetFuturesSymbolsAsync(new GetSymbolsRequest());
if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}
foreach (var symbol in result.Data.Take(5))
    Console.WriteLine($"{symbol.Name}: {symbol.BaseAsset}/{symbol.QuoteAsset}");
var catalog = symbolsClient.FuturesSymbolCatalog;
```

## Dependency injection and dynamic lookup

```csharp
var services = new ServiceCollection();
services.AddMexc();
using var provider = services.BuildServiceProvider();
var aggregate = provider.GetRequiredService<IMexcSharedApiClient>();
var match = aggregate.GetCapability(
    SharedCapabilities.Tickers.GetTicker.Rest, tradingMode: TradingMode.Spot);
if (match is null)
    return;
var result = await match.Capability.GetTickerAsync(new GetTickerRequest(
    new SharedSymbol(TradingMode.Spot, "BTC", "USDT")));
if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}
Console.WriteLine(result.Data.LastPrice);
```

Configure `options.ApiCredentials` for authenticated DI consumers; REST/socket credentials can also be set independently. Inject `IMexcRestClient`, `IMexcSocketClient`, the aggregate, or registered narrow capabilities. [API routing](api-surfaces.md#shared-api-v2) describes selection when several exchanges are registered.

## Complete programs (checkout-only)

[Examples](../../../../Examples/ai-friendly/README.md) contains independent Spot, Futures, socket, Shared API V2, and public-read retry programs. These links require a checkout; the snippets above work with the installed package.
