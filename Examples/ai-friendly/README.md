# AI-Friendly Examples

These examples are optimized for AI coding assistants and quick onboarding. Each file is:

- **Compilable** - drop into a console project with `dotnet add package JK.Mexc.Net` and it builds.
- **Self-contained** - single file, no external setup, no shared helpers.
- **Heavily commented** - explains why the pattern matters, not just what it does.
- **Idiomatic** - follows current Mexc.Net 5.x patterns.

## Files

| File | What it shows |
|---|---|
| `01-spot-quickstart.cs` | Client setup, public ticker, authenticated account balance, place limit order, query order status |
| `02-futures.cs` | Futures symbols, leverage, market order, get position, close position |
| `03-websocket.cs` | Spot ticker and klines, spot listen-key private streams, futures private stream pattern, teardown |
| `04-multi-exchange.cs` | `CryptoExchange.Net.SharedApis` pattern for exchange-agnostic code |
| `05-error-handling.cs` | `HttpResult` patterns, retry, common error scenarios |

## Running

```bash
dotnet new console -n MyMexcApp
cd MyMexcApp
dotnet add package JK.Mexc.Net
# Copy the example .cs file content into Program.cs
# Replace API_KEY / API_SECRET placeholders with your own
dotnet run
```
