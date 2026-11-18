using Mexc.Net.Objects.Models.Futures;

namespace Mexc.Net.Objects.Models.Protobuf.Futures
{
    [LightProto.ProtoContract]
    internal partial class MexcUpdateFuturesChaseOrderFailure : MexcFuturesUpdate
    {
        [LightProto.ProtoMember(1)]
        public int ErrorCode { get; set; }
        [LightProto.ProtoMember(2)]
        public string SymbolOrder { get; set; } = string.Empty;

        public MexcChaseOrderFailure ToModel()
        {
            return new MexcChaseOrderFailure
            {
                ErrorCode = ErrorCode,
                Symbol = SymbolOrder
            };
        }
    }
}
