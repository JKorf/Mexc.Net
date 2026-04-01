using CryptoExchange.Net.Trackers.UserData.Interfaces;
using CryptoExchange.Net.Trackers.UserData.Objects;

namespace Mexc.Net.Interfaces
{
    /// <summary>
    /// Tracker factory
    /// </summary>
    public interface IMexcTrackerFactory : ITrackerFactory
    {
        /// <summary>
        /// Create a new Spot user data tracker
        /// </summary>
        /// <param name="userIdentifier">User identifier</param>
        /// <param name="config">Configuration</param>
        /// <param name="credentials">Credentials</param>
        /// <param name="environment">Environment</param>
        IUserSpotDataTracker CreateUserSpotDataTracker(string userIdentifier, MexcCredentials credentials, SpotUserDataTrackerConfig? config = null, MexcEnvironment? environment = null);
        
        /// <summary>
        /// Create a new spot user data tracker
        /// </summary>
        /// <param name="config">Configuration</param>
        IUserSpotDataTracker CreateUserSpotDataTracker(SpotUserDataTrackerConfig? config = null);
        
        /// <summary>
        /// Create a new Futures user data tracker
        /// </summary>
        /// <param name="userIdentifier">User identifier</param>
        /// <param name="config">Configuration</param>
        /// <param name="credentials">Credentials</param>
        /// <param name="environment">Environment</param>
        IUserFuturesDataTracker CreateUserFuturesDataTracker(string userIdentifier, MexcCredentials credentials, FuturesUserDataTrackerConfig? config = null, MexcEnvironment? environment = null);
        
        /// <summary>
        /// Create a new Futures user data tracker
        /// </summary>
        /// <param name="config">Configuration</param>
        IUserFuturesDataTracker CreateUserFuturesDataTracker(FuturesUserDataTrackerConfig? config = null);
    }
}
