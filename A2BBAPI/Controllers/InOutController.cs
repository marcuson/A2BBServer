using Microsoft.AspNetCore.Mvc;
using A2BBCommon.Models;
using A2BBCommon;
using A2BBAPI.Data;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using A2BBAPI.Utils;
using System.Linq;
using A2BBAPI.Models;
using static A2BBAPI.Models.InOut;
using System;

namespace A2BBAPI.Controllers
{
    /// <summary>
    /// Controller user by HW granters to record in/out actions.
    /// </summary>
    [Produces("application/json")]
    [Route("api")]
    [Authorize("Granter")]
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
        /// <returns>The device with the given id, if authorized.</returns>
        private Device CheckDeviceId(int deviceId)
        {
            var device = _dbContext.Device.FirstOrDefault(d => d.Id == deviceId);

            if (device == null)
            {
                throw new RestReturnException(Constants.RestReturn.ERR_DEVICE_NOT_FOUND);
            }

            if (!device.Enabled)
            {
                throw new RestReturnException(Constants.RestReturn.ERR_DEVICE_DISABLED);
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
        /// <returns><c>True</c> if ok, <c>false</c> otherwise.</returns>
        [HttpPost]
        [Route("in/{deviceId}")]
        public ResponseWrapper<string> In([FromRoute] int deviceId)
        {
            Device device;

            try
            {
                device = CheckDeviceId(deviceId);
            }
            catch (RestReturnException e)
            {
                return new ResponseWrapper<string>(e.Value);
            }

            var inObj = new InOut
            {
                Type = InOutType.In,
                Device = device,
                OnDate = DateTime.Now
            };

            _dbContext.InOut.Add(inObj);
            _dbContext.SaveChanges();

            return new ResponseWrapper<string>("In " + deviceId, Constants.RestReturn.OK);
        }

        /// <summary>
        /// Register out action.
        /// </summary>
        /// <param name="deviceId">The id of the device which performs this action.</param>
        /// <returns><c>True</c> if ok, <c>false</c> otherwise.</returns>
        [HttpPost]
        [Route("out/{deviceId}")]
        public ResponseWrapper<string> Out([FromRoute] int deviceId)
        {
            Device device;

            try
            {
                device = CheckDeviceId(deviceId);
            }
            catch (RestReturnException e)
            {
                return new ResponseWrapper<string>(e.Value);
            }

            var inObj = new InOut
            {
                Type = InOutType.In,
                Device = device,
                OnDate = DateTime.Now
            };

            _dbContext.InOut.Add(inObj);
            _dbContext.SaveChanges();

            return new ResponseWrapper<string>("Out " + deviceId, Constants.RestReturn.OK);
        }
        #endregion
    }
}