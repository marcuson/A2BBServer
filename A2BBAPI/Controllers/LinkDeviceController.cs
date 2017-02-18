using A2BBAPI.Data;
using A2BBAPI.DTO;
using A2BBAPI.Models;
using A2BBAPI.Utils;
using A2BBCommon;
using A2BBCommon.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Linq;
using static A2BBAPI.Utils.ClaimsUtils;

namespace A2BBAPI.Controllers
{
    /// <summary>
    /// Controller to actually link device to user.
    /// </summary>
    [Produces("application/json")]
    [Route("api/link")]
    [AllowAnonymous]
    public class LinkDeviceController : Controller
    {
        #region Private fields
        /// <summary>
        /// The DB context.
        /// </summary>
        private readonly A2BBApiDbContext _dbContext;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// The in memory cache.
        /// </summary>
        private readonly IMemoryCache _memCache;
        #endregion

        #region Public methods
        /// <summary>
        /// Create a new isntance of this class.
        /// </summary>
        /// <param name="dbContext">The DI DB context.</param>
        /// <param name="loggerFactory">The DI logger factory.</param>
        /// /// <param name="memCache">The DI in memory cache.</param>
        public LinkDeviceController(A2BBApiDbContext dbContext, ILoggerFactory loggerFactory, IMemoryCache memCache)
        {
            _dbContext = dbContext;
            _logger = loggerFactory.CreateLogger<MeController>();
            _memCache = memCache;
        }

        /// <summary>
        /// Check if link has been estabilished.
        /// </summary>
        /// <param name="tempGuid">The temporary guid used for linking.</param>
        /// <returns>Response with link estabilished status.</returns>
        [HttpGet]
        [Route("{tempGuid}")]
        public ResponseWrapper<bool> Check([FromRoute] string tempGuid)
        {

            LinkHolder link;
            if (!_memCache.TryGetValue<LinkHolder>(tempGuid, out link))
            {
                return new ResponseWrapper<bool>(Constants.RestReturn.ERR_LINK);
            }

            return new ResponseWrapper<bool>(link.IsEstabilished, Constants.RestReturn.OK);
        }

        /// <summary>
        /// Actually link a device.
        /// </summary>
        /// <param name="tempGuid">The temporary guid used to link the device.</param>
        /// <returns>The response with status.</returns>
        [HttpPost]
        [Route("{tempGuid}")]
        public ResponseWrapper<Device> Link([FromRoute] string tempGuid)
        {
            LinkHolder link;
            if (!_memCache.TryGetValue<LinkHolder>(tempGuid, out link))
            {
                return new ResponseWrapper<Device>(Constants.RestReturn.ERR_LINK);
            }

            var response = ClientUtils.GetROClient(Constants.A2BB_API_RESOURCE_NAME + " offline_access", Constants.A2BB_API_RO_CLIENT_ID, link.Username, link.Password);

            if (response.IsError)
            {
                return new ResponseWrapper<Device>(Constants.RestReturn.ERR_INVALID_PASS);
            }

            var sub = _dbContext.Subject.FirstOrDefault(s => s.Id == link.Subject);
            if (sub == null)
            {
                sub = new Subject { Id = link.Subject };
                _dbContext.Subject.Add(sub);
            }

            link.Device.RefreshToken = response.RefreshToken;
            sub.Device.Add(link.Device);
            _dbContext.SaveChanges();

            link.IsEstabilished = true;

            return new ResponseWrapper<Device>(link.Device, Constants.RestReturn.OK);
        }
        #endregion
    }
}