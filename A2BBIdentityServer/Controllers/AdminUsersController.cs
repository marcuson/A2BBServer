using A2BBCommon;
using A2BBCommon.Models;
using A2BBIdentityServer.DTO;
using A2BBIdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace A2BBIdentityServer.Controllers
{
    /// <summary>
    /// Controller to deal with user via admin credentials.
    /// </summary>
    [Produces("application/json")]
    [Route("api/admin/users")]
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : Controller
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
        public AdminUsersController(UserManager<User> userManager, SignInManager<User> signInManager, ILoggerFactory loggerFactory)
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
        public IEnumerable<User> ListUsers()
        {
            return _userManager.Users;
        }

        /// <summary>
        /// Delete an user.
        /// </summary>
        /// <param name="userId">The id of the user to delete.</param>
        /// <returns>The response with status.</returns>
        [HttpDelete]
        [Route("{userId}")]
        public async Task<ResponseWrapper<IdentityResult>> DeleteUser([FromRoute] string userId)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return new ResponseWrapper<IdentityResult>(Constants.RestReturn.ERR_USER_NOT_FOUND);
            }

            IdentityResult res = await _userManager.DeleteAsync(user);

            if (!res.Succeeded)
            {
                return new ResponseWrapper<IdentityResult>(res, Constants.RestReturn.ERR_USER_DELETE);
            }

            return new ResponseWrapper<IdentityResult>(res);
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="req">Request params.</param>
        /// <returns>The response with status.</returns>
        [HttpPost]
        public async Task<ResponseWrapper<IdentityResult>> Register([FromBody] NewUserRequestDTO req)
        {
            req.User.EmailConfirmed = true;
            req.User.PhoneNumberConfirmed = true;
            req.User.TwoFactorEnabled = false;

            IdentityResult res = await _userManager.CreateAsync(req.User, req.Password);
            if (!res.Succeeded)
            {
                return new ResponseWrapper<IdentityResult>(res, Constants.RestReturn.ERR_USER_CREATE);
            }

            res = await _userManager.AddToRoleAsync(req.User, "User");

            if (!res.Succeeded)
            {
                return new ResponseWrapper<IdentityResult>(res, Constants.RestReturn.ERR_USER_ROLE_ASSIGN);
            }

            return new ResponseWrapper<IdentityResult>(res);
        }
        #endregion
    }
}