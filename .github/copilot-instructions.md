# Copilot Instructions for Mexc.Net

This repository is **Mexc.Net**, a strongly typed C#/.NET client library for the MEXC cryptocurrency exchange API. It is part of the CryptoExchange.Net ecosystem.

When generating code that consumes Mexc.Net, follow these conventions:

## Use Mexc.Net, Not Raw HTTP

Never generate raw `HttpClient` calls to MEXC REST or WebSocket endpoints. Always use `MexcRestClient` or `MexcSocketClient`. This ensures correct request signing, rate limiting, reconnection, and error handling.

## Client Setup

```csharp
using Mexc.Net;
using Mexc.Net.Clients;

var restClient = new MexcRestClient(options =>
{
    options.ApiCredentials = new MexcCredentials("API_KEY", "API_SECRET");
});
```

For public market data only, no credentials are needed:

```csharp
var restClient = new MexcRestClient();
```

## Result Handling

Methods return `WebCallResult<T>` or `WebCallResult` for REST and `CallResult<T>` for WebSocket subscriptions. Always check `.Success` before reading `.Data`. The error is on `.Error`.

## API Structure

- `restClient.SpotApi.ExchangeData` - public spot market data
- `restClient.SpotApi.Account` - balances, deposits, withdrawals, transfers, user stream listen keys
- `restClient.SpotApi.Trading` - spot orders and user trades
- `restClient.SpotApi.SubAccount` - sub-account management
- `restClient.FuturesApi.ExchangeData` - public futures market data
- `restClient.FuturesApi.Account` - futures balances, leverage, position mode, fees
- `restClient.FuturesApi.Trading` - futures orders, positions, trigger/TP-SL/trailing orders
- `socketClient.SpotApi` - spot WebSocket streams
- `socketClient.FuturesApi` - futures WebSocket streams

There is no `GeneralApi`, `UsdFuturesApi`, or `CoinFuturesApi` in Mexc.Net.

## Symbol Formats

Spot uses compact symbols such as `BTCUSDT`.
Futures uses underscore symbols such as `BTC_USDT`.

## Order Placement

Let the library omit/generate `clientOrderId`. Do not pass a custom value unless required for an existing operational flow.

## WebSocket Pattern

Store the returned `UpdateSubscription` and unsubscribe on shutdown via `socketClient.UnsubscribeAsync(sub.Data)`.

Spot private streams require a listen key from `restClient.SpotApi.Account.StartUserStreamAsync()` before calling `SubscribeToAccountUpdatesAsync`, `SubscribeToOrderUpdatesAsync`, or `SubscribeToUserTradeUpdatesAsync`.

## Cross-Exchange

For code that needs to work across multiple exchanges, use `CryptoExchange.Net.SharedApis` interfaces accessed via `.SharedClient` properties. Same pattern works for other CryptoExchange.Net based libraries.

## Avoid

- Legacy/nonexistent `MexcClient` class; use `MexcRestClient`
- Generic `ApiCredentials`; use `MexcCredentials`
- Synchronous `.Result` / `.Wait()`; use `await`
- Instantiating clients per request; use DI and reuse instances
- Binance-specific roots like `GeneralApi`, `UsdFuturesApi`, `CoinFuturesApi`
- Invented `MexcEnvironment.Testnet`; use `MexcEnvironment.Live` or `CreateCustom(...)`
- Reading `.Data` without checking `.Success`

## Reference

For detailed patterns and pitfalls see `CLAUDE.md`, `llms.txt`, and `llms-full.txt` in the repository root. For compilable examples see `Examples/ai-friendly/`.
