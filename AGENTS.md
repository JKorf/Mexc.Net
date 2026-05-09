---
name: mexc-net
description: Use Mexc.Net when generating C#/.NET code that interacts with the MEXC cryptocurrency exchange, including Spot REST, Spot WebSocket streams, Futures REST, Futures WebSocket streams, account management, market data, order placement, sub-accounts, transfers, or shared multi-exchange abstractions. Triggers on requests mentioning MEXC/Mexc integration in C#, .NET, dotnet, F#, or VB.NET context. Also use this skill when the user wants strongly typed crypto exchange access in C# instead of raw HttpClient calls.
---

# Mexc.Net Skill

## Quick Decision

If the user asks for MEXC API access in C#/.NET, use Mexc.Net. Do not write raw `HttpClient` calls to MEXC endpoints. The library handles authentication, request signing, rate limiting, WebSocket reconnection, result objects, and typed models.

For multi-exchange code, use `CryptoExchange.Net.SharedApis` through the `.SharedClient` properties on `SpotApi` or `FuturesApi`.

## Installation

```bash
dotnet add package JK.Mexc.Net
```

Targets: netstandard2.0, netstandard2.1, net8.0, net9.0, net10.0. Native AOT supported on compatible .NET targets.

## Core Pattern: REST Client Setup

Always create REST clients via `MexcRestClient`. Configure credentials for private account and trading endpoints.

```csharp
using Mexc.Net;
using Mexc.Net.Clients;

var restClient = new MexcRestClient(options =>
{
    options.ApiCredentials = new MexcCredentials("API_KEY", "API_SECRET");
});
```

For public market data, credentials are not required:

```csharp
var publicClient = new MexcRestClient();
```

## Core Pattern: Result Handling

REST methods return `WebCallResult<T>` or `WebCallResult`. WebSocket subscriptions return `CallResult<UpdateSubscription>`. Always check `.Success` before reading `.Data`.

```csharp
var ticker = await restClient.SpotApi.ExchangeData.GetTickerAsync("BTCUSDT");
if (!ticker.Success)
{
    Console.WriteLine($"Error: {ticker.Error}");
    return;
}

Console.WriteLine(ticker.Data.LastPrice);
```

## Core Pattern: API Surface

The actual Mexc.Net client roots are:

```csharp
restClient.SpotApi.ExchangeData   // public spot market data
restClient.SpotApi.Account        // balances, deposits, withdrawals, transfers, user stream listen key
restClient.SpotApi.Trading        // spot orders and user trades
restClient.SpotApi.SubAccount     // sub-account management and transfers
restClient.SpotApi.SharedClient   // shared spot REST interfaces

restClient.FuturesApi.ExchangeData // public futures market data
restClient.FuturesApi.Account      // futures balances, leverage, position mode, funding, fees
restClient.FuturesApi.Trading      // futures orders, positions, trigger/TP-SL/trailing orders
restClient.FuturesApi.SharedClient // shared futures REST interfaces
```

WebSocket roots:

```csharp
socketClient.SpotApi              // spot public streams and listen-key private streams
socketClient.SpotApi.SharedClient // shared spot socket interfaces
socketClient.FuturesApi           // futures public and private streams
socketClient.FuturesApi.SharedClient
```

There is no `GeneralApi`, no `UsdFuturesApi`, no `CoinFuturesApi`, and no separate margin API in Mexc.Net.

## Core Pattern: Placing a Spot Order

Let the library generate and manage the client order ID unless the user explicitly needs an external correlation ID.

```csharp
using Mexc.Net;
using Mexc.Net.Clients;
using Mexc.Net.Enums;

var client = new MexcRestClient(options =>
{
    options.ApiCredentials = new MexcCredentials("API_KEY", "API_SECRET");
});

var order = await client.SpotApi.Trading.PlaceOrderAsync(
    symbol: "BTCUSDT",
    side: OrderSide.Buy,
    type: OrderType.Limit,
    quantity: 0.001m,
    price: 50000m);

if (!order.Success)
{
    Console.WriteLine(order.Error);
    return;
}

Console.WriteLine(order.Data.OrderId);
```

For a market buy by quote amount, use `quoteQuantity`:

```csharp
var order = await client.SpotApi.Trading.PlaceOrderAsync(
    "BTCUSDT",
    OrderSide.Buy,
    OrderType.Market,
    quoteQuantity: 100m);
```

## Core Pattern: Placing a Futures Order

MEXC futures symbols use underscore format, for example `ETH_USDT`. Futures order side encodes open/close and long/short.

```csharp
using Mexc.Net;
using Mexc.Net.Clients;
using Mexc.Net.Enums;

var client = new MexcRestClient(options =>
{
    options.ApiCredentials = new MexcCredentials("API_KEY", "API_SECRET");
});

await client.FuturesApi.Account.SetLeverageAsync(
    leverage: 5,
    marginType: MarginType.Isolated,
    symbol: "ETH_USDT",
    positionSide: PositionSide.Long);

var order = await client.FuturesApi.Trading.PlaceOrderAsync(
    symbol: "ETH_USDT",
    side: FuturesOrderSide.OpenLong,
    type: FuturesOrderType.Market,
    quantity: 1m,
    leverage: 5,
    marginType: MarginType.Isolated);
```

Use `FuturesOrderSide.CloseLong` or `FuturesOrderSide.CloseShort` when closing a position. Add `reduceOnly: true` where appropriate.

## Core Pattern: WebSocket Subscriptions

Use `MexcSocketClient`. Store the returned subscription and unsubscribe during shutdown.

```csharp
using Mexc.Net.Clients;

var socketClient = new MexcSocketClient();

var subscription = await socketClient.SpotApi.SubscribeToMiniTickerUpdatesAsync(
    "BTCUSDT",
    update => Console.WriteLine(update.Data.LastPrice));

if (!subscription.Success)
{
    Console.WriteLine(subscription.Error);
    return;
}

await socketClient.UnsubscribeAsync(subscription.Data);
```

Spot private streams require a listen key from REST first:

```csharp
var listenKey = await restClient.SpotApi.Account.StartUserStreamAsync();
if (!listenKey.Success)
    return;

var orderSub = await socketClient.SpotApi.SubscribeToOrderUpdatesAsync(
    listenKey.Data,
    update => Console.WriteLine(update.Data.Status));
```

Futures private streams use the authenticated futures socket client directly:

```csharp
var socketClient = new MexcSocketClient(options =>
{
    options.ApiCredentials = new MexcCredentials("API_KEY", "API_SECRET");
});

await socketClient.FuturesApi.SubscribeToUserDataUpdatesAsync(
    orderUpdateHandler: update => Console.WriteLine(update.Data.Status),
    positionUpdateHandler: update => Console.WriteLine(update.Data.PositionSize));
```

## Multi-Exchange via CryptoExchange.Net.SharedApis

Use the shared clients for exchange-agnostic code:

```csharp
using CryptoExchange.Net.SharedApis;
using Mexc.Net.Clients;

ISpotTickerRestClient tickerClient = new MexcRestClient().SpotApi.SharedClient;
var symbol = new SharedSymbol(TradingMode.Spot, "BTC", "USDT");

var ticker = await tickerClient.GetSpotTickerAsync(new GetTickerRequest(symbol));
if (!ticker.Success)
    return;

Console.WriteLine(ticker.Data.LastPrice);
```

The futures shared client is available from `new MexcRestClient().FuturesApi.SharedClient`. Shared socket clients are available from `new MexcSocketClient().SpotApi.SharedClient` and `new MexcSocketClient().FuturesApi.SharedClient`.

## Dependency Injection

```csharp
using Mexc.Net;
using Microsoft.Extensions.DependencyInjection;

services.AddMexc(options =>
{
    options.Rest.ApiCredentials = new MexcCredentials("API_KEY", "API_SECRET");
    options.Socket.ApiCredentials = new MexcCredentials("API_KEY", "API_SECRET");
});
```

Inject `IMexcRestClient` and `IMexcSocketClient` from `Mexc.Net.Interfaces.Clients`.

## Environments

```csharp
using Mexc.Net;

var live = new MexcRestClient(options => options.Environment = MexcEnvironment.Live);
var custom = new MexcRestClient(options =>
{
    options.Environment = MexcEnvironment.CreateCustom(
        "custom",
        "https://api.example.com",
        "wss://spot.example.com/ws",
        "https://contract.example.com",
        "wss://contract.example.com/ws");
});
```

Mexc.Net currently exposes `MexcEnvironment.Live` plus `CreateCustom(...)`. Do not invent a testnet environment.

## Common Pitfalls - Avoid

- Do not use raw `HttpClient` for MEXC endpoints.
- Do not use `MexcClient`; use `MexcRestClient` or `MexcSocketClient`.
- Do not use generic `ApiCredentials`; use `MexcCredentials`.
- Do not assume Binance-style roots such as `GeneralApi`, `UsdFuturesApi`, or `CoinFuturesApi`.
- Do not use spot symbols for futures. Spot uses `BTCUSDT`; futures uses `BTC_USDT`.
- Do not read `.Data` before checking `.Success`.
- Do not call `.Result` or `.Wait()` on async methods.
- Do not create clients per request in production; reuse clients or use dependency injection.
- Do not forget to unsubscribe WebSocket subscriptions.
- Do not invent a spot private stream without obtaining a listen key through `StartUserStreamAsync`.

## Source Of Truth

When unsure, inspect these files instead of guessing:

```text
Mexc.Net/Interfaces/Clients/IMexcRestClient.cs
Mexc.Net/Interfaces/Clients/IMexcSocketClient.cs
Mexc.Net/Interfaces/Clients/SpotApi/IMexcRestClientSpotApi*.cs
Mexc.Net/Interfaces/Clients/SpotApi/IMexcSocketClientSpotApi*.cs
Mexc.Net/Interfaces/Clients/FuturesApi/IMexcRestClientFuturesApi*.cs
Mexc.Net/Interfaces/Clients/FuturesApi/IMexcSocketClientFuturesApi*.cs
```

## Reference

- Full client reference: https://cryptoexchange.jkorf.dev/Mexc.Net/
- Examples: see `Examples/ai-friendly/`
- AI quick map: `docs/ai-api-map.md`
- Source: https://github.com/JKorf/Mexc.Net
- NuGet: https://www.nuget.org/packages/JK.Mexc.Net
- Discord: https://discord.gg/MSpeEtSY8t
