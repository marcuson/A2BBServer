using A2BBCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace A2BBAPI.Utils
{
    public static class ClaimsUtils
    {
        #region Nested classes
        /// <summary>
        /// Holder for authorized user claims.
        /// </summary>
        public class ClaimsHolder
        {
            /// <summary>
            /// The name claim.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// The subject claim.
            /// </summary>
            public string Sub { get; set; }
        }
        #endregion

        #region Public static methods
        /// <summary>
        /// Validate principal for identity server call.
        /// </summary>
        /// <returns>An holder class with useful user claims.</returns>
        public static ClaimsHolder ValidateUserClaimForIdSrvCall(ClaimsPrincipal principal)
        {
            var subClaim = principal.Claims.FirstOrDefault(c => c.Type == "sub");
            if (subClaim == null || String.IsNullOrWhiteSpace(subClaim.Value))
            {
                throw new RestReturnException(Constants.RestReturn.ERR_INVALID_SUB_CLAIM);
            }

            var name = principal.Identity.Name;
            if (name == null || String.IsNullOrWhiteSpace(name))
            {
                throw new RestReturnException(Constants.RestReturn.ERR_INVALID_NAME_CLAIM);
            }

            return new ClaimsHolder { Name = name, Sub = subClaim.Value };
        }
        #endregion
    }
}
