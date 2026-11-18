using Mexc.Net.Objects.Models.Futures;

namespace Mexc.Net.Objects.Models.Protobuf.Futures
{
    [LightProto.ProtoContract]
    internal partial class MexcUpdateFuturesReversePosition : MexcFuturesUpdate
    {
        [LightProto.ProtoMember(1)]
        public int ContractId { get; set; }
        [LightProto.ProtoMember(2)]
        public long PositionId { get; set; }
        [LightProto.ProtoMember(3)]
        public int State { get; set; }
        [LightProto.ProtoMember(4)]
        public int? ErrorCode { get; set; }
        [LightProto.ProtoMember(5)]
        public Dictionary<string, string> Extend { get; set; } = new Dictionary<string, string>();

        public MexcReversePositionUpdate ToModel()
        {
            return new MexcReversePositionUpdate
            {
                ContractId = ContractId,
                PositionId = PositionId,
                Status = State,
                ErrorCode = ErrorCode ?? 0
            };
        }
    }
}
