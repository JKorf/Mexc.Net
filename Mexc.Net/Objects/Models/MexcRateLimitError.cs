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
        public MexcRateLimitError(int? code, string message, Exception? exception) : base(code, message, exception)
        {
        }
    }
}
