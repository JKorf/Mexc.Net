# ![.Mexc.Net](https://github.com/JKorf/Mexc.Net/blob/main/Mexc.Net/Icon/icon.png?raw=true) Mexc.Net

[![.NET](https://img.shields.io/github/actions/workflow/status/JKorf/Mexc.Net/dotnet.yml?style=for-the-badge)](https://github.com/JKorf/Mexc.Net/actions/workflows/dotnet.yml) ![License](https://img.shields.io/github/license/JKorf/Mexc.Net?style=for-the-badge)

Mexc.Net is a strongly typed client library for accessing the [Mexc REST and Websocket API](https://mexcdevelop.github.io/apidocs/spot_v3_en/#introduction). 
## Features
* Response data is mapped to descriptive models
* Input parameters and response values are mapped to discriptive enum values where possible
* Automatic websocket (re)connection management 
* Client side rate limiting 
* Client side order book implementation
* Support for managing different accounts
* Extensive logging
* Support for different environments
* Easy integration with other exchange client based on the CryptoExchange.Net base library
* Native AOT support

## Supported Frameworks
The library is targeting both `.NET Standard 2.0` and `.NET Standard 2.1` for optimal compatibility

|.NET implementation|Version Support|
|--|--|
|.NET Core|`2.0` and higher|
|.NET Framework|`4.6.1` and higher|
|Mono|`5.4` and higher|
|Xamarin.iOS|`10.14` and higher|
|Xamarin.Android|`8.0` and higher|
|UWP|`10.0.16299` and higher|
|Unity|`2018.1` and higher|

## Install the library

### NuGet 
[![NuGet version](https://img.shields.io/nuget/v/JK.Mexc.net.svg?style=for-the-badge)](https://www.nuget.org/packages/JK.Mexc.Net)  [![Nuget downloads](https://img.shields.io/nuget/dt/JK.Mexc.Net.svg?style=for-the-badge)](https://www.nuget.org/packages/JK.Mexc.Net)

	dotnet add package JK.Mexc.Net
	
### GitHub packages
Mexc.Net is available on [GitHub packages](https://github.com/JKorf/Mexc.Net/pkgs/nuget/JK.Mexc.Net). You'll need to add `https://nuget.pkg.github.com/JKorf/index.json` as a NuGet package source.

### Download release
[![GitHub Release](https://img.shields.io/github/v/release/JKorf/Mexc.Net?style=for-the-badge&label=GitHub)](https://github.com/JKorf/Mexc.Net/releases)

The NuGet package files are added along side the source with the latest GitHub release which can found [here](https://github.com/JKorf/Mexc.Net/releases).

## How to use
*REST Endpoints*  

```csharp
// Get the ETH/USDT ticker via rest request
var restClient = new MexcRestClient();
var tickerResult = await restClient.SpotApi.ExchangeData.GetTickerAsync("ETHUSDT");
var lastPrice = tickerResult.Data.LastPrice;
```

*Websocket streams*  

```csharp
// Subscribe to ETH/USDT ticker updates via the websocket API
var socketClient = new MexcSocketClient();
var tickerSubscriptionResult = socketClient.SpotApi.SubscribeToMiniTickerUpdatesAsync("ETHUSDT", (update) => 
{
  var lastPrice = update.Data.LastPrice;
});
```

For information on the clients, dependency injection, response processing and more see the [Mexc.Net documentation](https://cryptoexchange.jkorf.dev?library=Mexc.Net) or have a look at the examples [here](https://github.com/JKorf/Mexc.Net/tree/main/Examples) or [here](https://github.com/JKorf/CryptoExchange.Net/tree/master/Examples).

## CryptoExchange.Net
Mexc.Net is based on the [CryptoExchange.Net](https://github.com/JKorf/CryptoExchange.Net) base library. Other exchange API implementations based on the CryptoExchange.Net base library are available and follow the same logic.

CryptoExchange.Net also allows for [easy access to different exchange API's](https://cryptoexchange.jkorf.dev/client-libs/shared).

|Exchange|Repository|Nuget|
|--|--|--|
|Binance|[JKorf/Binance.Net](https://github.com/JKorf/Binance.Net)|[![Nuget version](https://img.shields.io/nuget/v/Binance.net.svg?style=flat-square)](https://www.nuget.org/packages/Binance.Net)|
|BingX|[JKorf/BingX.Net](https://github.com/JKorf/BingX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.BingX.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.BingX.Net)|
|Bitfinex|[JKorf/Bitfinex.Net](https://github.com/JKorf/Bitfinex.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bitfinex.net.svg?style=flat-square)](https://www.nuget.org/packages/Bitfinex.Net)|
|Bitget|[JKorf/Bitget.Net](https://github.com/JKorf/Bitget.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.Bitget.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.Bitget.Net)|
|BitMart|[JKorf/BitMart.Net](https://github.com/JKorf/BitMart.Net)|[![Nuget version](https://img.shields.io/nuget/v/BitMart.net.svg?style=flat-square)](https://www.nuget.org/packages/BitMart.Net)|
|BitMEX|[JKorf/BitMEX.Net](https://github.com/JKorf/BitMEX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.BitMEX.net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.BitMEX.Net)|
|BloFin|[JKorf/BloFin.Net](https://github.com/JKorf/BloFin.Net)|[![Nuget version](https://img.shields.io/nuget/v/BloFin.net.svg?style=flat-square)](https://www.nuget.org/packages/BloFin.Net)|
|Bybit|[JKorf/Bybit.Net](https://github.com/JKorf/Bybit.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bybit.net.svg?style=flat-square)](https://www.nuget.org/packages/Bybit.Net)|
|Coinbase|[JKorf/Coinbase.Net](https://github.com/JKorf/Coinbase.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.Coinbase.Net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.Coinbase.Net)|
|CoinEx|[JKorf/CoinEx.Net](https://github.com/JKorf/CoinEx.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinEx.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinEx.Net)|
|CoinGecko|[JKorf/CoinGecko.Net](https://github.com/JKorf/CoinGecko.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinGecko.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinGecko.Net)|
|CoinW|[JKorf/CoinW.Net](https://github.com/JKorf/CoinW.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinW.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinW.Net)|
|Crypto.com|[JKorf/CryptoCom.Net](https://github.com/JKorf/CryptoCom.Net)|[![Nuget version](https://img.shields.io/nuget/v/CryptoCom.net.svg?style=flat-square)](https://www.nuget.org/packages/CryptoCom.Net)|
|DeepCoin|[JKorf/DeepCoin.Net](https://github.com/JKorf/DeepCoin.Net)|[![Nuget version](https://img.shields.io/nuget/v/DeepCoin.net.svg?style=flat-square)](https://www.nuget.org/packages/DeepCoin.Net)|
|Gate.io|[JKorf/GateIo.Net](https://github.com/JKorf/GateIo.Net)|[![Nuget version](https://img.shields.io/nuget/v/GateIo.net.svg?style=flat-square)](https://www.nuget.org/packages/GateIo.Net)|
|HTX|[JKorf/HTX.Net](https://github.com/JKorf/HTX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.HTX.Net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.HTX.Net)|
|HyperLiquid|[JKorf/HyperLiquid.Net](https://github.com/JKorf/HyperLiquid.Net)|[![Nuget version](https://img.shields.io/nuget/v/HyperLiquid.Net.svg?style=flat-square)](https://www.nuget.org/packages/HyperLiquid.Net)|
|Kraken|[JKorf/Kraken.Net](https://github.com/JKorf/Kraken.Net)|[![Nuget version](https://img.shields.io/nuget/v/KrakenExchange.net.svg?style=flat-square)](https://www.nuget.org/packages/KrakenExchange.Net)|
|Kucoin|[JKorf/Kucoin.Net](https://github.com/JKorf/Kucoin.Net)|[![Nuget version](https://img.shields.io/nuget/v/Kucoin.net.svg?style=flat-square)](https://www.nuget.org/packages/Kucoin.Net)|
|OKX|[JKorf/OKX.Net](https://github.com/JKorf/OKX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.OKX.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.OKX.Net)|
|Toobit|[JKorf/Toobit.Net](https://github.com/JKorf/Toobit.Net)|[![Nuget version](https://img.shields.io/nuget/v/Toobit.net.svg?style=flat-square)](https://www.nuget.org/packages/Toobit.Net)|
|WhiteBit|[JKorf/WhiteBit.Net](https://github.com/JKorf/WhiteBit.Net)|[![Nuget version](https://img.shields.io/nuget/v/WhiteBit.net.svg?style=flat-square)](https://www.nuget.org/packages/WhiteBit.Net)|
|XT|[JKorf/XT.Net](https://github.com/JKorf/XT.Net)|[![Nuget version](https://img.shields.io/nuget/v/XT.net.svg?style=flat-square)](https://www.nuget.org/packages/XT.Net)|

## Discord
[![Nuget version](https://img.shields.io/discord/847020490588422145?style=for-the-badge)](https://discord.gg/MSpeEtSY8t)  
A Discord server is available [here](https://discord.gg/MSpeEtSY8t). Feel free to join for discussion and/or questions around the CryptoExchange.Net and implementation libraries.

## AOT Support
Mexc websocket uses Protobuf serialization. The library uses the `protobuf-net` library to handle this, but unfortunately this library is not AOT compatible by itself.  
To support AOT compilation:
1. Add the `rd.xml` file to your application start project. File can be found [here](https://github.com/JKorf/Mexc.Net/rd.xml).
2. In your application start project add a reference to the file with the following property in your `.csproj` file: `<RdXmlFile Include="rd.xml" />`

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
Any support is greatly appreciated.

### Donate
Make a one time donation in a crypto currency of your choice. If you prefer to donate a currency not listed here please contact me.

**Btc**:  bc1q277a5n54s2l2mzlu778ef7lpkwhjhyvghuv8qf  
**Eth**:  0xcb1b63aCF9fef2755eBf4a0506250074496Ad5b7   
**USDT (TRX)**  TKigKeJPXZYyMVDgMyXxMf17MWYia92Rjd

### Sponsor
Alternatively, sponsor me on Github using [Github Sponsors](https://github.com/sponsors/JKorf). 

## Release notes
* Version 3.9.0 - 30 Sep 2025
    * Updated CryptoExchange.Net version to 9.8.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added ITrackerFactory to TrackerFactory implementation
    * Added ContractAddress mapping in Shared IAssetClient implementation

* Version 3.8.0 - 01 Sep 2025
    * Updated CryptoExchange.Net.Protobuf version to 9.7.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * HTTP REST requests will now use HTTP version 2.0 by default

* Version 3.7.0 - 25 Aug 2025
    * Updated CryptoExchange.Net.Protobuf version to 9.6.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added ClearUserClients method to user client provider

* Version 3.6.0 - 20 Aug 2025
    * Updated CryptoExchange.Net to version 9.5.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added improved error parsing
    * Updated rest request sending too prevent duplicate parameter serialization
    * Removed miniticker spot API subscription as it's no longer supported

* Version 3.5.0 - 04 Aug 2025
    * Updated CryptoExchange.Net.Protobuf to version 9.4.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added support for multi-symbol Shared socket subscriptions

* Version 3.4.0 - 31 Jul 2025
    * Added futures REST and Websocket API implementation (exluding order operations since the API for it is disabled)

* Version 3.3.0 - 23 Jul 2025
    * Updated CryptoExchange.Net to version 9.3.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Updated websocket message matching
    * Add SequenceEnd property to order book update events
    * Fixed threading issue in protobuf initialization

* Version 3.2.0 - 14 Jul 2025
    * Updated from CryptoExchange.Net version 9.1.0 to CryptoExchange.Net.Protobuf version 9.2.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added updateInterval parameter to socketClient.SpotApi.SubscribeToTradeUpdatesAsync and SubscribeToOrderBookUpdatesAsync subscriptions
    * Added socketClient.SpotApi.SubscribeToOrderBookUpdatesAsync subscription
    * Added check for listenKey being null to subscription requiring a listenKey
    * Updated spot websocket implementation to use the protobuf protocol

* Version 3.1.1 - 20 Jun 2025
    * Fixed mapping of ClientOrderId in restClient.SpotApi.Trading.PlaceMultipleOrdersAsync response

* Version 3.1.0 - 02 Jun 2025
    * Updated CryptoExchange.Net to version 9.1.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added (I)MexcUserClientProvider allowing for easy client management when handling multiple users

* Version 3.0.0 - 13 May 2025
    * Updated CryptoExchange.Net to version 9.0.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added support for Native AOT compilation
    * Added RateLimitUpdated event
    * Added SharedSymbol response property to all Shared interfaces response models returning a symbol name
    * Added GenerateClientOrderId method to Spot Shared client
    * Added IBookTickerRestClient implementation to SpotApi Shared clients
    * Added TriggerPrice property to SharedSpotOrder model
    * Added OptionalExchangeParameters and Supported properties to EndpointOptions
    * Added restClient.SpotApi.Trading.PlaceMultipleOrdersAsync endpoint
    * Added restClient.SpotApi.ExchangeData.GetBookPricesAsync endpoint for single symbol
    * Added QuoteVolume property mapping to SharedSpotTicker model
    * Added All property to retrieve all available environment on MexcEnvironment
    * Refactored Shared clients quantity parameters and responses to use SharedQuantity
    * Updated all IEnumerable response and model types to array response types
    * Removed Newtonsoft.Json dependency
    * Removed legacy ISpotClient implementation
    * Removed legacy AddMexc(restOptions, socketOptions) DI overload
    * Fixed some typos
    * Fixed AveragePrice being return 0 instead of null for Shared order updates

* Version 3.0.0-beta3 - 01 May 2025
    * Updated CryptoExchange.Net version to 9.0.0-beta5
    * Added property to retrieve all available API environments

* Version 3.0.0-beta2 - 23 Apr 2025
    * Updated CryptoExchange.Net to version 9.0.0-beta2
    * Added Shared spot ticker QuoteVolume mapping

* Version 3.0.0-beta1 - 22 Apr 2025
    * Updated CryptoExchange.Net to version 9.0.0-beta1, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added support for Native AOT compilation
    * Added RateLimitUpdated event
    * Added SharedSymbol response property to all Shared interfaces response models returning a symbol name
    * Added GenerateClientOrderId method to Spot Shared client
    * Added IBookTickerRestClient implementation to SpotApi Shared clients
    * Added TriggerPrice property to SharedSpotOrder model
    * Added OptionalExchangeParameters and Supported properties to EndpointOptions
    * Added restClient.SpotApi.Trading.PlaceMultipleOrdersAsync endpoint
    * Added restClient.SpotApi.ExchangeData.GetBookPricesAsync endpoint for single symbol
    * Refactored Shared clients quantity parameters and responses to use SharedQuantity
    * Updated all IEnumerable response and model types to array response types
    * Removed Newtonsoft.Json dependency
    * Removed legacy ISpotClient implementation
    * Removed legacy AddMexc(restOptions, socketOptions) DI overload
    * Fixed restClient.SpotApi.ExchangeData.GetKlinesAsync returning signature error
    * Fixed some typos

* Version 2.1.0 - 11 Feb 2025
    * Updated CryptoExchange.Net to version 8.8.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added support for more SharedKlineInterval values
    * Added setting of DataTime value on websocket DataEvent updates
    * Fix Mono runtime exception on rest client construction using DI

* Version 2.0.0 - 24 Jan 2025
    * Updated CryptoExchange.Net to version 8.7.1, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added client side-ratelimiting implementation
    * Updated json (de)serializer from Newtonsoft.Json to System.Text.Json

* Version 1.15.2 - 22 Jan 2025
    * Added restClient.SpotApi.Account.TransferInternalAsync endpoint
    * Added restClient.SpotApi.Account.GetInternalTransferHistoryAsync endpoint

* Version 1.15.1 - 07 Jan 2025
    * Updated CryptoExchange.Net version
    * Added Type property to MexcExchange class

* Version 1.15.0 - 23 Dec 2024
    * Updated CryptoExchange.Net to version 8.5.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added SetOptions methods on Rest and Socket clients
    * Added setting of DefaultProxyCredentials to CredentialCache.DefaultCredentials on the DI http client
    * Improved websocket disconnect detection

* Version 1.14.1 - 20 Dec 2024
    * Fixed deserialization of too large decimal values in Ticker and Kline models

* Version 1.14.0 - 13 Dec 2024
    * Fixed typo in MexcAggregatedTrade and MexcKline models
    * Fix for restClient.SpotApi.Account.WithdrawAsync deserialization

* Version 1.13.1 - 03 Dec 2024
    * Updated CryptoExchange.Net to version 8.4.3, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Fixed orderbook creation via MexcOrderBookFactory

* Version 1.13.0 - 28 Nov 2024
    * Updated CryptoExchange.Net to version 8.4.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.4.0
    * Added GetFeesAsync Shared REST client implementations
    * Updated MexcOptions to LibraryOptions implementation
    * Updated test and analyzer package versions
    * Fixed some deserialization issues on decimal larger than Decimal.MaxValue on websocket streams

* Version 1.12.0 - 19 Nov 2024
    * Updated CryptoExchange.Net to version 8.3.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.3.0
    * Added support for loading client settings from IConfiguration
    * Added DI registration method for configuring Rest and Socket options at the same time
    * Added DisplayName and ImageUrl properties to MexcExchange class
    * Updated client constructors to accept IOptions from DI
    * Removed redundant MexcSocketClient constructor

* Version 1.11.0 - 06 Nov 2024
    * Updated CryptoExchange.Net to version 8.2.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.2.0

* Version 1.10.0 - 28 Oct 2024
    * Updated CryptoExchange.Net to version 8.1.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.1.0
    * Moved FormatSymbol to MexcExchange class
    * Added support Side setting on SharedTrade model
    * Added MexcTrackerFactory for creating trackers
    * Added overload to Create method on MexcOrderBookFactory support SharedSymbol parameter

* Version 1.9.0 - 22 Oct 2024
    * Added mexcRestClient.SpotApi.Account.GetKycStatusAsync endpoint
    * Added ListenkeyRenewed event to socketClient.SpotApi client so users can react to updated listenkeys for keep-alive caused by reconnecting

* Version 1.8.2 - 14 Oct 2024
    * Updated CryptoExchange.Net to version 8.0.3, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.0.3
    * Fixed TypeLoadException during initialization

* Version 1.8.1 - 14 Oct 2024
    * Fixed cancellation token not being passed to subscribe method in Shared client

* Version 1.8.0 - 27 Sep 2024
    * Updated CryptoExchange.Net to version 8.0.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.0.0
    * Added Shared client interfaces implementation for Spot Rest and Socket clients
    * Added QuoteQuantity property to MexcOrder model
    * Updated KlineInterval Enum values to match number of seconds
    * Updated Sourcelink package version
    * Marked ISpotClient references as deprecated

* Version 1.7.2 - 08 Aug 2024
    * Fixed SpotApi.Account.GetUserAssetsAsync deserialization due to too large number being returned

* Version 1.7.1 - 08 Aug 2024
    * Fixed deserialization issues caused by too big number value

* Version 1.7.0 - 07 Aug 2024
    * Updated CryptoExchange.Net to version 7.11.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/7.11.0
    * Updated XML code comments
    * Fixed deserialization error on SpotApi.ExchangeData.GetTickersAsync due to too large number

* Version 1.6.0 - 27 Jul 2024
    * Updated CryptoExchange.Net to version 7.10.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/7.10.0

* Version 1.5.0 - 16 Jul 2024
    * Updated CryptoExchange.Net to version 7.9.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/7.9.0
    * Updated internal classes to internal access modifier
    * Fixed StartTime and EndTime mapping on MexcStreamKline model

* Version 1.4.1 - 02 Jul 2024
    * Updated CryptoExchange.Net to V7.8.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/7.8.0

* Version 1.4.0 - 23 Jun 2024
    * Updated CryptoExchange.Net to version 7.7.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/7.7.0
    * Added websocket connection ratelimit
    * Updated SpotApi.Account.WithdrawAsync parameters and SpotApi.Account.GetUserAssetsAsync response
    * Updated response models from classes to records

* Version 1.3.0 - 11 Jun 2024
    * Updated CryptoExchange.Net to v7.6.0, see https://github.com/JKorf/CryptoExchange.Net?tab=readme-ov-file#release-notes for release notes

* Version 1.2.5 - 02 Jun 2024
    * Added SpotApi.Account.GetTradeFeeAsync endpoint

* Version 1.2.4 - 07 May 2024
    * Updated CryptoExchange.Net to v7.5.2, see https://github.com/JKorf/CryptoExchange.Net?tab=readme-ov-file#release-notes for release notes

* Version 1.2.3 - 01 May 2024
    * Updated CryptoExchange.Net to v7.5.0, see https://github.com/JKorf/CryptoExchange.Net?tab=readme-ov-file#release-notes for release notes

* Version 1.2.2 - 28 Apr 2024
    * Added MexcExchange static info class
    * Added MexcOrderBookFactory book creation method
    * Fixed MexcOrderBookFactory injection issue
    * Updated CryptoExchange.Net to v7.4.0, see https://github.com/JKorf/CryptoExchange.Net?tab=readme-ov-file#release-notes for release notes

* Version 1.2.1 - 23 Apr 2024
    * Fixed endless looping when authenticated websocket gets disconnected and listenKey is expired
    * Updated CryptoExchange.Net to 7.3.3, see https://github.com/JKorf/CryptoExchange.Net?tab=readme-ov-file#release-notes for release notes

* Version 1.2.0 - 18 Apr 2024
    * Updated CryptoExchange.Net to 7.3.1, see https://github.com/JKorf/CryptoExchange.Net?tab=readme-ov-file#release-notes for release notes
    * Added QuoteVolume property to Ticker model
    * Updated Withdrawal response model
    * Fixed OneWeek KlineInterval serialization

* Version 1.1.1 - 24 Mar 2024
	* Updated CryptoExchange.Net to 7.2.0, see https://github.com/JKorf/CryptoExchange.Net?tab=readme-ov-file#release-notes for release notes

* Version 1.1.0 - 16 Mar 2024
    * Updated CryptoExchange.Net to 7.1.0, see https://github.com/JKorf/CryptoExchange.Net?tab=readme-ov-file#release-notes for release notes
	* Improved error response handling
	
* Version 1.0.1 - 28 Feb 2024
    * Fixed SpotApi.Trading.GetAccountInfoAsync deserialization
    * Renamed OrderType on MexcOrder model to Type
    * Renamed OrderSide on MexcOrderUpdate to Side

* Version 1.0.0 - 25 Feb 2024
    * Initial release
