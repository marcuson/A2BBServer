using A2BBCommon;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using static IdentityServer4.IdentityServerConstants;

namespace A2BBIdentityServer
{
    /// <summary>
    /// Identity Server configuration utilities.
    /// </summary>
    public class Config
    {
        #region Public static methods
        /// <summary>
        /// Get API resources.
        /// </summary>
        /// <returns>A list of API resources.</returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource {
                    Name = Constants.A2BB_API_RESOURCE_NAME,
                    DisplayName = "A2BB API",
                    Enabled = true,
                    Scopes =
                    {
                       new Scope
                       {
                            Name = Constants.A2BB_API_RESOURCE_NAME,
                            DisplayName = "A2BB API",
                            Required = true,
                            UserClaims = { JwtClaimTypes.Role, JwtClaimTypes.Name }
                       }
                    },
                    UserClaims = { JwtClaimTypes.Role, JwtClaimTypes.Name }
                }
            };
        }

        /// <summary>
        /// Get possible clients to access protected resources.
        /// </summary>
        /// <returns>A list of possible clients to access resources.</returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = Constants.OAUTH_CLIENT_ID,
                    ClientName = "OAuth2 client",
                    Enabled = true,

                    AccessTokenLifetime = 60,
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedCorsOrigins = { "*" },
                    AllowedScopes = { Constants.A2BB_API_RESOURCE_NAME },
                    RequireClientSecret = false,
                    RequireConsent = false
                }
            };
        }
        #endregion
    }
}
