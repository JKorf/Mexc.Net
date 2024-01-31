---
title: Getting started
nav_order: 2
---

## Creating client
There are 2 clients available to interact with the Bitget API, the `MexcRestClient` and `MexcSocketClient`. They can be created manually on the fly or be added to the dotnet DI using the `AddMexc` extension method.

*Manually create a new client*
```csharp
var mexcRestClient = new MexcRestClient(options
{
    // Set options here for this client
});

var mexcSocketClient = new MexcSocketClient(options =>
{
    // Set options here for this client
});
```

*Using dotnet dependency inject*
```csharp
services.AddMexc(
    restOptions => {
        // set options for the rest client
    },
    socketClientOptions => {
        // set options for the socket client
    }); 
    
// IMexcRestClient, IMexcSocketClient and IMexcOrderBookFactory, as well as an implementation of the ISpotClient interface for Mexc are now available for injecting
```

Different options are available to set on the clients, see this example
```csharp
var mexcRestClient = new MexcRestClient(options =>
{
    options.ApiCredentials = new ApiCredentials("API-KEY", "API-SECRET");
    options.RequestTimeout = TimeSpan.FromSeconds(60);
});
```
Alternatively, options can be provided before creating clients by using `SetDefaultOptions` or during the registration in the DI container:  
```csharp
MexcRestClient.SetDefaultOptions(options => {
    // Set options here for all new clients
});
var mexcRestClient = new MexcRestClient();
```
More info on the specific options can be found in the [CryptoExchange.Net documentation](https://jkorf.github.io/CryptoExchange.Net/Options.html)
