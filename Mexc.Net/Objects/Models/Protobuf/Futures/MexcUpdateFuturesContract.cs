using Mexc.Net.Enums;
using Mexc.Net.Objects.Models.Futures;

namespace Mexc.Net.Objects.Models.Protobuf.Futures
{
    [LightProto.ProtoContract]
    internal partial class MexcUpdateFuturesContract : MexcFuturesUpdate
    {
        [LightProto.ProtoMember(1)]
        public string SymbolTicker { get; set; } = string.Empty;

        [LightProto.ProtoMember(2)]
        public string DisplayName { get; set; } = string.Empty;
        [LightProto.ProtoMember(3)]
        public string DisplayNameEnglish { get; set; } = string.Empty;
        [LightProto.ProtoMember(4)]
        public int PositionOpenType { get; set; }
        [LightProto.ProtoMember(5)]
        public string BaseAsset { get; set; } = string.Empty;
        [LightProto.ProtoMember(7)]
        public string BaseAssetName { get; set; } = string.Empty;
        [LightProto.ProtoMember(6)]
        public string QuoteAsset { get; set; } = string.Empty;
        [LightProto.ProtoMember(8)]
        public string QuoteAssetName { get; set; } = string.Empty;
        [LightProto.ProtoMember(9)]
        public int FuturesType { get; set; }
        [LightProto.ProtoMember(10)]
        public string SettleAsset { get; set; } = string.Empty;
        [LightProto.ProtoMember(11)]
        public string ContractSize { get; set; } = string.Empty;
        [LightProto.ProtoMember(12)]
        public string MinLeverage { get; set; } = string.Empty;
        [LightProto.ProtoMember(13)]
        public string MaxLeverage { get; set; } = string.Empty;
        [LightProto.ProtoMember(14)]
        public string PriceScale { get; set; } = string.Empty;
        [LightProto.ProtoMember(15)]
        public string VolumeScale { get; set; } = string.Empty;
        [LightProto.ProtoMember(16)]
        public string QuantityScale { get; set; } = string.Empty;
        [LightProto.ProtoMember(17)]
        public string PriceUnit { get; set; } = string.Empty;
        [LightProto.ProtoMember(18)]
        public string VolumeUnit { get; set; } = string.Empty;
        [LightProto.ProtoMember(19)]
        public string MinQuantity { get; set; } = string.Empty;
        [LightProto.ProtoMember(20)]
        public string MaxQuantity { get; set; } = string.Empty;
        [LightProto.ProtoMember(21)]
        public string MaxOrderQuantityLimit { get; set; } = string.Empty;
        [LightProto.ProtoMember(22)]
        public string BidLimitPriceRate { get; set; } = string.Empty;
        [LightProto.ProtoMember(23)]
        public string AskLimitPriceRate { get; set; } = string.Empty;
        [LightProto.ProtoMember(24)]
        public string TakerFeeRate { get; set; } = string.Empty;
        [LightProto.ProtoMember(25)]
        public string MakerFeeRate { get; set; } = string.Empty;
        [LightProto.ProtoMember(26)]
        public string MaintenanceMarginRate { get; set; } = string.Empty;
        [LightProto.ProtoMember(27)]
        public string InitialMarginRate { get; set; } = string.Empty;
        [LightProto.ProtoMember(28)]
        public string RiskBaseVolume { get; set; } = string.Empty;
        [LightProto.ProtoMember(29)]
        public string RiskIncreaseVolume { get; set; } = string.Empty;
        [LightProto.ProtoMember(30)]
        public string RiskIncreaseMmr { get; set; } = string.Empty;
        [LightProto.ProtoMember(31)]
        public string RiskIncreaseImr { get; set; } = string.Empty;
        [LightProto.ProtoMember(32)]
        public string RiskLevelLimit { get; set; } = string.Empty;
        [LightProto.ProtoMember(33)]
        public string PriceCoefficientVariation { get; set; } = string.Empty;
        [LightProto.ProtoMember(34)]
        public string[] IndexOrigin { get; set; } = [];
        [LightProto.ProtoMember(35)]
        public int ContractStatus { get; set; }
        [LightProto.ProtoMember(36)]
        public bool IsNew { get; set; }
        [LightProto.ProtoMember(37)]
        public bool IsHot { get; set; }
        [LightProto.ProtoMember(38)]
        public bool IsHidden { get; set; }
        [LightProto.ProtoMember(39)]
        public string[] ConceptPlate { get; set; } = [];
        [LightProto.ProtoMember(40)]
        public int RiskLimitType { get; set; }
        [LightProto.ProtoMember(41)]
        public string TriggerProtect { get; set; } = string.Empty;
        [LightProto.ProtoMember(42)]
        public int[] MaxNumberOfOrders { get; set; } = [];
        [LightProto.ProtoMember(43)]
        public string[] DepthStepList { get; set; } = [];
        [LightProto.ProtoMember(44)]
        public int RiskLongShortSwitch { get; set; }
        [LightProto.ProtoMember(49)]
        public long? OpenTime { get; set; }
        [LightProto.ProtoMember(50)]
        public int OpeningCountdownOption { get; set; }
        [LightProto.ProtoMember(51)]
        public long[] ConceptPlateId { get; set; } = [];
        [LightProto.ProtoMember(52)]
        public string? RiskLimitRate { get; set; }
        [LightProto.ProtoMember(53)]
        public MexcUpdateFuturesRiskLimit[] CustomRiskLimits { get; set; } = [];
        [LightProto.ProtoMember(54)]
        public string LiquidationFeeRate { get; set; } = string.Empty;
        [LightProto.ProtoMember(55)]
        public int FeeRateMode { get; set; }

        public MexcContract ToModel()
        {
            return new MexcContract
            {
                Symbol = SymbolTicker,
                AskLimitPriceRate = ExchangeHelpers.ParseDecimal(AskLimitPriceRate) ?? 0,
                BaseAsset = BaseAsset,
                BaseAssetName = BaseAssetName,
                BidLimitPriceRate = ExchangeHelpers.ParseDecimal(BidLimitPriceRate) ?? 0,
                ContractSize = ExchangeHelpers.ParseDecimal(ContractSize) ?? 0,
                ContractStatus = EnumConverter.ParseString<ContractStatus>(ContractStatus.ToString())!.Value,
                DisplayName = DisplayName,
                DisplayNameEnglish = DisplayNameEnglish,
                FuturesType = FuturesType,
                InitialMarginRate = ExchangeHelpers.ParseDecimal(InitialMarginRate) ?? 0,
                IsHidden = IsHidden,
                IsHot = IsHot,
                IsNew = IsNew,
                MakerFeeRate = ExchangeHelpers.ParseDecimal(MakerFeeRate) ?? 0,
                MaintenanceMarginRate = ExchangeHelpers.ParseDecimal(MaintenanceMarginRate) ?? 0,
                MaxLeverage = ExchangeHelpers.ParseDecimal(MaxLeverage) ?? 0,
                MaxOrderQuantityLimit = ExchangeHelpers.ParseDecimal(MaxOrderQuantityLimit) ?? 0,
                MaxQuantity = ExchangeHelpers.ParseDecimal(MaxQuantity) ?? 0,
                MinLeverage = ExchangeHelpers.ParseDecimal(MinLeverage) ?? 0,
                MinQuantity = ExchangeHelpers.ParseDecimal(MinQuantity) ?? 0,
                PositionOpenType = EnumConverter.ParseString<PositionOpenType>(PositionOpenType.ToString())!.Value,
                PriceCoefficientVariation = ExchangeHelpers.ParseDecimal(PriceCoefficientVariation) ?? 0,
                PriceScale = ExchangeHelpers.ParseDecimal(PriceScale) ?? 0,
                PriceUnit = ExchangeHelpers.ParseDecimal(PriceUnit) ?? 0,
                QuoteAsset = QuoteAsset,
                QuoteAssetName = QuoteAssetName,
                RiskBaseVolume = ExchangeHelpers.ParseDecimal(RiskBaseVolume) ?? 0,
                RiskIncreaseImr = ExchangeHelpers.ParseDecimal(RiskIncreaseImr) ?? 0,
                RiskIncreaseMmr = ExchangeHelpers.ParseDecimal(RiskIncreaseMmr) ?? 0,
                RiskIncreaseVolume = ExchangeHelpers.ParseDecimal(RiskIncreaseVolume) ?? 0,
                RiskLevelLimit = ExchangeHelpers.ParseDecimal(RiskLevelLimit) ?? 0,
                RiskLimitRate = ExchangeHelpers.ParseDecimal(RiskLimitRate) ?? 0,
                RiskLimitType = EnumConverter.ParseString<RiskLimitType>(RiskLimitType.ToString())!.Value,
                RiskLongShortSwitch = RiskLongShortSwitch == 1,
                SettleAsset = SettleAsset,
                TakerFeeRate = ExchangeHelpers.ParseDecimal(TakerFeeRate) ?? 0,
                VolumeScale = ExchangeHelpers.ParseDecimal(VolumeScale) ?? 0,
                VolumeUnit = ExchangeHelpers.ParseDecimal(VolumeUnit) ?? 0,
                IndexOrigin = IndexOrigin,
                ConceptPlate = ConceptPlate,
                ConceptPlateId = ConceptPlateId,
                CustomRiskLimits = CustomRiskLimits.Select(x => new MexcCustomRiskLimit
                {
                    Level = x.Level,
                    InitialMarginRate = ExchangeHelpers.ParseDecimal(x.Imr) ?? 0,
                    MaxLeverage = int.Parse(x.MaxLeverage),
                    MaxVolume = ExchangeHelpers.ParseDecimal(x.MaxVolume) ?? 0,
                    MaintenanceMarginRate = ExchangeHelpers.ParseDecimal(x.Mmr) ?? 0,
                }).ToArray(),
                OpenTime = OpenTime.HasValue ? DateTimeConverter.ConvertFromMilliseconds(OpenTime.Value) : null,
                OpeningCountdownOption = OpeningCountdownOption,
                DepthStepList = DepthStepList,
                //FeeRateMode = FeeRateMode,
                LiquidationFeeRate = ExchangeHelpers.ParseDecimal(LiquidationFeeRate) ?? 0,
                MaxNumberOfOrders = MaxNumberOfOrders,
                QuantityScale = ExchangeHelpers.ParseDecimal(QuantityScale) ??0,
                //RiskLimitMode = RiskLimitType,
            };
        }
    }

    [LightProto.ProtoContract]
    internal partial class MexcUpdateFuturesRiskLimit
    {
        [LightProto.ProtoMember(1)]
        public int Level { get; set; }
        [LightProto.ProtoMember(2)]
        public string MaxVolume { get; set; } = string.Empty;
        [LightProto.ProtoMember(3)]
        public string Mmr { get; set; } = string.Empty;
        [LightProto.ProtoMember(4)]
        public string Imr { get; set; } = string.Empty;
        [LightProto.ProtoMember(5)]
        public string MaxLeverage { get; set; } = string.Empty;
    }
}
