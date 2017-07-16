using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using OpenCI.API.Rest.Controllers.Contracts;
using OpenCI.API.Rest.Models;
using OpenCI.Identity.Dapper;

namespace OpenCI.API.Rest.Controllers
{
    public class AuthenticationController : ApiController, IAuthenticationController
    {
        private readonly SignInManager<IdentityUser, int> _signInManager;

        public AuthenticationController(
            SignInManager<IdentityUser, int> signInManager
        )
        {
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("SignIn")]
        public async Task<IHttpActionResult> PasswordSignIn([FromBody] PasswordSignInModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, true)
                .ConfigureAwait(false);

            switch (result)
            {
                case SignInStatus.Failure: return BadRequest();
                case SignInStatus.LockedOut: return BadRequest();
                case SignInStatus.RequiresVerification: return Ok();
                case SignInStatus.Success: return Ok();
                default: return InternalServerError();
            }
        }
    }
}