using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Futures;

namespace Mexc.Net.Objects.Models.Protobuf.Futures
{
    [LightProto.ProtoContract]
    internal partial class MexcUpdateFuturesIndexPrice : MexcFuturesUpdate
    {
        [LightProto.ProtoMember(1)]
        public string SymbolRate { get; set; } = string.Empty;
        [LightProto.ProtoMember(2)]
        public string Price { get; set; } = string.Empty;

        public MexcPriceUpdate ToModel()
        {
            return new MexcPriceUpdate
            {
                Symbol = SymbolRate,
                Price = ExchangeHelpers.ParseDecimal(Price) ?? 0,
            };
        }
    }
}
