using A2BBAPI.Data;
using A2BBAPI.Models;
using A2BBCommon;
using A2BBCommon.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using static A2BBAPI.Models.InOut;

namespace A2BBAPI.Controllers
{
    /// <summary>
    /// Controller used by HW granters to record in/out actions.
    /// </summary>
    [Produces("application/json")]
    [Route("api")]
    [AllowAnonymous]
    public class InOutController : Controller
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

        #region Private methods
        /// <summary>
        /// Check if device is authorized or not by its id.
        /// </summary>
        /// <param name="deviceId">The id of the device to check.</param>
        /// <param name="granterId">The granter id.</param>
        /// <returns>The device with the given id, if authorized.</returns>
        private Device CheckDevice(int deviceId, string granterId)
        {
            var device = _dbContext.Device.Include(d => d.User.Granter).FirstOrDefault(d => d.Id == deviceId);

            if (device == null)
            {
                throw new RestReturnException(Constants.RestReturn.ERR_DEVICE_NOT_FOUND);
            }

            if (!device.Enabled)
            {
                throw new RestReturnException(Constants.RestReturn.ERR_DEVICE_DISABLED);
            }

            if (device.User == null || device.User.Granter == null || device.User.Granter.FirstOrDefault(g => g.Id == granterId) == null)
            {
                throw new RestReturnException(Constants.RestReturn.ERR_INVALID_GRANTER);
            }

            return device;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Create a new instance of this class.
        /// </summary>
        /// <param name="dbContext">The DI DB context.</param>
        /// <param name="loggerFactory">The DI logger factory.</param>
        public InOutController(A2BBApiDbContext dbContext, ILoggerFactory loggerFactory)
        {
            _dbContext = dbContext;
            _logger = loggerFactory.CreateLogger<MeController>();
        }
        
        /// <summary>
        /// Register in action.
        /// </summary>
        /// <param name="deviceId">The id of the device which performs this action.</param>
        /// <param name="granterId">The granter id.</param>
        /// <returns><c>True</c> if ok, <c>false</c> otherwise.</returns>
        [HttpGet]
        [Route("in/{deviceId}/{granterId}")]
        public ResponseWrapper<bool> In([FromRoute] int deviceId, [FromRoute] string granterId)
        {
            Device device;

            try
            {
                device = CheckDevice(deviceId, granterId);
            }
            catch (RestReturnException e)
            {
                return new ResponseWrapper<bool>(false, e.Value);
            }

            var inObj = new InOut
            {
                Type = InOutType.In,
                Device = device,
                OnDate = DateTime.Now
            };

            _dbContext.InOut.Add(inObj);
            _dbContext.SaveChanges();

            return new ResponseWrapper<bool>(true, Constants.RestReturn.OK);
        }

        /// <summary>
        /// Register out action.
        /// </summary>
        /// <param name="deviceId">The id of the device which performs this action.</param>
        /// <param name="granterId">The granter id.</param>
        /// <returns><c>True</c> if ok, <c>false</c> otherwise.</returns>
        [HttpGet]
        [Route("out/{deviceId}/{granterId}")]
        public ResponseWrapper<bool> Out([FromRoute] int deviceId, [FromRoute] string granterId)
        {
            Device device;

            try
            {
                device = CheckDevice(deviceId, granterId);
            }
            catch (RestReturnException e)
            {
                return new ResponseWrapper<bool>(false, e.Value);
            }

            var inObj = new InOut
            {
                Type = InOutType.Out,
                Device = device,
                OnDate = DateTime.Now
            };

            _dbContext.InOut.Add(inObj);
            _dbContext.SaveChanges();

            return new ResponseWrapper<bool>(true, Constants.RestReturn.OK);
        }
        #endregion
    }
}