using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson.MessageHandlers;
using Mexc.Net.Objects.Sockets.Models;
using System.Text.Json;

namespace Mexc.Net.Clients.MessageHandlers
{
    internal class MexcSocketFuturesMessageHandler : JsonSocketMessageHandler
    {
        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(MexcExchange.SerializerContext);

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
}
