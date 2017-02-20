using A2BBAPI.Data;
using A2BBAPI.Utils;
using A2BBCommon;
using A2BBCommon.DTO;
using A2BBCommon.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static A2BBAPI.Utils.ClaimsUtils;

namespace A2BBAPI.Controllers
{
    /// <summary>
    /// Controller for authorized user to configure their profile.
    /// </summary>
    [Produces("application/json")]
    [Route("api/me")]
    [Authorize("User")]
    public class MeController : Controller
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
        public MeController(A2BBApiDbContext dbContext, ILoggerFactory loggerFactory)
        {
            _dbContext = dbContext;
            _logger = loggerFactory.CreateLogger<MeController>();
        }

        /// <summary>
        /// Change user password.
        /// </summary>
        /// <param name="req">The request parameters.</param>
        /// <returns>The response with status.</returns>
        [HttpPut]
        [Route("changepass")]
        public async Task<ResponseWrapper<IdentityResult>> ChangePass([FromBody] ChangePassRequestDTO req)
        {
            ClaimsHolder claimsHolder;

            try
            {
                claimsHolder = ClaimsUtils.ValidateUserClaimForIdSrvCall(User);
            }
            catch (RestReturnException ex)
            {
                return new ResponseWrapper<IdentityResult>(ex.Value);
            }

            var response = ClientUtils.GetROClient(Constants.A2BB_IDSRV_RESOURCE_NAME, Constants.A2BB_IDSRV_RO_CLIENT_ID, claimsHolder.Name, req.OldPassword);

            if (response.IsError)
            {
                return new ResponseWrapper<IdentityResult>(Constants.RestReturn.ERR_INVALID_PASS);
            }

            var userClient = new HttpClient();
            userClient.DefaultRequestHeaders.Accept.Clear();
            userClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            userClient.SetBearerToken(response.AccessToken);
            var body = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
            var res = await userClient.PutAsync(Constants.IDENTITY_SERVER_ENDPOINT + "/api/me/changepass", body);

            if (!res.IsSuccessStatusCode)
            {
                return new ResponseWrapper<IdentityResult>(Constants.RestReturn.ERR_USER_UPDATE);
            }

            string resContent = await res.Content.ReadAsStringAsync();
            var identityRes = JsonConvert.DeserializeObject<ResponseWrapper<IdentityResult>>(resContent);

            // Fix JSON deserialization
            if (identityRes.Payload.Errors.GetEnumerator().MoveNext() == false)
            {
                var prop = identityRes.Payload.GetType().GetProperty("Succeeded");
                prop.SetValue(identityRes.Payload, true);
            }

            return identityRes;
        }
        #endregion
    }
}