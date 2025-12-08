using Mexc.Net.Objects.Models.Spot;

namespace Mexc.Net.Objects.Models.Protobuf
{
    [LightProto.ProtoContract]
    [ProtoBuf.ProtoContract]
    internal partial class MexcUpdateAccount : MexcUpdate
    {
        /// <summary>
        /// Asset
        /// </summary>
        [ProtoBuf.ProtoMember(1)]
        [LightProto.ProtoMember(1)]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [ProtoBuf.ProtoMember(8)]
        [LightProto.ProtoMember(8)]
        public long Timestamp { get; set; }
        /// <summary>
        /// New free quantity
        /// </summary>
        [ProtoBuf.ProtoMember(3)]
        [LightProto.ProtoMember(3)]
        public string Free { get; set; } = string.Empty;
        /// <summary>
        /// Changed free quantity
        /// </summary>
        [ProtoBuf.ProtoMember(4)]
        [LightProto.ProtoMember(4)]
        public string FreeChange { get; set; } = string.Empty;
        /// <summary>
        /// New frozen quantity
        /// </summary>
        [ProtoBuf.ProtoMember(5)]
        [LightProto.ProtoMember(5)]
        public string Frozen { get; set; } = string.Empty;
        /// <summary>
        /// Changed frozen quantity
        /// </summary>
        [ProtoBuf.ProtoMember(6)]
        [LightProto.ProtoMember(6)]
        public string FrozenChange { get; set; } = string.Empty;
        /// <summary>
        /// Trigger update type
        /// </summary>
        [ProtoBuf.ProtoMember(7)]
        [LightProto.ProtoMember(7)]
        public string UpdateType { get; set; } = string.Empty;

        public MexcAccountUpdate ToModel()
        {
            return new MexcAccountUpdate
            {
                Asset = Asset,
                Timestamp = DateTimeConverter.ParseFromDouble(Timestamp),
                Free = ExchangeHelpers.ParseDecimal(Free) ?? 0,
                FreeChange = ExchangeHelpers.ParseDecimal(FreeChange) ?? 0,
                Frozen = ExchangeHelpers.ParseDecimal(Frozen) ?? 0,
                FrozenChange = ExchangeHelpers.ParseDecimal(FrozenChange) ?? 0,
                UpdateType = UpdateType
            };
        }
    }
}
