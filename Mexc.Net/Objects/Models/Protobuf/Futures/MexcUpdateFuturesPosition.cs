using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Futures;

namespace Mexc.Net.Objects.Models.Protobuf.Futures
{
    [LightProto.ProtoContract]
    internal partial class MexcUpdateFuturesPosition : MexcFuturesUpdate
    {
        [LightProto.ProtoMember(1)]
        public long PositionId { get; set; }
        [LightProto.ProtoMember(2)]
        public string SymbolPosition { get; set; } = string.Empty;
        [LightProto.ProtoMember(3)]
        public int State { get; set; }
        [LightProto.ProtoMember(4)]
        public string? HoldVolume { get; set; }
        [LightProto.ProtoMember(5)]
        public string? FrozenVolume { get; set; }
        [LightProto.ProtoMember(6)]
        public string? CloseVolume { get; set; }
        [LightProto.ProtoMember(7)]
        public string? HoldAveragePrice { get; set; }
        [LightProto.ProtoMember(8)]
        public string? HoldAveragePriceFullyScale { get; set; }
        [LightProto.ProtoMember(9)]
        public string? OpenAveragePrice { get; set; }
        [LightProto.ProtoMember(10)]
        public string? OpenAveragePriceFullyScale { get; set; }
        [LightProto.ProtoMember(11)]
        public string? CloseAveragePrice { get; set; }
        [LightProto.ProtoMember(12)]
        public string? LiquidationPrice { get; set; }
        [LightProto.ProtoMember(13)]
        public string? OriginalInitialMargin { get; set; }
        [LightProto.ProtoMember(14)]
        public string? InitialMargin { get; set; }
        [LightProto.ProtoMember(15)]
        public string? MaintenanceMargin { get; set; }
        [LightProto.ProtoMember(16)]
        public string? HoldFee { get; set; }
        [LightProto.ProtoMember(17)]
        public string? RealisedPnl { get; set; }
        [LightProto.ProtoMember(18)]
        public int? AdlLevel { get; set; }
        [LightProto.ProtoMember(19)]
        public int PositionType { get; set; }
        [LightProto.ProtoMember(20)]
        public int OpenType { get; set; }
        [LightProto.ProtoMember(21)]
        public int? Leverage { get; set; }
        [LightProto.ProtoMember(22)]
        public bool? AutoMargin { get; set; }
        [LightProto.ProtoMember(23)]
        public string Pnl { get; set; } = string.Empty;
        [LightProto.ProtoMember(24)]
        public string? MarginRatio { get; set; }
        [LightProto.ProtoMember(25)]
        public long? CreateTimePosition { get; set; }
        [LightProto.ProtoMember(26)]
        public long? UpdateTime { get; set; }
        [LightProto.ProtoMember(27)]
        public long? Version { get; set; }
        [LightProto.ProtoMember(28)]
        public string? NewOpenAveragePrice { get; set; }
        [LightProto.ProtoMember(29)]
        public string? NewCloseAveragePrice { get; set; }
        [LightProto.ProtoMember(30)]
        public string? CloseProfitAndLoss { get; set; }
        [LightProto.ProtoMember(31)]
        public string? Fee { get; set; }
        [LightProto.ProtoMember(32)]
        public MexcPositionUpdateDeductFee[] DeductFeeList { get; set; } = [];
        [LightProto.ProtoMember(33)]
        public string MakerFeeRate { get; set; } = string.Empty;
        [LightProto.ProtoMember(34)]
        public string TakerFeeRate { get; set; } = string.Empty;

        public MexcPositionUpdate ToModel()
        {
            return new MexcPositionUpdate
            {
                PositionId = PositionId,
                Symbol = SymbolPosition,
                PositionSide = EnumConverter.ParseString<PositionSide>(PositionType.ToString())!.Value,
                MarginType = EnumConverter.ParseString<MarginType>(OpenType.ToString())!.Value,
                Status = EnumConverter.ParseString<PositionStatus>(State.ToString())!.Value,
                PositionSize = ExchangeHelpers.ParseDecimal(HoldVolume) ?? 0,
                FrozenVolume = ExchangeHelpers.ParseDecimal(FrozenVolume) ?? 0,
                CloseVolume = ExchangeHelpers.ParseDecimal(CloseVolume) ?? 0,
                HoldAveragePrice = ExchangeHelpers.ParseDecimal(HoldAveragePrice) ?? 0,
                OpenAveragePrice = ExchangeHelpers.ParseDecimal(OpenAveragePrice) ?? 0,
                CloseAveragePrice = ExchangeHelpers.ParseDecimal(CloseAveragePrice) ?? 0,
                LiquidationPrice = ExchangeHelpers.ParseDecimal(LiquidationPrice) ?? 0,
                OriginalInitialMargin = ExchangeHelpers.ParseDecimal(OriginalInitialMargin) ?? 0,
                InitialMargin = ExchangeHelpers.ParseDecimal(InitialMargin) ?? 0,
                HoldFee = ExchangeHelpers.ParseDecimal(HoldFee) ?? 0,
                RealisedPnl = ExchangeHelpers.ParseDecimal(RealisedPnl) ?? 0,
                Pnl = ExchangeHelpers.ParseDecimal(Pnl),
                AdlLevel = AdlLevel,
                Leverage = Leverage ?? 0,
                MarginRatio = ExchangeHelpers.ParseDecimal(MarginRatio),
                CreateTime = CreateTimePosition == null ? default : DateTimeConverter.ParseFromDecimal(CreateTimePosition.Value),
                UpdateTime = UpdateTime == null ? null : DateTimeConverter.ParseFromDecimal(UpdateTime.Value),
                AutoMargin = AutoMargin ?? false,
                Version = Version ?? 0,
                NewOpenAveragePrice = ExchangeHelpers.ParseDecimal(NewOpenAveragePrice),
                NewCloseAveragePrice = ExchangeHelpers.ParseDecimal(NewCloseAveragePrice),
                CloseProfitAndLoss = ExchangeHelpers.ParseDecimal(CloseProfitAndLoss),
                Fee = ExchangeHelpers.ParseDecimal(Fee),
                HoldAveragePriceFullyScale = ExchangeHelpers.ParseDecimal(HoldAveragePriceFullyScale),
                OpenAveragePriceFullyScale = ExchangeHelpers.ParseDecimal(OpenAveragePriceFullyScale),
                MaintenanceMargin = ExchangeHelpers.ParseDecimal(MaintenanceMargin),
                FeeDeductions = DeductFeeList.Select(x => new MexcPositionFeeDeduction
                {
                    Asset = x.Currency,
                    DeductedFee = ExchangeHelpers.ParseDecimal(x.DeductFee) ?? 0,
                    SettlementFee = ExchangeHelpers.ParseDecimal(x.ConvertSettleFee) ?? 0
                }).ToArray(),
                MakerFeeRate = ExchangeHelpers.ParseDecimal(MakerFeeRate),
                TakerFeeRate = ExchangeHelpers.ParseDecimal(TakerFeeRate)
            };
        }
    }

    [LightProto.ProtoContract]
    internal partial class MexcPositionUpdateDeductFee
    {
        [LightProto.ProtoMember(1)]
        public string Currency { get; set; } = string.Empty;
        [LightProto.ProtoMember(2)]
        public string DeductFee { get; set; } = string.Empty;
        [LightProto.ProtoMember(3)]
        public string ConvertSettleFee { get; set; } = string.Empty;
    }
}
