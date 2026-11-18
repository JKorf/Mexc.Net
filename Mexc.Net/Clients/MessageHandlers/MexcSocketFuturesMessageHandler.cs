using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson.MessageHandlers;
using LightProto;
using Mexc.Net.Objects.Models.Protobuf;
using Mexc.Net.Objects.Sockets.Models;
using System.Net.WebSockets;
using System.Text.Json;
using MexcFuturesUpdate = Mexc.Net.Objects.Models.Protobuf.Futures.MexcFuturesUpdate;

namespace Mexc.Net.Clients.MessageHandlers
{
    internal class MexcSocketFuturesMessageHandler : JsonSocketMessageHandler
    {
        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(MexcExchange._serializerContext);

        public MexcSocketFuturesMessageHandler()
        {
            AddTopicMapping<MexcFuturesUpdate>(x => x.Symbol);
        }

        protected override MessageTypeDefinition[] TypeEvaluators { get; } = [

            new MessageTypeDefinition {
                ForceIfFound = true,
                Fields = [
                    new PropertyFieldReference("channel"),
                ],
                TypeIdentifierCallback = x => x.FieldValue("channel")!
            }
        ];
    }

    internal class MexcFuturesProtobufMessageHandler : ISocketMessageHandler
    {
        public object Deserialize(ReadOnlySpan<byte> data, Type type)
        {
            return Serializer.Deserialize<MexcFuturesUpdate>(data, MexcFuturesUpdate.ProtoReader);
        }

        public string? GetTopicFilter(object deserializedObject)
        {
            var update = ((MexcFuturesUpdate)deserializedObject);
            if (string.IsNullOrEmpty(update.Symbol))
                return $"{update.Channel}";

            return $"{update.Channel}.{update.Symbol}";
        }

        public string? GetTypeIdentifier(ReadOnlySpan<byte> data, WebSocketMessageType? webSocketMessageType) => "pb";
    }
}
