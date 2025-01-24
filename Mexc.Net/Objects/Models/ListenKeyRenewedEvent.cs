namespace Mexc.Net.Objects.Models
{
    /// <summary>
    /// Listen key has been renewed by the client
    /// </summary>
    public class ListenKeyRenewedEvent
    {
        /// <summary>
        /// The previous listen key
        /// </summary>
        public string OldKey { get; set; }
        /// <summary>
        /// The new listen key
        /// </summary>
        public string NewKey { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        public ListenKeyRenewedEvent(string oldKey, string newKey)
        {
            OldKey = oldKey;
            NewKey = newKey;
        }
    }
}
