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
        /// Get identity resources.
        /// </summary>
        /// <returns>The identity resources.</returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

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
                },
                new ApiResource {
                    Name = Constants.A2BB_IDSRV_RESOURCE_NAME,
                    DisplayName = "IDSRV API",
                    Enabled = true,
                    Scopes =
                    {
                       new Scope
                       {
                            Name = Constants.A2BB_IDSRV_RESOURCE_NAME,
                            DisplayName = "IDSRV API",
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
                    ClientId = Constants.A2BB_IDSRV_RO_CLIENT_ID,
                    ClientName = "IdSrv client",
                    Enabled = true,

                    AccessTokenLifetime = 60,
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedCorsOrigins = { "*", "http://localhost:4200" },
                    AllowOfflineAccess = true,
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        Constants.A2BB_IDSRV_RESOURCE_NAME },
                    RequireClientSecret = false,
                    RequireConsent = false,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    SlidingRefreshTokenLifetime = 60 * 60
                },
                new Client
                {
                    ClientId = Constants.A2BB_API_CLIENT_ID,
                    ClientName = "A2BB client",
                    ClientSecrets =
                    {
                        new Secret(Constants.A2BB_API_CLIENT_SECRET.Sha256())
                    },
                    Enabled = true,

                    AccessTokenLifetime = 60,
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedCorsOrigins = { "*" },
                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.ReUse,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    SlidingRefreshTokenLifetime = int.MaxValue,
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        Constants.A2BB_API_RESOURCE_NAME },
                    RequireConsent = false
                }
            };
        }
        #endregion
    }
}
