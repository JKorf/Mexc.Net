using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Sockets;
using Mexc.Net.Enums;
using Mexc.Net.Interfaces.Clients.FuturesApi;
using Mexc.Net.Objects.Models.Futures;
using Mexc.Net.Objects.Options;
using Mexc.Net.Objects.Sockets.Models;
using Mexc.Net.Objects.Sockets.Queries;
using Mexc.Net.Objects.Sockets.Subscriptions;
using System.Net.WebSockets;
using System.Text.Json;

namespace Mexc.Net.Clients.FuturesApi
{
    public class MexcSocketClientFuturesApiMessageConverter : DynamicJsonConverter
    {
        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(MexcExchange.SerializerContext);

        protected override MessageEvaluator[] MessageEvaluators { get; } = [

            new MessageEvaluator {
                Priority = 1,
                Fields = [
                    new PropertyFieldReference("channel"),
                    new PropertyFieldReference("symbol"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("channel") + x.FieldValue("symbol")
            },

            new MessageEvaluator {
                Priority = 2,
                Fields = [
                    new PropertyFieldReference("symbol"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("symbol")
            }
        ];

        //public override string? GetMessageIdentifier(ReadOnlySpan<byte> data, WebSocketMessageType? webSocketMessageType)
        //{
        //    var reader = new Utf8JsonReader(data);
        //    string? channel = null;
        //    string? symbol = null;
        //    while (reader.Read())
        //    {
        //        if (reader.TokenType == JsonTokenType.PropertyName)
        //        {
        //            if (reader.CurrentDepth == 1 && reader.ValueTextEquals("channel"))
        //            {
        //                reader.Read();

        //                if (symbol != null)
        //                    return new MessageInfo { Identifier = reader.GetString() + symbol };
        //                else
        //                    channel = reader.GetString();
        //            }

        //            if (reader.CurrentDepth == 1 && reader.ValueTextEquals("symbol"))
        //            {
        //                reader.Read();

        //                if (channel != null)
        //                    return new MessageInfo { Identifier = channel + reader.GetString() };
        //                else
        //                    symbol = reader.GetString();
        //            }
        //        }
        //    }

        //    return new MessageInfo() { Identifier = channel };
        //}
    }
}
