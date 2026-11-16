# Mexc.Net examples

Each file is an independent console program. Install `JK.Mexc.Net` in a console project and copy one program to `Program.cs`.

| Program | Workflow |
|---|---|
| [01-spot-quickstart.cs](01-spot-quickstart.cs) | Public ticker, balances, live limit order, query/cancellation |
| [02-futures.cs](02-futures.cs) | Leverage, live order, positions and close workflow |
| [03-websocket.cs](03-websocket.cs) | Public/private streams and cleanup |
| [04-multi-exchange.cs](04-multi-exchange.cs) | Shared API V2 ticker, symbol catalog, and subscription |
| [05-error-handling.cs](05-error-handling.cs) | Results, public-read retries and metadata-based order handling |

Replace key/secret placeholders for intended private operations. Spot/Futures examples change account state when executed; cancellation can race with fills. Read [operational considerations](../../.claude/skills/mexc-net/references/safety.md) first. See [usage snippets](../../.claude/skills/mexc-net/references/usage.md) for focused patterns.

`AiExampleCompileTests` compiles these programs without executing exchange operations.
