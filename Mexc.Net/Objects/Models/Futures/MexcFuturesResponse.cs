namespace Mexc.Net.Objects.Models.Futures
{
    internal record MexcFuturesResponse
    {
        [JsonPropertyName("code")]
        public int? Code { get; set; }
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }

    internal record MexcFuturesResponse<T> : MexcFuturesResponse
    {
        [JsonPropertyName("data")]
        public T Data { get; set; } = default!;
    }
}
