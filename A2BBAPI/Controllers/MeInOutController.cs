using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using A2BBCommon.Models;
using A2BBCommon;
using A2BBAPI.Data;
using IdentityModel.Client;
using A2BBAPI.DTO;
using Microsoft.Extensions.Logging;
using A2BBAPI.Models;
using System.Collections.Generic;
using A2BBAPI.Utils;
using static A2BBAPI.Utils.ClaimsUtils;
using Microsoft.Extensions.Caching.Memory;

namespace A2BBAPI.Controllers
{
    /// <summary>
    /// Controller to manage user in/out actions.
    /// </summary>
    [Produces("application/json")]
    [Route("api/me/inout")]
    [Authorize("User")]
    public class MeInOutController : Controller
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
        #endregion

        #region Public methods
        /// <summary>
        /// Create a new isntance of this class.
        /// </summary>
        /// <param name="dbContext">The DI DB context.</param>
        /// <param name="loggerFactory">The DI logger factory.</param>
        public MeInOutController(A2BBApiDbContext dbContext, ILoggerFactory loggerFactory)
        {
            _dbContext = dbContext;
            _logger = loggerFactory.CreateLogger<MeController>();
        }

        /// <summary>
        /// Get a list of in/out actions associated to this user.
        /// </summary>
        /// <returns>A list of in/out actions associated to this user.</returns>
        [HttpGet]
        public ResponseWrapper<IEnumerable<InOut>> ListAll()
        {
            return new ResponseWrapper<IEnumerable<InOut>>(_dbContext.InOut.Where(io => io.Device.UserId == User.Claims.FirstOrDefault(c => c.Type == "sub").Value), Constants.RestReturn.OK);
        }

        /// <summary>
        /// Get a list of in/out actions associated to this user, filtered by device id.
        /// </summary>
        /// <returns>A list of in/out actions associated to this user, filtered by device id.</returns>
        [HttpGet]
        [Route("{deviceId}")]
        public ResponseWrapper<IEnumerable<InOut>> ListOfSpecificDevice([FromRoute] int deviceId)
        {
            return new ResponseWrapper<IEnumerable<InOut>>(_dbContext.InOut.Where(io => io.Device.UserId == User.Claims.FirstOrDefault(c => c.Type == "sub").Value && io.DeviceId == deviceId),
                Constants.RestReturn.OK);
        }
        #endregion
    }
}