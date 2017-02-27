using A2BBAPI.Data;
using A2BBAPI.Models;
using A2BBCommon;
using A2BBCommon.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using static A2BBAPI.Models.InOut;

namespace A2BBAPI.Controllers
{
    /// <summary>
    /// Controller used by HW granters to record in/out actions.
    /// </summary>
    [Produces("application/json")]
    [Route("api/admin/granter")]
    [Authorize("Admin")]
    public class AdminGranterController : Controller
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
        public AdminGranterController(A2BBApiDbContext dbContext, ILoggerFactory loggerFactory)
        {
            _dbContext = dbContext;
            _logger = loggerFactory.CreateLogger<MeController>();
        }

        /// <summary>
        /// Get a list of granters.
        /// </summary>
        /// <returns>The list of granters.</returns>
        [HttpGet]
        public ResponseWrapper<IEnumerable<Granter>> List()
        {
            return new ResponseWrapper<IEnumerable<Granter>>(_dbContext.Granter.AsEnumerable());
        }

        /// <summary>
        /// Insert a new granter.
        /// </summary>
        /// <param name="granter">The granter object.</param>
        /// <returns>The newly inserted granted object.</returns>
        [HttpPost]
        public ResponseWrapper<Granter> NewGranter([FromBody] Granter granter)
        {
            _dbContext.Granter.Add(granter);
            _dbContext.SaveChanges();

            return new ResponseWrapper<Granter>(granter);
        }

        /// <summary>
        /// Update a granter.
        /// </summary>
        /// <param name="granterId">The granter id to update.</param>
        /// <param name="granter">The granter object.</param>
        /// <returns>The updated granter object.</returns>
        [HttpPut]
        [Route("{granterId}")]
        public ResponseWrapper<Granter> UpdateGranter([FromRoute] string granterId, [FromBody] Granter granter)
        {
            if (granterId != granter.Id)
            {
                return new ResponseWrapper<Granter>(Constants.RestReturn.ERR_INVALID_GRANTER);
            }

            if (_dbContext.Subject.FirstOrDefault(s => s.Id == granter.SubId) == null)
            {
                Subject s = new Subject();
                s.Id = granter.SubId;
                _dbContext.Subject.Add(s);
            }

            _dbContext.Granter.Update(granter);
            _dbContext.SaveChanges();

            return new ResponseWrapper<Granter>(granter);
        }

        /// <summary>
        /// Delete a granter.
        /// </summary>
        /// <param name="granterId">The granter id to update.</param>
        /// <returns>Response with <c>true</c> if deleted.</returns>
        [HttpDelete]
        [Route("{granterId}")]
        public ResponseWrapper<bool> DeleteGranter([FromRoute] string granterId)
        {
            Granter grant = _dbContext.Granter.FirstOrDefault(g => g.Id == granterId);

            if (grant == null)
            {
                return new ResponseWrapper<bool>(Constants.RestReturn.ERR_GRANTER_NOT_FOUND);
            }

            _dbContext.Granter.Remove(grant);
            _dbContext.SaveChanges();

            return new ResponseWrapper<bool>(true);
        }
        #endregion
    }
}