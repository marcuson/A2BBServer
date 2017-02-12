using A2BBCommon.Models;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace A2BBIdentityServer
{
    /// <summary>
    /// Class used to populate profile data on authentication request/response.
    /// </summary>
    public class AspNetCoreIdentityProfileService : IProfileService
    {
        #region Private fields
        /// <summary>
        /// The DI ASP.NET Core Identity user manager.
        /// </summary>
        private readonly UserManager<User> _userManager;
        #endregion

        #region Public methods
        /// <summary>
        /// Create a new instance of this class.
        /// </summary>
        /// <param name="userManager">The DI ASP.NET Core Identity user manager.</param>
        public AspNetCoreIdentityProfileService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// This method is called whenever claims about the user are requested (e.g. during token creation or via the userinfo endpoint)
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            if (context.RequestedClaimTypes.Any())
            {
                context.AddFilteredClaims(context.Subject.Claims);
            }

            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            var roles = await _userManager.GetRolesAsync(user);

            context.AddFilteredClaims(roles.Select(r => new Claim(JwtClaimTypes.Role, r)));
            context.AddFilteredClaims(
                new List<Claim> { new Claim(JwtClaimTypes.Name, user.UserName) });
        }

        /// <summary>
        /// This method gets called whenever identity server needs to determine if the user is valid or active (e.g. if the user's account has been deactivated since they logged in).
        /// (e.g. during token issuance or validation).
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
        #endregion
    }
}
