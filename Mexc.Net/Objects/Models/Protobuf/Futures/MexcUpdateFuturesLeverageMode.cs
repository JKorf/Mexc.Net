using Mexc.Net.Objects.Models.Futures;

namespace Mexc.Net.Objects.Models.Protobuf.Futures
{
    [LightProto.ProtoContract]
    internal partial class MexcUpdateFuturesLeverageMode : MexcFuturesUpdate
    {
        [LightProto.ProtoMember(1)]
        public int? PositionMode { get; set; }

        public MexcLeverageModeUpdate ToModel()
        {
            return new MexcLeverageModeUpdate
            {
                PositionMode = PositionMode ?? 0
            };
        }
    }
}
