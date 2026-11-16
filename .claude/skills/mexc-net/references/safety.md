# MEXC operational considerations

## Credentials and permissions

Use `MexcCredentials(key, secret)` for HMAC; RSA XML and PEM forms must match the key configured at the exchange. PEM credential helpers are unavailable on netstandard2.0. Keep private material in secret storage. Trading permissions and API-supported market availability are separate from public symbol visibility. Withdrawal and subaccount administration require their own permissions.

## Orders and cancellation

Spot `PlaceTestOrderAsync` validates an order without creating a live order; it does not reserve funds or guarantee future acceptance. Spot market orders distinguish base quantity from quote quantity. A limit order can fill immediately, and cancellation can race with fills. Inspect executed quantity and user trades to determine remaining exposure. Preserve client order IDs where correlation is needed.

Batch placement/cancellation can contain individual failures despite outer success. Inspect every item before deciding what remains open or retrying any subset. Futures ordinary, plan, TP/SL, and trailing orders have distinct query/cancel workflows; cancel-all scope must match the intended order category.

## Futures modes and exposure

`FuturesOrderSide` encodes both direction and open/close intent. Read positions and current position mode before choosing `CloseLong` or `CloseShort`; account for position ID, margin type, and mode-dependent `reduceOnly` semantics. Futures quantity is contract volume; use contract size/volume metadata and avoid truncating decimal size or confusing contracts with base assets.

Leverage, position mode, margin, and auto-add-margin changes affect live account state. Without a position, leverage changes require the appropriate symbol, margin type, and position side. `ReversePositionAsync` and `CloseAllPositionsAsync` affect broad exposure and should only be used for the requested account action. Submission success does not prove a position opened or closed.

## Funds movement and administration

Withdrawals require the intended asset, network, address, memo/tag, amount, and client identifier. Transfers/universal transfers distinguish account types and master/subaccount identifiers. Dust conversion changes asset balances; MX deduction changes fee behavior. Subaccount/key creation or deletion changes account access. Read histories/status and verify the requested scope rather than treating these as ordinary market-data operations.

## Private streams

Credential-based Spot subscriptions acquire and renew listen keys internally. Do not add a second refresh loop for these overloads. For explicit keys, own start/keepalive/stop and any renewal event handling; avoid stopping a key still used by another subscription. Futures user-data streams authenticate directly and do not reuse Spot listen keys.

## Uncertain submissions and retries

A timeout or connection loss can leave an order, withdrawal, transfer, cancellation, or mode change accepted but unconfirmed. `Error.IsTransient` alone does not justify repeating it. Reconcile order/client IDs, trades, balances, transfer/withdrawal history, or current mode before deciding whether to resubmit; allow for propagation delays and partial batches. Bounded backoff is appropriate for transient public reads; keep generic read-retry helpers away from account changes.
