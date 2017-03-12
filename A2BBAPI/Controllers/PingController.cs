using A2BBAPI.Data;
using A2BBAPI.Models;
using A2BBCommon;
using A2BBCommon.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using static A2BBAPI.Models.InOut;

namespace A2BBAPI.Controllers
{
    /// <summary>
    /// Controller to test that API server is up and running.
    /// </summary>
    [Produces("application/json")]
    [Route("api/ping")]
    [AllowAnonymous]
    public class PingController : Controller
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
        /// Create a new instance of this class.
        /// </summary>
        /// <param name="dbContext">The DI DB context.</param>
        /// <param name="loggerFactory">The DI logger factory.</param>
        public PingController(A2BBApiDbContext dbContext, ILoggerFactory loggerFactory)
        {
            _dbContext = dbContext;
            _logger = loggerFactory.CreateLogger<MeController>();
        }
        
        /// <summary>
        /// Ping this server.
        /// </summary>
        /// <returns>A successfull response if API server is up and running.</returns>
        [HttpGet]
        public ResponseWrapper<bool> Ping()
        {
            return new ResponseWrapper<bool>(true, Constants.RestReturn.OK);
        }
        #endregion
    }
}