using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A2BBIdentityServer
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("A2BBAPI", "A2BB API")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "oauth",
                    ClientName = "OAuth2 client",

                    Enabled = true,
                    RequireClientSecret = false,
                    AccessTokenLifetime = 60,

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedCorsOrigins = { "*" },

                    RequireConsent = false,
                    
                    AllowedScopes = { "A2BBAPI" }
                }
            };
        }
    }
}
