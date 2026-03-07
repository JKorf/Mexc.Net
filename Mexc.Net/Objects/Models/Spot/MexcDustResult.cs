namespace Mexc.Net.Objects.Models.Spot
{
    /// <summary>
    /// Dust transfer result
    /// </summary>
    [SerializationModel]
    public record MexcDustResult
    {
        /// <summary>
        /// ["<c>successList</c>"] Successfully converted
        /// </summary>
        [JsonPropertyName("successList")]
        public string[] Successful { get; set; } = Array.Empty<string>();
        /// <summary>
        /// ["<c>failedList</c>"] Failed to convert
        /// </summary>
        [JsonPropertyName("failedList")]
        public MexcFailedDust[] Failed { get; set; } = Array.Empty<MexcFailedDust>();
        /// <summary>
        /// ["<c>totalConvert</c>"] Total converted
        /// </summary>
        [JsonPropertyName("totalConvert")]
        public decimal TotalConverted { get; set; }
        /// <summary>
        /// ["<c>convertFee</c>"] Convert fee
        /// </summary>
        [JsonPropertyName("convertFee")]
        public decimal ConvertFee { get; set; }
    }

    /// <summary>
    /// Failed dust asset
    /// </summary>
    [SerializationModel]
    public record MexcFailedDust
    {
        /// <summary>
        /// ["<c>asset</c>"] Asset name
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>message</c>"] Message
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>code</c>"] Code
        /// </summary>
        [JsonPropertyName("code")]
        public int Code { get; set; }
    }
}
