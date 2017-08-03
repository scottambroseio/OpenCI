using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using OpenCI.API.Rest.Controllers.Contracts;
using OpenCI.API.Rest.Models.Authentication;
using OpenCI.Identity.Dapper;

namespace OpenCI.API.Rest.Controllers
{
    public class AuthenticationController : ApiController, IAuthenticationController
    {
        private IAuthenticationManager _authenticationManager;
        private SignInManager<IdentityUser, int> _signInManager;

        public SignInManager<IdentityUser, int> SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<SignInManager<IdentityUser, int>>();
            }
            set { _signInManager = value; }
        }

        public IAuthenticationManager AuthenticationManager
        {
            get { return _authenticationManager ?? HttpContext.Current.GetOwinContext().Authentication; }
            set { _authenticationManager = value; }
        }

        [HttpPost]
        [Route("SignIn")]
        public async Task<IHttpActionResult> PasswordSignIn([FromBody] PasswordSignInModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, false, true);

            switch (result)
            {
                case SignInStatus.Failure: return BadRequest();
                case SignInStatus.LockedOut: return BadRequest();
                case SignInStatus.RequiresVerification: return Ok();
                case SignInStatus.Success: return Ok();
                default: return InternalServerError();
            }
        }

        [HttpPost]
        [Route("SignOut")]
        public IHttpActionResult SignOut()
        {
            AuthenticationManager.SignOut();

            return Ok();
        }
    }
}