# Mexc.Net AI API Quick Map

Use this file to route common user intents to the correct Mexc.Net client member. If a method name or parameter is not listed here, inspect `Mexc.Net/Interfaces/Clients/**` before generating code.

## Client Roots

| Intent | Use |
|---|---|
| REST calls | `new MexcRestClient()` |
| WebSocket streams | `new MexcSocketClient()` |
| API key authentication | `options.ApiCredentials = new MexcCredentials("key", "secret")` |
| Live environment | `MexcEnvironment.Live` |
| Custom environment | `MexcEnvironment.CreateCustom(...)` |
| Dependency injection | `services.AddMexc(options => { ... })` |
| Spot REST root | `client.SpotApi` |
| Futures REST root | `client.FuturesApi` |
| Spot socket root | `socketClient.SpotApi` |
| Futures socket root | `socketClient.FuturesApi` |

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

## SharedApis

Use SharedApis for exchange-agnostic code across MEXC, Binance, Bybit, OKX, Kraken, and other CryptoExchange.Net libraries.

| User intent | Mexc.Net member or interface |
|---|---|
| Shared spot REST client | `new MexcRestClient().SpotApi.SharedClient` |
| Shared futures REST client | `new MexcRestClient().FuturesApi.SharedClient` |
| Shared spot socket client | `new MexcSocketClient().SpotApi.SharedClient` |
| Shared futures socket client | `new MexcSocketClient().FuturesApi.SharedClient` |
| Shared spot ticker REST | `ISpotTickerRestClient.GetSpotTickerAsync(new GetTickerRequest(symbol))` |
| Shared spot symbol REST | `ISpotSymbolRestClient.GetSpotSymbolsAsync(...)` |
| Shared spot order REST | `ISpotOrderRestClient.PlaceSpotOrderAsync(...)` |
| Shared futures order REST | `IFuturesOrderRestClient.PlaceFuturesOrderAsync(...)` |
| Shared futures trigger order REST | `IFuturesTriggerOrderRestClient.PlaceFuturesTriggerOrderAsync(...)` |
| Shared balances REST | `IBalanceRestClient.GetBalancesAsync(...)` |
| Shared fees REST | `IFeeRestClient.GetFeeAsync(...)` |
| Shared ticker socket | `ITickerSocketClient.SubscribeToTickerUpdatesAsync(...)` |
| Shared order book socket | `IOrderBookSocketClient.SubscribeToOrderBookUpdatesAsync(...)` |
| Shared trades socket | `ITradeSocketClient.SubscribeToTradeUpdatesAsync(...)` |
| Shared spot order socket | `ISpotOrderSocketClient.SubscribeToSpotOrderUpdatesAsync(...)` |
| Shared user trade socket | `IUserTradeSocketClient.SubscribeToUserTradeUpdatesAsync(...)` |

For shared socket subscriptions, keep the concrete socket client and unsubscribe with `await socketClient.UnsubscribeAsync(subscription.Data)`.

## Result Handling

| Situation | Pattern |
|---|---|
| REST success check | `if (!result.Success) { Console.WriteLine(result.Error); return; }` |
| Socket subscription success check | `if (!sub.Success) { Console.WriteLine(sub.Error); return; }` |
| Read REST data | Read `result.Data` only after `result.Success` |
| Retry decision | Retry only when `result.Error?.IsTransient == true` |
| Cancellation | Pass `ct: cancellationToken` |

## Common Routing Pitfalls

| Do not use | Use instead |
|---|---|
| `MexcClient` | `MexcRestClient` |
| `ApiCredentials` | `MexcCredentials` |
| `dotnet add package Mexc.Net` | `dotnet add package JK.Mexc.Net` |
| `GeneralApi` | `SpotApi.SubAccount` or spot/futures account roots |
| `UsdFuturesApi` / `CoinFuturesApi` | `FuturesApi` |
| `MexcEnvironment.Testnet` | `MexcEnvironment.Live` or `MexcEnvironment.CreateCustom(...)` |
| Spot symbol `ETH_USDT` | Spot symbol `ETHUSDT` |
| Futures symbol `ETHUSDT` | Futures symbol `ETH_USDT` |
| Spot private socket without listen key | `StartUserStreamAsync()` then pass the listen key |
| `.Data` without `.Success` check | Check `.Success` first |
| `ITickerSocketClient.UnsubscribeAsync(...)` | Keep the concrete socket client and call `socketClient.UnsubscribeAsync(subscription.Data)` |
| Custom `clientOrderId` by default | Let Mexc.Net omit/generate it unless external correlation is required |
