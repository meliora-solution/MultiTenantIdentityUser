using Asp.Versioning;
using AuthPermissions.BaseCode.PermissionsCode;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiSample.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {

        /// <summary>
        /// This returns the permission names for the current user (or null if not available)
        /// This can be useful for your front-end to use the current user's Permissions to only expose links
        /// that the user has access too.
        /// You should call this after a login and when the JWT Token is refreshed
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getuserpermissions")]
        public ActionResult<List<string>> GetUsersPermissions([FromServices] IUsersPermissionsService service)
        {
            return service.PermissionsFromUser(User);
        }
    }
}
