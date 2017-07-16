using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OpenCI.API.Rest.Models;
using OpenCI.Identity.Dapper;
using System.Threading.Tasks;
using System.Web.Http;

namespace OpenCI.API.Rest.Controllers
{
    [RoutePrefix("Authentication")]
    public class AuthenticationController : ApiController
    {
        private readonly SignInManager<IdentityUser, int> _signInManager;
        private readonly UserManager<IdentityUser, int> _userManager;

        public AuthenticationController(
            SignInManager<IdentityUser, int> signInManager,
            UserManager<IdentityUser, int> userManager
        )
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("SignIn")]
        public async Task<IHttpActionResult> SignIn([FromBody]PasswordSignInModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, true).ConfigureAwait(false);

            switch(result)
            {
                case SignInStatus.Failure: return BadRequest();
                case SignInStatus.LockedOut: return BadRequest();
                case SignInStatus.RequiresVerification: return BadRequest();
                case SignInStatus.Success: return Ok();
                default: return BadRequest();
            }
        }
    }
}
