using CryptoExchange.Net.Authentication;

namespace Mexc.Net
{
    /// <summary>
    /// Mexc credentials
    /// </summary>
    public class MexcCredentials : ApiCredentials
    {
        /// <summary>
        /// Credential type provided
        /// </summary>
        public ApiCredentialsType CredentialType => CredentialPairs.First().CredentialType;

        /// <summary>
        /// </summary>
        [Obsolete("Parameterless constructor is only for deserialization purposes and should not be used directly. Use parameterized constructor instead.")]
        public MexcCredentials() { }

        /// <summary>
        /// Create credentials using an HMAC key, secret and passphrase
        /// </summary>
        /// <param name="apiKey">The API key</param>
        /// <param name="secret">The API secret</param>
        public MexcCredentials(string apiKey, string secret) : this(new HMACCredential(apiKey, secret)) { }

        /// <summary>
        /// Create Mexc credentials using HMAC credentials
        /// </summary>
        /// <param name="credential">The HMAC credentials</param>
        public MexcCredentials(HMACCredential credential) : base(credential) { }

#if NETSTANDARD2_1_OR_GREATER || NET7_0_OR_GREATER
        /// <summary>
        /// Create Mexc credentials using RSA credentials in PEM format
        /// </summary>
        /// <param name="rsaCredential">RSA credentials</param>
        public MexcCredentials(RSAPemCredential rsaCredential)
            : base(rsaCredential)
        {
        }
#endif
        /// <summary>
        /// Create Mexc credentials using RSA credentials in XML format
        /// </summary>
        /// <param name="rsaCredential">RSA credentials</param>
        public MexcCredentials(RSAXmlCredential rsaCredential)
            : base(rsaCredential)
        {
        }


        /// <inheritdoc />
#pragma warning disable CS0618 // Type or member is obsolete
        public override ApiCredentials Copy() => new MexcCredentials { CredentialPairs = CredentialPairs };
#pragma warning restore CS0618 // Type or member is obsolete
    }
}
