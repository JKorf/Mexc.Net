---
name: mexc-net
description: Generate and troubleshoot C#/.NET code consuming Mexc.Net for MEXC Spot/Futures data, accounts, orders, subaccounts, transfers, WebSocket streams, and Shared API V2 integrations.
---

# Mexc.Net

Install the consumer package (its ID differs from the namespace):

```sh
dotnet add package JK.Mexc.Net
```

Use `MexcRestClient.SpotApi.{ExchangeData|Account|Trading|SubAccount}` or `FuturesApi.{ExchangeData|Account|Trading}`. `MexcSocketClient` exposes subscriptions directly on `SpotApi` and `FuturesApi`. Reuse and dispose clients, await calls, and retain successful subscriptions for unsubscribe cleanup.

HMAC credentials are `MexcCredentials(key, secret)`; no passphrase is required. RSA XML and compatible-target PEM credential forms also exist. Match signing type and permissions to the API key. Public market data needs no credentials. Private Spot streams offer credential-based overloads that acquire/renew listen keys, or explicit-key overloads for application-managed keys.

Spot symbols use `BTCUSDT`; Futures use `ETH_USDT`. Use metadata or `MexcExchange.FormatSymbol` for reusable formatting. Futures quantity is contract volume and `FuturesOrderSide` combines direction and open/close intent (`OpenLong`, `OpenShort`, `CloseLong`, `CloseShort`). Respect account position/margin mode.

Check `Success` before `Data`: REST returns `HttpResult`, subscriptions return `WebSocketResult<UpdateSubscription>`, and some Shared helpers return `ExchangeCallResult`, with generic variants where applicable. Inspect nested per-item batch outcomes.

Use `.SharedApi` and narrow V2 capabilities for portable operations. `IMexcSharedApiClient` groups `SpotRest`, `FuturesRest`, `SpotSocket`, and `FuturesSocket`. Native methods cover subaccounts, listen keys, detailed futures modes and conditional-order workflows. Shared Futures orders use contract quantities.

## Read when needed

- [API surfaces](references/api-surfaces.md): endpoint routing, Shared capabilities, catalogs, quantities, and stream prerequisites.
- [Usage](references/usage.md): setup, authentication, account reads, test orders, subscriptions, Shared API V2, and DI.
- [Operational considerations](references/safety.md): execution, position modes, funds movement, listen keys, and uncertain submissions.

For work on the library itself, use [repository instructions](../../../AGENTS.md) (checkout-only).
