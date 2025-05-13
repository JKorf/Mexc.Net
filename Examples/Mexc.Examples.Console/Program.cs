using Mexc.Net.Clients;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Mexc.Net.Objects.Options;
using Microsoft.Extensions.Options;

// REST
var restClient = new MexcRestClient();
var ticker = await restClient.SpotApi.ExchangeData.GetTickerAsync("ETHUSDT");
Console.WriteLine($"Rest client ticker price for ETHUSDT: {ticker.Data.LastPrice}");

Console.WriteLine();
Console.WriteLine("Press enter to start websocket subscription");
Console.ReadLine();

// Websocket
// Optional, manually add logging
var logFactory = new LoggerFactory();
logFactory.AddProvider(new TraceLoggerProvider());

var socketClient = new MexcSocketClient(Options.Create(new MexcSocketOptions { }), logFactory);
var subscription = await socketClient.SpotApi.SubscribeToMiniTickerUpdatesAsync("ETHUSDT", update =>
{
    Console.WriteLine($"Websocket client ticker price for ETHUSDT: {update.Data.LastPrice}");
});

Console.ReadLine();
