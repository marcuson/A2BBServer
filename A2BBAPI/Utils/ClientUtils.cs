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
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static TokenResponse GetROClient(string scope, string clientId, string username, string password)
        {
            var disco = DiscoveryClient.GetAsync(Constants.IDENTITY_SERVER_ENDPOINT).Result;
            var client = new TokenClient(disco.TokenEndpoint);
            return client.RequestResourceOwnerPasswordAsync(username, password, scope, new Dictionary<string, string> {
                { "client_id", clientId }
            }).Result;
        }
        #endregion
    }
}
