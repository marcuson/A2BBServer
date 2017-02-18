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
    [Produces("application/json")]
    [Route("api")]
    [AllowAnonymous]
    public class InOutController : Controller
    {
        private readonly A2BBApiDbContext _dbContext;
        private readonly ILogger _logger;

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

            var response = ClientUtils.GetRTClient(Constants.A2BB_API_RESOURCE_NAME, Constants.A2BB_API_RO_CLIENT_ID, device.RefreshToken);

            if (response.IsError)
            {
                throw new RestReturnException(Constants.RestReturn.ERR_DEVICE_DISABLED);
            }

            return device;
        }

        public InOutController(A2BBApiDbContext dbContext, ILoggerFactory loggerFactory)
        {
            _dbContext = dbContext;
            _logger = loggerFactory.CreateLogger<MeController>();
        }
        
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
    }
}