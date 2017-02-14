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

namespace A2BBAPI.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    public class InOutController : Controller
    {
        private readonly A2BBApiDbContext _dbContext;
        private readonly ILogger _logger;

        public InOutController(A2BBApiDbContext dbContext, ILoggerFactory loggerFactory)
        {
            _dbContext = dbContext;
            _logger = loggerFactory.CreateLogger<MeController>();
        }
        
        [HttpPost]
        [Route("in/{deviceId}")]
        public ResponseWrapper<string> In([FromRoute] int deviceId)
        {
            return new ResponseWrapper<string>("In " + deviceId, Constants.RestReturn.OK);
        }
    }
}