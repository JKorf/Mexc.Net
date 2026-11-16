# Mexc.Net API surfaces

## Client roots

`MexcRestClient` / `IMexcRestClient` expose `SpotApi` with `ExchangeData`, `Account`, `Trading`, and `SubAccount`, and `FuturesApi` with `ExchangeData`, `Account`, and `Trading`. `MexcSocketClient` / `IMexcSocketClient` exposes subscriptions directly on `SpotApi` and `FuturesApi`.

## Spot REST

| User intent | Mexc.Net member |
|---|---|
| Ping spot API | `client.SpotApi.ExchangeData.PingAsync()` |
| Get spot server time | `client.SpotApi.ExchangeData.GetServerTimeAsync()` |
| Get API-supported spot symbols | `client.SpotApi.ExchangeData.GetApiSymbolsAsync()` |
| Get spot exchange info | `client.SpotApi.ExchangeData.GetExchangeInfoAsync()` |
| Get info for one spot symbol | `client.SpotApi.ExchangeData.GetExchangeInfoAsync(new[] { "BTCUSDT" })` |
| Get latest spot ticker | `client.SpotApi.ExchangeData.GetTickerAsync("BTCUSDT")` |
| Get all spot tickers | `client.SpotApi.ExchangeData.GetTickersAsync()` |
| Get spot prices | `client.SpotApi.ExchangeData.GetPricesAsync()` |
| Get one spot price | `client.SpotApi.ExchangeData.GetPricesAsync(new[] { "BTCUSDT" })` |
| Get spot order book | `client.SpotApi.ExchangeData.GetOrderBookAsync("BTCUSDT")` |
| Get recent trades | `client.SpotApi.ExchangeData.GetRecentTradesAsync("BTCUSDT")` |
| Get aggregate trades | `client.SpotApi.ExchangeData.GetAggregatedTradeHistoryAsync("BTCUSDT")` |
| Get klines/candles | `client.SpotApi.ExchangeData.GetKlinesAsync("BTCUSDT", KlineInterval.OneMinute)` |
| Get average price | `client.SpotApi.ExchangeData.GetAveragePriceAsync("BTCUSDT")` |
| Get book ticker | `client.SpotApi.ExchangeData.GetBookPricesAsync("BTCUSDT")` |
| Get all book tickers | `client.SpotApi.ExchangeData.GetBookPricesAsync()` |
| Get offline/paused symbols | `client.SpotApi.ExchangeData.GetOfflineSymbolsAsync()` |
| Get account info and balances | `client.SpotApi.Account.GetAccountInfoAsync()` |
| Get KYC status | `client.SpotApi.Account.GetKycStatusAsync()` |
| Get user assets | `client.SpotApi.Account.GetUserAssetsAsync()` |
| Get deposit history | `client.SpotApi.Account.GetDepositHistoryAsync(...)` |
| Get withdrawal history | `client.SpotApi.Account.GetWithdrawHistoryAsync(...)` |
| Generate deposit address | `client.SpotApi.Account.GenerateDepositAddressAsync(asset, network)` |
| Get deposit addresses | `client.SpotApi.Account.GetDepositAddressesAsync(asset, network)` |
| Get withdrawal addresses | `client.SpotApi.Account.GetWithdrawAddressesAsync(...)` |
| Withdraw asset | `client.SpotApi.Account.WithdrawAsync(...)` |
| Cancel withdrawal | `client.SpotApi.Account.CancelWithdrawAsync(withdrawId)` |
| Transfer between account types | `client.SpotApi.Account.TransferAsync(asset, fromAccountType, toAccountType, quantity)` |
| Get transfer history | `client.SpotApi.Account.GetTransferHistoryAsync(...)` |
| Get one transfer | `client.SpotApi.Account.GetTransferAsync(transferId)` |
| Get assets eligible for dust conversion | `client.SpotApi.Account.GetAssetsForDustTransferAsync()` |
| Dust convert assets | `client.SpotApi.Account.DustTransferAsync(assets)` |
| Get dust log | `client.SpotApi.Account.GetDustLogAsync(...)` |
| Internal transfer | `client.SpotApi.Account.TransferInternalAsync(...)` |
| Get internal transfer history | `client.SpotApi.Account.GetInternalTransferHistoryAsync(...)` |
| Set MX deduction status | `client.SpotApi.Account.SetMxDeductionAsync(enabled)` |
| Get MX deduction status | `client.SpotApi.Account.GetMxDeductionStatusAsync()` |
| Get spot trade fee | `client.SpotApi.Account.GetTradeFeeAsync(symbol)` |
| Start spot user stream | `client.SpotApi.Account.StartUserStreamAsync()` |
| Keep alive spot user stream | `client.SpotApi.Account.KeepAliveUserStreamAsync(listenKey)` |
| Stop spot user stream | `client.SpotApi.Account.StopUserStreamAsync(listenKey)` |
| Place spot order | `client.SpotApi.Trading.PlaceOrderAsync(...)` |
| Place spot test order | `client.SpotApi.Trading.PlaceTestOrderAsync(...)` |
| Place multiple spot orders | `client.SpotApi.Trading.PlaceMultipleOrdersAsync(...)` |
| Query spot order | `client.SpotApi.Trading.GetOrderAsync(symbol, orderId: orderId)` |
| Query spot order by client id | `client.SpotApi.Trading.GetOrderAsync(symbol, clientOrderId: clientOrderId)` |
| Get open spot orders | `client.SpotApi.Trading.GetOpenOrdersAsync(symbol)` |
| Get all spot orders | `client.SpotApi.Trading.GetOrdersAsync(symbol, ...)` |
| Cancel spot order | `client.SpotApi.Trading.CancelOrderAsync(symbol, orderId: orderId)` |
| Cancel spot order by client id | `client.SpotApi.Trading.CancelOrderAsync(symbol, clientOrderId: clientOrderId)` |
| Cancel all spot orders | `client.SpotApi.Trading.CancelAllOrdersAsync(symbol)` |
| Get spot user trades | `client.SpotApi.Trading.GetUserTradesAsync(symbol)` |

## Spot Sub-Accounts

Sub-account endpoints are under `SpotApi.SubAccount`, not under a `GeneralApi`.

| User intent | Mexc.Net member |
|---|---|
| Get sub-account list | `client.SpotApi.SubAccount.GetSubUserAccountsAsync(...)` |
| Get sub-account API details | `client.SpotApi.SubAccount.GetSubUserAccountApiDetailsAsync(subAccount)` |
| Create virtual sub-account | `client.SpotApi.SubAccount.CreateSubAccountAsync(name, note)` |
| Create sub-account API key | `client.SpotApi.SubAccount.CreateSubAccountApiKeyAsync(...)` |
| Delete sub-account API key | `client.SpotApi.SubAccount.DeleteSubAccountApiKeyAsync(subAccount, apiKey)` |
| Get sub-account balances | `client.SpotApi.SubAccount.GetSubAccountBalancesAsync(subAccount, accountType)` |
| Universal transfer for master/sub accounts | `client.SpotApi.SubAccount.UniversalTransferAsync(...)` |
| Get universal transfer history | `client.SpotApi.SubAccount.GetUniversalTransfersAsync(...)` |

## Futures REST

MEXC futures symbols use underscore format, for example `ETH_USDT`.

| User intent | Mexc.Net member |
|---|---|
| Get futures server time | `client.FuturesApi.ExchangeData.GetServerTimeAsync()` |
| Get one futures contract | `client.FuturesApi.ExchangeData.GetSymbolAsync("ETH_USDT")` |
| Get all futures contracts | `client.FuturesApi.ExchangeData.GetSymbolsAsync()` |
| Get transferable futures assets | `client.FuturesApi.ExchangeData.GetTransferableAssetsAsync()` |
| Get futures order book | `client.FuturesApi.ExchangeData.GetOrderBookAsync("ETH_USDT")` |
| Get futures index price | `client.FuturesApi.ExchangeData.GetIndexPriceAsync("ETH_USDT")` |
| Get futures mark price | `client.FuturesApi.ExchangeData.GetMarkPriceAsync("ETH_USDT")` |
| Get futures funding rate | `client.FuturesApi.ExchangeData.GetFundingRateAsync("ETH_USDT")` |
| Get all futures funding rates | `client.FuturesApi.ExchangeData.GetFundingRatesAsync()` |
| Get futures klines | `client.FuturesApi.ExchangeData.GetKlinesAsync("ETH_USDT", FuturesKlineInterval.OneMinute)` |
| Get futures index price klines | `client.FuturesApi.ExchangeData.GetIndexPriceKlinesAsync(...)` |
| Get futures mark price klines | `client.FuturesApi.ExchangeData.GetMarkPriceKlinesAsync(...)` |
| Get futures recent trades | `client.FuturesApi.ExchangeData.GetRecentTradesAsync("ETH_USDT")` |
| Get futures ticker | `client.FuturesApi.ExchangeData.GetTickerAsync("ETH_USDT")` |
| Get all futures tickers | `client.FuturesApi.ExchangeData.GetTickersAsync()` |
| Get risk fund balances | `client.FuturesApi.ExchangeData.GetRiskFundBalancesAsync()` |
| Get risk fund balance history | `client.FuturesApi.ExchangeData.GetRiskFundBalanceHistoryAsync(symbol, ...)` |
| Get funding rate history | `client.FuturesApi.ExchangeData.GetFundingRateHistoryAsync(symbol, ...)` |
| Get one futures balance | `client.FuturesApi.Account.GetBalanceAsync(asset)` |
| Get futures balances | `client.FuturesApi.Account.GetBalancesAsync()` |
| Get futures transfer history | `client.FuturesApi.Account.GetTransferHistoryAsync(...)` |
| Get user funding history | `client.FuturesApi.Account.GetFundingHistoryAsync(...)` |
| Get futures trading fees | `client.FuturesApi.Account.GetTradingFeesAsync(symbol)` |
| Change position margin | `client.FuturesApi.Account.ChangeMarginAsync(positionId, quantity, changeType)` |
| Get leverage | `client.FuturesApi.Account.GetLeverageAsync(symbol)` |
| Set leverage | `client.FuturesApi.Account.SetLeverageAsync(...)` |
| Get position mode | `client.FuturesApi.Account.GetPositionModeAsync()` |
| Set position mode | `client.FuturesApi.Account.SetPositionModeAsync(positionMode)` |
| Get personal profit rate | `client.FuturesApi.Account.GetProfitRateAsync(period)` |
| Get deduction config | `client.FuturesApi.Account.GetDeductionConfigAsync()` |
| Get discount types | `client.FuturesApi.Account.GetDiscountTypesAsync()` |
| Get zero-fee symbols | `client.FuturesApi.Account.GetZeroFeeSymbolsAsync(symbol)` |
| Toggle auto-add margin | `client.FuturesApi.Account.ToggleAutoAddMarginAsync(positionId, isEnabled)` |
| Get open futures orders | `client.FuturesApi.Trading.GetOpenOrdersAsync(...)` |
| Get futures order history | `client.FuturesApi.Trading.GetOrderHistoryAsync(...)` |
| Get futures order by id | `client.FuturesApi.Trading.GetOrderAsync(orderId)` |
| Get futures order by client id | `client.FuturesApi.Trading.GetOrderByClientOrderIdAsync(symbol, clientOrderId)` |
| Get futures orders by ids | `client.FuturesApi.Trading.GetOrdersByIdAsync(orderIds)` |
| Get futures order trades | `client.FuturesApi.Trading.GetOrderTradesAsync(orderId)` |
| Get futures user trades | `client.FuturesApi.Trading.GetUserTradesAsync(symbol, ...)` |
| Get trigger orders | `client.FuturesApi.Trading.GetTriggerOrdersAsync(...)` |
| Get TP/SL orders | `client.FuturesApi.Trading.GetTpSlOrdersAsync(...)` |
| Get open positions | `client.FuturesApi.Trading.GetPositionsAsync(symbol)` |
| Get position history | `client.FuturesApi.Trading.GetPositionHistoryAsync(...)` |
| Get risk limits | `client.FuturesApi.Trading.GetRiskLimitsAsync(symbol)` |
| Place futures order | `client.FuturesApi.Trading.PlaceOrderAsync(...)` |
| Place multiple futures orders | `client.FuturesApi.Trading.PlaceMultipleOrdersAsync(...)` |
| Cancel futures orders by id | `client.FuturesApi.Trading.CancelOrdersAsync(orderIds)` |
| Cancel futures orders by client id | `client.FuturesApi.Trading.CancelOrdersByClientOrderIdsAsync(...)` |
| Cancel all futures orders | `client.FuturesApi.Trading.CancelAllOrdersAsync(symbol)` |
| Chase futures order | `client.FuturesApi.Trading.ChaseOrderAsync(orderId)` |
| Edit futures order | `client.FuturesApi.Trading.EditOrderAsync(orderId, quantity, price)` |
| Reverse position | `client.FuturesApi.Trading.ReversePositionAsync(symbol, positionId, quantity)` |
| Close all positions | `client.FuturesApi.Trading.CloseAllPositionsAsync()` |
| Get open order counts | `client.FuturesApi.Trading.GetOpenOrderCountsAsync()` |
| Place plan order | `client.FuturesApi.Trading.PlacePlanOrderAsync(...)` |
| Edit plan order | `client.FuturesApi.Trading.EditPlanOrderAsync(...)` |
| Cancel plan orders | `client.FuturesApi.Trading.CancelPlanOrdersAsync(...)` |
| Cancel all planned orders | `client.FuturesApi.Trading.CancelAllPlannedOrdersAsync(symbol)` |
| Place TP/SL order | `client.FuturesApi.Trading.PlaceTpSlOrderAsync(...)` |
| Cancel TP/SL orders | `client.FuturesApi.Trading.CancelTpSlOrdersAsync(...)` |
| Cancel all TP/SL orders | `client.FuturesApi.Trading.CancelAllTpSlOrdersAsync(...)` |
| Edit limit order TP/SL | `client.FuturesApi.Trading.EditLimitOrderTpSlAsync(...)` |
| Edit TP/SL order | `client.FuturesApi.Trading.EditTpSlOrderAsync(...)` |
| Get open TP/SL orders | `client.FuturesApi.Trading.GetOpenTpSlOrdersAsync(symbol)` |
| Place trailing order | `client.FuturesApi.Trading.PlaceTrailingOrderAsync(...)` |
| Cancel trailing order | `client.FuturesApi.Trading.CancelTrailingOrderAsync(symbol, orderId)` |
| Edit trailing order | `client.FuturesApi.Trading.EditTrailingOrderAsync(...)` |
| Get trailing orders | `client.FuturesApi.Trading.GetTrailingOrdersAsync(...)` |

## Spot WebSocket

| User intent | Mexc.Net member |
|---|---|
| Subscribe spot mini ticker | `socketClient.SpotApi.SubscribeToMiniTickerUpdatesAsync(symbol, handler)` |
| Subscribe many spot mini tickers | `socketClient.SpotApi.SubscribeToMiniTickerUpdatesAsync(symbols, handler)` |
| Subscribe all spot mini tickers | `socketClient.SpotApi.SubscribeToAllMiniTickerUpdatesAsync(handler)` |
| Subscribe spot klines | `socketClient.SpotApi.SubscribeToKlineUpdatesAsync(symbol, interval, handler)` |
| Subscribe spot order book diffs | `socketClient.SpotApi.SubscribeToOrderBookUpdatesAsync(symbol, handler)` |
| Subscribe spot order book diffs with interval | `socketClient.SpotApi.SubscribeToOrderBookUpdatesAsync(symbol, updateInterval, handler)` |
| Subscribe spot partial order book | `socketClient.SpotApi.SubscribeToPartialOrderBookUpdatesAsync(symbol, depth, handler)` |
| Subscribe spot book ticker | `socketClient.SpotApi.SubscribeToBookTickerUpdatesAsync(symbol, handler)` |
| Subscribe spot trades | `socketClient.SpotApi.SubscribeToTradeUpdatesAsync(symbol, handler)` |
| Subscribe spot trades with interval | `socketClient.SpotApi.SubscribeToTradeUpdatesAsync(symbol, updateInterval, handler)` |
| Subscribe spot account updates | `socketClient.SpotApi.SubscribeToAccountUpdatesAsync(listenKey, handler)` |
| Subscribe spot order updates | `socketClient.SpotApi.SubscribeToOrderUpdatesAsync(listenKey, handler)` |
| Subscribe spot user trade updates | `socketClient.SpotApi.SubscribeToUserTradeUpdatesAsync(listenKey, handler)` |

## Futures WebSocket

| User intent | Mexc.Net member |
|---|---|
| Subscribe all futures tickers | `socketClient.FuturesApi.SubscribeToTickersUpdatesAsync(handler)` |
| Subscribe futures ticker | `socketClient.FuturesApi.SubscribeToTickerUpdatesAsync(symbol, handler)` |
| Subscribe futures trades | `socketClient.FuturesApi.SubscribeToTradeUpdatesAsync(symbol, handler)` |
| Subscribe futures klines | `socketClient.FuturesApi.SubscribeToKlineUpdatesAsync(symbol, interval, handler)` |
| Subscribe futures order book diffs | `socketClient.FuturesApi.SubscribeToOrderBookUpdatesAsync(symbol, handler)` |
| Subscribe futures partial order book | `socketClient.FuturesApi.SubscribeToPartialOrderBookUpdatesAsync(symbol, limit, handler)` |
| Subscribe futures funding rate | `socketClient.FuturesApi.SubscribeToFundingRateUpdatesAsync(symbol, handler)` |
| Subscribe futures index price | `socketClient.FuturesApi.SubscribeToIndexPriceUpdatesAsync(symbol, handler)` |
| Subscribe futures mark price | `socketClient.FuturesApi.SubscribeToMarkPriceUpdatesAsync(symbol, handler)` |
| Subscribe futures contract updates | `socketClient.FuturesApi.SubscribeToSymbolUpdatesAsync(handler)` |
| Subscribe futures user data | `socketClient.FuturesApi.SubscribeToUserDataUpdatesAsync(...)` |

## Shared API V2

`IMexcSharedApiClient` groups these aggregate properties. Each corresponding native surface exposes `.SharedApi`.

- `SpotRest` (`IMexcRestClientSpotSharedApi`): `IGetAssetRest`, `IGetAllAssetsRest`, `IGetBalancesRest`, `IGetDepositAddressesRest`, `IGetDepositHistoryRest`, `IGetKlinesRest`, `IGetRecentTradesRest`, `IGetOrderBookRest`, `IPlaceSpotOrderRest`, `IGetSpotOrderRest`, `IGetOpenSpotOrdersRest`, `IGetClosedSpotOrdersRest`, `IGetSpotOrderTradesRest`, `ICancelSpotOrderRest`, `IGetSpotUserTradeHistoryRest`, `IGetSpotSymbolsRest`, `IGetTickerRest`, `IGetAllTickersRest`, `IGetWithdrawalHistoryRest`, `IWithdrawRest`, `IGetFeesRest`, `IGetBookTickerRest`, `ITransferRest`, `IGetSpotOrderByClientOrderIdRest`, `ICancelSpotOrderByClientOrderIdRest`.
- `SpotSocket` (`IMexcSocketClientSpotSharedApi`): `ISubscribeTradesSocket`, `ISubscribeTickerSocket`, `ISubscribeAllTickersSocket`, `ISubscribeBookTickerSocket`, `ISubscribeKlinesSocket`, `ISubscribeOrderBookSocket`, `ISubscribeBalancesSocket`, `ISubscribeSpotOrdersSocket`, `ISubscribeUserTradesSocket`.
- `FuturesRest` (`IMexcRestClientFuturesSharedApi`): `IGetKlinesRest`, `IGetOrderBookRest`, `IGetRecentTradesRest`, `IGetFundingRateHistoryRest`, `IGetFuturesSymbolsRest`, `IGetTickerRest`, `IGetAllTickersRest`, `IGetBalancesRest`, `IGetLeverageRest`, `ISetLeverageRest`, `IGetPositionModeRest`, `ISetPositionModeRest`, `IGetPositionHistoryRest`, `IGetFeesRest`, `IPlaceFuturesOrderRest`, `IGetFuturesOrderRest`, `IGetOpenFuturesOrdersRest`, `IGetClosedFuturesOrdersRest`, `IGetFuturesOrderTradesRest`, `IGetFuturesUserTradeHistoryRest`, `ICancelFuturesOrderRest`, `IGetPositionsRest`, `ICloseFullPositionRest`, `IPlaceFuturesTriggerOrderRest`, `IGetFuturesTriggerOrderRest`, `ICancelFuturesTriggerOrderRest`, `IGetFuturesOrderByClientOrderIdRest`, `ICancelFuturesOrderByClientOrderIdRest`.
- `FuturesSocket` (`IMexcSocketClientFuturesSharedApi`): `ISubscribeKlinesSocket`, `ISubscribeOrderBookSocket`, `ISubscribeTickerSocket`, `ISubscribeAllTickersSocket`, `ISubscribeTradesSocket`, `ISubscribeBalancesSocket`, `ISubscribeFuturesOrdersSocket`, `ISubscribePositionsSocket`, `ISubscribeUserTradesSocket`.

Use aggregate properties or inject narrow capabilities for known workflows. For dynamic selection call `aggregate.GetCapability(SharedCapabilities.Tickers.GetTicker.Rest, tradingMode: TradingMode.Spot)` and check for null before using `Capability` and `Options`. Descriptors identify operations, not guarantees of support. `GetCapabilities` enumerates matches and `Discover` summarizes metadata. With several exchanges registered, inject `IEnumerable<TCapability>` and select by exchange, supported mode, and options.

## Symbols, catalogs, and quantities

Spot symbols use `BTCUSDT`; Futures use `ETH_USDT`. `SharedSymbol` and `MexcExchange.FormatSymbol` support reusable formatting. Native symbol discovery and API-supported Spot symbol discovery are distinct: do not assume every listed market accepts API orders.

Call `IGetSpotSymbolsRest.GetSpotSymbolsAsync(new GetSymbolsRequest())` or `IGetFuturesSymbolsRest.GetFuturesSymbolsAsync(new GetSymbolsRequest())`, check success, then read `SpotSymbolCatalog` or `FuturesSymbolCatalog`. Requests honor base/quote asset type/subtype filters. Returned metadata includes display names and crypto/stablecoin/fiat/TradFi equity/commodity classifications where populated. Inspect availability, precision, min/max quantity and contract-size metadata rather than assuming all products share rules.

Native Futures order `quantity` is decimal contract volume. Shared Futures requests use `SharedOrderQuantity.QuantityInContracts`; Futures books identify `SharedQuantityType.Contracts`. Use contract size and volume constraints for conversions rather than sending base-asset quantity as contracts. Spot books use base quantities; Spot market orders may use base `quantity` or quote `quoteQuantity` as supported by that operation.

## Additional native workflows and prerequisites

Spot batch orders return item results that must be inspected after outer success. Subaccount creation, key administration, and universal transfers are under `SpotApi.SubAccount`. Spot account methods include transfers, dust conversion, fee deduction, deposit/withdrawal history and addresses.

Futures exposes separate ordinary, plan/trigger, TP/SL, and trailing-order workflows, plus reverse-position and close-all operations. Use the matching query/cancel path; cancelling an ordinary order is not equivalent to removing every conditional order. Read current position and margin mode before changing leverage or closing exposure.

Private Spot credential-based socket overloads manage listen keys. Explicit listen-key overloads require application-owned start/keepalive/stop handling, including renewed keys where applicable. Futures user-data subscriptions authenticate directly. Spot socket payloads use protobuf, which the library decodes; consumers should use the typed subscriptions.

Only `MexcEnvironment.Live` is built in; `CreateCustom(...)` selects explicit addresses and does not imply an official testnet. `IMexcOrderBookFactory`, `IMexcTrackerFactory`, and `IMexcUserClientProvider` support managed books, trackers, and cached user clients.

## Checkout-only sources

- [Public interfaces](../../../../Mexc.Net/Interfaces/Clients/): exact signatures, order sides, mode parameters, and cancellation tokens.
- [Implementations](../../../../Mexc.Net/Clients/): Shared options, conversions, and protobuf subscription behavior.
- [Complete examples](../../../../Examples/ai-friendly/): independent consumer programs.
