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

namespace A2BBAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/me")]
    [Authorize]
    public class MeController : Controller
    {
        private readonly A2BBApiDbContext _dbContext;
        private readonly ILogger _logger;

        public MeController(A2BBApiDbContext dbContext, ILoggerFactory loggerFactory)
        {
            _dbContext = dbContext;
            _logger = loggerFactory.CreateLogger<MeController>();
        }

        [HttpGet]
        [Route("devices")]
        public ResponseWrapper<IEnumerable<Device>> ListDevices()
        {
            return new ResponseWrapper<IEnumerable<Device>>(_dbContext.Device.Where(d => d.UserId == User.Claims.FirstOrDefault(c => c.Type == "sub").Value), Constants.RestReturn.OK);
        }

        [HttpPost]
        [Route("link")]
        public async Task<ResponseWrapper<Device>> Link([FromBody] NewLinkRequestDTO req)
        {
            var subClaim = User.Claims.FirstOrDefault(c => c.Type == "sub");
            if (subClaim == null || String.IsNullOrWhiteSpace(subClaim.Value))
            {
                return new ResponseWrapper<Device>(Constants.RestReturn.ERR_INVALID_SUB_CLAIM);
            }

            var name = User.Identity.Name;
            if (name == null || String.IsNullOrWhiteSpace(name))
            {
                return new ResponseWrapper<Device>(Constants.RestReturn.ERR_INVALID_NAME_CLAIM);
            }

            var disco = await DiscoveryClient.GetAsync(Constants.IDENTITY_SERVER_ENDPOINT);
            var client = new TokenClient(disco.TokenEndpoint, Constants.A2BB_API_CLIENT_ID, Constants.A2BB_API_CLIENT_SECRET);
            var response = await client.RequestResourceOwnerPasswordAsync(name, req.Password);

            var sub = _dbContext.Subject.FirstOrDefault(s => s.Id == subClaim.Value);
            if (sub == null)
            {
                sub = new Subject { Id = subClaim.Value };
                _dbContext.Subject.Add(sub);
            }

            var d = new Device { RefreshToken = response.RefreshToken };
            sub.Device.Add(d);
            _dbContext.SaveChanges();

            return new ResponseWrapper<Device>(d, Constants.RestReturn.OK);
        }
    }
}