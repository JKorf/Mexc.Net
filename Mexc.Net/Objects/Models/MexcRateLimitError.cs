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
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        public MexcRateLimitError(int? code, string message, Exception? exception) : base(code, message, exception)
        {
        }
    }
}
