using A2BBCommon;
using A2BBCommon.DTO;
using A2BBIdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace A2BBIdentityServer.Controllers
{
    /// <summary>
    /// Controller to deal with user.
    /// </summary>
    [Produces("application/json")]
    [Route("api/me")]
    [Authorize]
    public class MeController : Controller
    {
        #region Private fields
        /// <summary>
        /// The ASP NET identity user manager.
        /// </summary>
        private readonly UserManager<User> _userManager;

        /// <summary>
        /// The ASP NET identity sign in manager.
        /// </summary>
        private readonly SignInManager<User> _signInManager;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger _logger;
        #endregion

        #region Public methods
        /// <summary>
        /// Create a new instance of this class.
        /// </summary>
        /// <param name="userManager">The DI user manager for ASP NET identity.</param>
        /// <param name="signInManager">The DI sign in manager for ASP NET identity.</param>
        /// <param name="loggerFactory">The DI logger factory.</param>
        public MeController(UserManager<User> userManager, SignInManager<User> signInManager, ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AdminUsersController>();
        }

        /// <summary>
        /// List all users.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public User GetInfo()
        {
            return _userManager.Users.FirstOrDefault(u => u.Id == User.Claims.FirstOrDefault(c => c.Type == "sub").Value);
        }

        /// <summary>
        /// Update an user password.
        /// </summary>
        /// <param name="req">The request parameters.</param>
        /// <returns>The response with status.</returns>
        [HttpPut]
        [Route("changepass")]
        public async Task<ResponseWrapper<IdentityResult>> UpdateUserPass([FromBody] ChangePassRequestDTO req)
        {
            User user = _userManager.Users.FirstOrDefault(u => u.Id == User.Claims.FirstOrDefault(c => c.Type == "sub").Value);
            IdentityResult res = await _userManager.ChangePasswordAsync(user, req.OldPassword, req.NewPassword);

            if (!res.Succeeded)
            {
                return new ResponseWrapper<IdentityResult>(res, Constants.RestReturn.ERR_USER_UPDATE);
            }

            return new ResponseWrapper<IdentityResult>(res);
        }
        #endregion
    }
}