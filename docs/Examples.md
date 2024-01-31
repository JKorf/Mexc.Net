---
title: Examples
nav_order: 3
---

## Basic operations
Make sure to read the [CryptoExchange.Net documentation](https://jkorf.github.io/CryptoExchange.Net/Clients.html#processing-request-responses) on processing responses.

### Get market data
```csharp
// Getting info on all symbols trading on Mexc
var exchangeData = await mexcRestClient.SpotApi.ExchangeData.GetExchangeInfoAsync();

// Getting info on all symbols supported by the API
var symbolData = await mexcRestClient.SpotApi.ExchangeData.GetApiSymbolsAsync();

// Getting tickers for all symbols
var tickerData = await mexcRestClient.SpotApi.ExchangeData.GetTickersAsync();

// Getting the order book of a symbol
var orderBookData = await mexcRestClient.SpotApi.ExchangeData.GetOrderBookAsync("ETHUSDC");

// Getting recent trades of a symbol
var tradeHistoryData = await mexcRestClient.SpotApi.ExchangeData.GetRecentTradesAsync("ETHUSDC");
```

### Requesting balances
```csharp
var accountData = await mexcRestClient.SpotApi.Account.GetAccountInfoAsync();
```
### Placing order
```csharp
// Placing a buy limit order for 0.001 BTC at a price of 50000USDC each
var symbolData = await mexcRestClient.SpotApi.Trading.PlaceOrderAsync("BTCUSDC", OrderSide.Buy, OrderType.Limit, 0.01m, price: 50000);
```
### Requesting a specific order
```csharp
// Request info on order with id `1234`
var orderData = await mexcRestClient.SpotApi.Trading.GetOrderAsync("BTCUSDC", "1234");
```

### Requesting order history
```csharp
// Get all orders conform the parameters
 var ordersData = await mexcRestClient.SpotApi.Trading.GetOrdersAsync("BTCUSDC");
```

### Cancel order
```csharp
// Cancel order with id `1234`
var orderData = await await mexcRestClient.SpotApi.Trading.CancelOrderAsync("BTCUSDC", "1234");
```

### Get user trades
```csharp
var userTradesResult = await mexcRestClient.SpotApi.Trading.GetUserTradesAsync("BTCUSDC");
```

### Subscribing to market data updates
```csharp
var subscribeResult = await mexcSocketClient.SpotApi.SubscribeToTickerUpdatesAsync("BTCUSDT_SPBL", data =>
{
    // Handle ticker data
});
```

### Subscribing to order updates
```csharp
var subscribeResult = 
await mexcSocketClient.SpotApi.SubscribeToOrderUpdatesAsync(data =>
{
    // Handle order data
});
```
