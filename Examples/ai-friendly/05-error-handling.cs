// 05-error-handling.cs
//
// Demonstrates: WebCallResult patterns, retry logic, common error scenarios.
//
// Setup: dotnet add package JK.Mexc.Net

using CryptoExchange.Net.Objects;
using Mexc.Net;
using Mexc.Net.Clients;
using Mexc.Net.Enums;

var client = new MexcRestClient(options =>
{
    options.ApiCredentials = new MexcCredentials("API_KEY", "API_SECRET");
});

// ---- 1. THE BASIC PATTERN ----
// Every REST method returns WebCallResult<T> or WebCallResult.
// .Success is true/false. .Data is valid only when .Success.
// .Error contains structured error info when .Success is false.
// .Error.IsTransient hints whether a retry might succeed.

var result = await client.SpotApi.ExchangeData.GetTickerAsync("BTCUSDT");

if (result.Success)
{
    Console.WriteLine($"Price: {result.Data.LastPrice}");
}
else
{
    Console.WriteLine($"Code:      {result.Error?.Code}");
    Console.WriteLine($"Message:   {result.Error?.Message}");
    Console.WriteLine($"Type:      {result.Error?.ErrorType}");
    Console.WriteLine($"Transient: {result.Error?.IsTransient}");
}

// ---- 2. SIMPLE RETRY WITH BACKOFF ----
// Retry only on transient errors such as network issues, rate limits, or server overload.
// Do not retry validation errors, unknown symbols, or insufficient balance.

async Task<WebCallResult<T>> WithRetry<T>(
    Func<Task<WebCallResult<T>>> call,
    int maxAttempts = 3)
{
    WebCallResult<T> last = default!;
    for (var attempt = 1; attempt <= maxAttempts; attempt++)
    {
        last = await call();
        if (last.Success)
            return last;

        if (last.Error?.IsTransient != true)
            return last;

        await Task.Delay(TimeSpan.FromMilliseconds(250 * Math.Pow(2, attempt)));
    }

    return last;
}

var ticker = await WithRetry(
    () => client.SpotApi.ExchangeData.GetTickerAsync("BTCUSDT"));

if (ticker.Success)
    Console.WriteLine($"Retried ticker: {ticker.Data.LastPrice}");

// ---- 3. COMMON MEXC ERROR SCENARIOS ----
//
// Spot 429:
//   Rate limit hit. Retry with backoff if the operation is safe to retry.
//
// Spot 700003 / 10073:
//   Invalid timestamp. Check local clock and timestamp/recvWindow settings.
//
// Spot 10095 / 10096 / 10097 / 10102 / 30002 / 30003 / 30029:
//   Invalid quantity. Validate against exchange info before placing the order.
//
// Spot 30010:
//   Invalid price. Validate price precision and allowed symbol rules.
//
// Spot 10101 / 30004:
//   Insufficient balance. Permanent for the current request; surface to caller.
//
// Spot -2011 / 30026:
//   Unknown order. May be expected for already-filled, canceled, or missing orders.
//
// Futures 510 / 2037:
//   Too many requests. Transient; back off and retry if the operation is idempotent.
//
// Futures 2005 / 2018:
//   Insufficient balance or margin. Permanent until account state changes.
//
// Futures 2009:
//   No open position. Handle as a normal state when trying to close or update a position.

// ---- 4. ORDER PARAMETER VALIDATION ----
var exchangeInfo = await client.SpotApi.ExchangeData.GetExchangeInfoAsync(new[] { "BTCUSDT" });
if (!exchangeInfo.Success || exchangeInfo.Data.Symbols.Length == 0)
{
    Console.WriteLine($"Cannot fetch symbol info: {exchangeInfo.Error}");
    return;
}

var symbol = exchangeInfo.Data.Symbols.First();
Console.WriteLine($"{symbol.Name} status: {symbol.Status}");

// Current Mexc.Net versions normalize spot order quantity and price scale internally,
// but production code should still validate user inputs against exchange info before trading.
var order = await client.SpotApi.Trading.PlaceOrderAsync(
    symbol: "BTCUSDT",
    side: OrderSide.Buy,
    type: OrderType.Market,
    quoteQuantity: 25m);

if (!order.Success)
{
    var category = order.Error?.IsTransient == true
        ? "Transient - retry may be useful"
        : "Permanent - surface to user or fix inputs";

    Console.WriteLine($"{category}: {order.Error?.Code} {order.Error?.Message}");
}

// ---- 5. EXCEPTIONS VS ERROR RESULTS ----
// Mexc.Net returns API failures through WebCallResult.Error, not thrown exceptions.
// Exceptions are normally for:
//   - Misconfiguration
//   - Disposed clients
//   - OperationCanceledException from CancellationToken
//   - Programmer errors

// Common variations:
//   With CancellationToken:   pass `ct: cancellationToken` to any method
//   With timeout per request: options.RequestTimeout = TimeSpan.FromSeconds(10)
//   Futures retry target:     client.FuturesApi.ExchangeData.GetTickerAsync("ETH_USDT")
