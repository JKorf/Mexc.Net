using CryptoExchange.Net.Converters.SystemTextJson.MessageConverters;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using Mexc.Net.Objects.Models.Futures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mexc.Net.Clients.MessageHandlers
{
    internal class MexcRestMessageHandler : JsonRestMessageHandler
    {
        private readonly ErrorMapping _errorMapping;

        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(MexcExchange.SerializerContext);

        public MexcRestMessageHandler(ErrorMapping errorMapping)
        {
            _errorMapping = errorMapping;
        }

        public override Error? CheckDeserializedResponse<T>(HttpResponseHeaders responseHeaders, T result)
        {
            if (result is not MexcFuturesResponse mexcResponse)
                return null;

            if (mexcResponse.Code == 0 || mexcResponse.Code == 200 || mexcResponse.Code == null)
                return null;

            return new ServerError(mexcResponse.Code.Value, _errorMapping.GetErrorInfo(mexcResponse.Code.Value.ToString(), mexcResponse.Message));
        }

        public override async ValueTask<Error> ParseErrorResponse(int httpStatusCode, object? state, HttpResponseHeaders responseHeaders, Stream responseStream)
        {
            if (httpStatusCode == 401)
                return new ServerError(new ErrorInfo(ErrorType.Unauthorized, "Unauthorized"));

            var (parseError, document) = await GetJsonDocument(responseStream, state).ConfigureAwait(false);
            if (parseError != null)
                return parseError;

            int? code = document!.RootElement.TryGetProperty("code", out var codeProp) ? codeProp.GetInt32() : null;
            var msg = document!.RootElement.TryGetProperty("msg", out var msgProp) ? msgProp.GetString() : null;

            return new ServerError(code!.Value, _errorMapping.GetErrorInfo(code.Value.ToString(), msg));
        }
    }
}
