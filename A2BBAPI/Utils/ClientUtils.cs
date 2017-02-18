using A2BBCommon;
using IdentityModel.Client;
using System.Collections.Generic;

namespace A2BBAPI.Utils
{
    /// <summary>
    /// Utilities for REST clients.
    /// </summary>
    public static class ClientUtils
    {
        #region Public static methods
        /// <summary>
        /// Get token for API using username and password.
        /// </summary>
        /// <param name="clientId">The client id.</param>
        /// <param name="scope">The requested scope.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>A token response.</returns>
        public static TokenResponse GetROClient(string scope, string clientId, string username, string password)
        {
            var disco = DiscoveryClient.GetAsync(Constants.IDENTITY_SERVER_ENDPOINT).Result;
            var client = new TokenClient(disco.TokenEndpoint);
            return client.RequestResourceOwnerPasswordAsync(username, password, scope, new Dictionary<string, string> {
                { "client_id", clientId }
            }).Result;
        }

        /// <summary>
        /// Get token for API using client credentials.
        /// </summary>
        /// <param name="clientId">The client id.</param>
        /// <param name="scope">The requested scope.</param>
        /// <param name="secret">The client secret.</param>
        /// <returns>A token response.</returns>
        public static TokenResponse GetCCClient(string scope, string clientId, string secret)
        {
            var disco = DiscoveryClient.GetAsync(Constants.IDENTITY_SERVER_ENDPOINT).Result;
            var client = new TokenClient(disco.TokenEndpoint);
            return client.RequestClientCredentialsAsync(scope, new Dictionary<string, string> {
                { "client_id", clientId },
                { "client_secret", secret}
            }).Result;
        }

        /// <summary>
        /// Get token for API using refresh token.
        /// </summary>
        /// <param name="clientId">The client id.</param>
        /// <param name="scope">The requested scope.</param>
        /// <param name="refreshToken">The refresh token.</param>
        /// <returns>A token response.</returns>
        public static TokenResponse GetRTClient(string scope, string clientId, string refreshToken)
        {
            var disco = DiscoveryClient.GetAsync(Constants.IDENTITY_SERVER_ENDPOINT).Result;
            var client = new TokenClient(disco.TokenEndpoint);
            return client.RequestRefreshTokenAsync(refreshToken, new Dictionary<string, string> {
                { "client_id", clientId }
            }).Result;
        }
        #endregion
    }
}
