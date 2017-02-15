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

namespace A2BBAPI.Controllers
{
    /// <summary>
    /// Controller to manage user devices.
    /// </summary>
    [Produces("application/json")]
    [Route("api/me/devices")]
    [Authorize]
    public class MeDevicesController : Controller
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
        public MeDevicesController(A2BBApiDbContext dbContext, ILoggerFactory loggerFactory)
        {
            _dbContext = dbContext;
            _logger = loggerFactory.CreateLogger<MeController>();
        }

        /// <summary>
        /// Get all devices associated to currently authorized user.
        /// </summary>
        /// <returns>The list of device belonging to the current user.</returns>
        [HttpGet]
        public ResponseWrapper<IEnumerable<Device>> ListDevices()
        {
            return new ResponseWrapper<IEnumerable<Device>>(_dbContext.Device.Where(d => d.UserId == User.Claims.FirstOrDefault(c => c.Type == "sub").Value), Constants.RestReturn.OK);
        }

        /// <summary>
        /// Link a new device to the user.
        /// </summary>
        /// <param name="req">The request params.</param>
        /// <returns>The response with status.</returns>
        [HttpPost]
        public ResponseWrapper<Device> Link([FromBody] NewLinkRequestDTO req)
        {
            ClaimsHolder claimsHolder;

            try
            {
                claimsHolder = ClaimsUtils.ValidateUserClaimForIdSrvCall(User);
            }
            catch (RestReturnException ex)
            {
                return new ResponseWrapper<Device>(ex.Value);
            }

            var response = ClientUtils.GetROClient(Constants.A2BB_API_RESOURCE_NAME + " offline_access", Constants.A2BB_API_CLIENT_ID, claimsHolder.Name, req.Password);

            if (response.IsError)
            {
                return new ResponseWrapper<Device>(Constants.RestReturn.ERR_INVALID_PASS);
            }

            var sub = _dbContext.Subject.FirstOrDefault(s => s.Id == claimsHolder.Sub);
            if (sub == null)
            {
                sub = new Subject { Id = claimsHolder.Sub };
                _dbContext.Subject.Add(sub);
            }

            req.Device.RefreshToken = response.RefreshToken;
            sub.Device.Add(req.Device);
            _dbContext.SaveChanges();

            return new ResponseWrapper<Device>(req.Device, Constants.RestReturn.OK);
        }

        /// <summary>
        /// Get a device associated to currently authorized user.
        /// </summary>
        /// <returns>The device belonging to the current user.</returns>
        [HttpGet]
        [Route("{deviceId}")]
        public ResponseWrapper<Device> GetDevice([FromRoute] int deviceId)
        {
            return new ResponseWrapper<Device>(_dbContext.Device.FirstOrDefault(d => d.UserId == User.Claims.FirstOrDefault(c => c.Type == "sub").Value && d.Id == deviceId), Constants.RestReturn.OK);
        }

        /// <summary>
        /// Update a device associated to currently authorized user.
        /// </summary>
        /// <param name="deviceId">The id of the device to update.</param>
        /// <param name="device">The updated device info.</param>
        /// <returns>The updated device.</returns>
        [HttpPut]
        [Route("{deviceId}")]
        public ResponseWrapper<Device> UpdateDevice([FromRoute] int deviceId, [FromBody] Device device)
        {
            _dbContext.Device.Update(device);
            _dbContext.SaveChanges();

            return new ResponseWrapper<Device>(device, Constants.RestReturn.OK);
        }
        #endregion
    }
}