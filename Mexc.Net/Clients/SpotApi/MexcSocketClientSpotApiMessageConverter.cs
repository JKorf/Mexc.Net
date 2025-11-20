using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Protobuf.Converters.Protobuf;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Sockets;
using LightProto;
using Mexc.Net.Converters;
using Mexc.Net.Enums;
using Mexc.Net.Interfaces.Clients.FuturesApi;
using Mexc.Net.Objects.Models.Futures;
using Mexc.Net.Objects.Models.Protobuf;
using Mexc.Net.Objects.Options;
using Mexc.Net.Objects.Sockets.Models;
using Mexc.Net.Objects.Sockets.Queries;
using Mexc.Net.Objects.Sockets.Subscriptions;
using System.Net.WebSockets;
using System.Text.Json;

namespace Mexc.Net.Clients.FuturesApi
{
    public class MexcSocketClientSpotApiMessageConverter : DynamicJsonConverter
    {
        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(MexcExchange.SerializerContext);

        protected override MessageEvaluator[] MessageEvaluators { get; } = [

            new MessageEvaluator {
                Priority = 1,
                ForceIfFound = true,
                Fields = [
                    new PropertyFieldReference("id"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("id")
            }
        ];
    }


    public abstract class DynamicProtobufConverter<T> : IMessageConverter
    {
        public object Deserialize(ReadOnlySpan<byte> data, Type type)
        {
            var result = Serializer.Deserialize<MexcUpdate>(data);
            return result;
        }

        public abstract string GetMessageIdentifier(ReadOnlySpan<byte> data, WebSocketMessageType? webSocketMessageType);
    }

    public class MexcProtobufMessageConverter : DynamicProtobufConverter
    {
        public MexcProtobufMessageConverter() : base(ProtobufInclude.Model)
        {
        }

        public override string GetMessageIdentifier(ReadOnlySpan<byte> data, WebSocketMessageType? webSocketMessageType)
        {
            var result = _model.Deserialize<SocketEvent>(data);
            return result.c;
        }
    }
}
