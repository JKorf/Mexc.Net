# ![.Mexc.Net](https://github.com/JKorf/Mexc.Net/blob/main/Mexc.Net/Icon/icon.png?raw=true) Mexc.Net
[![.NET](https://github.com/JKorf/Mexc.Net/actions/workflows/dotnet.yml/badge.svg)](https://github.com/JKorf/Mexc.Net/actions/workflows/dotnet.yml)  

Mexc.Net is a wrapper around the Mexc API as described on [Mexc](https://mexcdevelop.github.io/apidocs/spot_v3_en/#introduction), including all features the API provides using clear and readable objects. The library support the spot, (isolated) margin and futures API's, both the REST and websocket API's.

## Get the library
Available on Nuget  
[![Nuget version](https://img.shields.io/nuget/v/jk.mexc.net.svg)](https://www.nuget.org/packages/JK.Mexc.Net)  [![Nuget downloads](https://img.shields.io/nuget/dt/JK.Mexc.Net.svg)](https://www.nuget.org/packages/JK.Mexc.Net)

`dotnet add package JK.Mexc.Net`

## How to use
Simplest usage
```csharp
// Get the ETH/USDT ticker via rest request
var restClient = new MexcRestClient();
var tickerResult = await restClient.SpotApi.ExchangeData.GetTickerAsync("ETHUSDT");
var lastPrice = tickerResult.Data.LastPrice;
```

```csharp
// Subscribe to ETH/USDT ticker updates via the websocket API
var socketClient = new MexcSocketClient();
var tickerSubscriptionResult = socketClient.SpotApi.SubscribeToMiniTickerUpdatesAsync("ETHUSDT", (update) => 
{
  var lastPrice = update.Data.LastPrice;
});
```

For information on the clients, dependency injection, response processing and more see the [documentation](https://jkorf.github.io/CryptoExchange.Net), or have a look at the examples  [here](https://github.com/JKorf/CryptoExchange.Net/tree/master/Examples).


## CryptoExchange.Net
Mexc.Net is based on the [CryptoExchange.Net](https://github.com/JKorf/CryptoExchange.Net) base library. Other exchange API implementations based on the CryptoExchange.Net base library are available and follow the same logic.

CryptoExchange.Net also allows for [easy access to different exchange API's](https://jkorf.github.io/CryptoExchange.Net#idocs_common).

|Exchange|Repository|Nuget|
|--|--|--|
|Binance|[JKorf/Binance.Net](https://github.com/JKorf/Binance.Net)|[![Nuget version](https://img.shields.io/nuget/v/Binance.net.svg)](https://www.nuget.org/packages/Binance.Net)|
|Bitfinex|[JKorf/Bitfinex.Net](https://github.com/JKorf/Bitfinex.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bitfinex.net.svg)](https://www.nuget.org/packages/Bitfinex.Net)|
|Bitget|[JKorf/Bitget.Net](https://github.com/JKorf/Bitget.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bitget.net.svg)](https://www.nuget.org/packages/Bitget.Net)|
|Bybit|[JKorf/Bybit.Net](https://github.com/JKorf/Bybit.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bybit.net.svg)](https://www.nuget.org/packages/Bybit.Net)|
|CoinEx|[JKorf/CoinEx.Net](https://github.com/JKorf/CoinEx.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinEx.net.svg)](https://www.nuget.org/packages/CoinEx.Net)|
|CoinGecko|[JKorf/CoinGecko.Net](https://github.com/JKorf/CoinGecko.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinGecko.net.svg)](https://www.nuget.org/packages/CoinGecko.Net)|
|Huobi/HTX|[JKorf/Huobi.Net](https://github.com/JKorf/Huobi.Net)|[![Nuget version](https://img.shields.io/nuget/v/Huobi.net.svg)](https://www.nuget.org/packages/Huobi.Net)|
|Kraken|[JKorf/Kraken.Net](https://github.com/JKorf/Kraken.Net)|[![Nuget version](https://img.shields.io/nuget/v/KrakenExchange.net.svg)](https://www.nuget.org/packages/KrakenExchange.Net)|
|Kucoin|[JKorf/Kucoin.Net](https://github.com/JKorf/Kucoin.Net)|[![Nuget version](https://img.shields.io/nuget/v/Kucoin.net.svg)](https://www.nuget.org/packages/Kucoin.Net)|
|OKX|[JKorf/OKX.Net](https://github.com/JKorf/OKX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.OKX.net.svg)](https://www.nuget.org/packages/JK.OKX.Net)|

## Discord
A Discord server is available [here](https://discord.gg/MSpeEtSY8t). Feel free to join for discussion and/or questions around the CryptoExchange.Net and implementation libraries.

## Supported functionality

### Spot V3
|API|Supported|Location|
|--|--:|--|
|Market Data Endpoints|✓|`restClient.SpotApi.ExchangeData`|
|SubAccount Endpoints|X||
|Acount/Trade|✓|`restClient.SpotApi.Account` / `restClient.SpotApi.Trading`|
|Wallet endpoints|✓|`restClient.SpotApi.Account`|
|Websocket Market Streams|✓|`socketClient.SpotApi`|
|Websocket User Data Streams|✓|`socketClient.SpotApi`|

### Futures
*Futures is not currently available in the MEXC API*

|API|Supported|Location|
|--|--:|--|
|*|X||

### Broker
|API|Supported|Location|
|--|--:|--|
|*|X||

## Support the project
I develop and maintain this package on my own for free in my spare time, any support is greatly appreciated.

### Donate
Make a one time donation in a crypto currency of your choice. If you prefer to donate a currency not listed here please contact me.

**Btc**:  bc1qz0jv0my7fc60rxeupr23e75x95qmlq6489n8gh  
**Eth**:  0x8E21C4d955975cB645589745ac0c46ECA8FAE504  

### Sponsor
Alternatively, sponsor me on Github using [Github Sponsors](https://github.com/sponsors/JKorf). 

## Release notes
* Version 0.0.1-beta1 - 06 Feb 2024
    * Initial release
