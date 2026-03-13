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

        internal string? Passphrase =>
            CredentialType == ApiCredentialsType.Hmac ? GetCredential<HMACCredential>()?.Pass
            : CredentialType == ApiCredentialsType.Rsa ? GetCredential<RSACredential>()?.Passphrase
            : null;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="apiKey">The API key</param>
        /// <param name="secret">The API secret</param>
        public MexcCredentials(string apiKey, string secret) : this(new HMACCredential(apiKey, secret)) { }

        /// <summary>
        /// Create Bitget credentials using HMAC credentials
        /// </summary>
        /// <param name="credential">The HMAC credentials</param>
        public MexcCredentials(HMACCredential credential) : base(credential) { }

        /// <summary>
        /// Create Bitget credentials using RSA credentials
        /// </summary>
        /// <param name="rsaCredential">RSA credentials</param>
        public MexcCredentials(RSACredential rsaCredential)
            : base(rsaCredential)
        {
        }

        /// <inheritdoc />
        public override ApiCredentials Copy() =>
            CredentialType switch
            {
                ApiCredentialsType.Hmac => new MexcCredentials(GetCredential<HMACCredential>()!),
                ApiCredentialsType.Rsa => new MexcCredentials(GetCredential<RSACredential>()!)
            };
    }
}
