using CryptoExchange.Net.Objects.Errors;

namespace Mexc.Net.Objects.Models
{
    /// <summary>
    /// Mexc rate limit error
    /// </summary>
    public class MexcRateLimitError : ServerRateLimitError
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="message"></param>
        public MexcRateLimitError(string message) : base(message)
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        public MexcRateLimitError(int code, string message) : base(_errorInfo with { ErrorCodes = [code.ToString()], Message = _errorInfo.Message + ": " + message }, null)
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        protected MexcRateLimitError(ErrorInfo info, Exception? exception) : base(info, exception) { }
    }
}
