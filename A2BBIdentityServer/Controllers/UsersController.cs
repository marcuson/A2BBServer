using A2BBCommon.Data;
using A2BBCommon.Models;
using A2BBIdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace A2BBIdentityServer.Controllers
{
    [Produces("application/json")]
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger _logger;

        public UsersController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Get()
        {
            return new JsonResult(User.Claims.Select(c => new { c.Type, c.Value }));
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<ResponseWrapper<IdentityResult>> Register([FromBody] NewUserRequestDTO req)
        {
            req.User.EmailConfirmed = true;
            req.User.PhoneNumberConfirmed = true;
            req.User.TwoFactorEnabled = false;

            IdentityResult res = await _userManager.CreateAsync(req.User, req.Password);
            if (!res.Succeeded)
            {
                return new ResponseWrapper<IdentityResult>(res, "ERR", "Error during user creation");
            }

            res = await _userManager.AddToRoleAsync(req.User, "User");

            return new ResponseWrapper<IdentityResult>(res);
        }

        [HttpGet]
        [Route("link/{userId}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ResponseWrapper<LinkResponseDTO>> Link([FromRoute] string userId)
        {
            User u = await _userManager.FindByIdAsync(userId);
            if (u == null)
            {
                return new ResponseWrapper<LinkResponseDTO>(null, "ERR", "User not found.");
            }

            return new ResponseWrapper<LinkResponseDTO>(
                new LinkResponseDTO() { UserName = u.UserName });
        }
    }
}