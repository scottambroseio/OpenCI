using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using OpenCI.API.Rest.Controllers.Contracts;
using OpenCI.API.Rest.Models.Registration;
using OpenCI.Identity.Dapper;

namespace OpenCI.API.Rest.Controllers
{
    public class RegistrationController : ApiController, IRegistrationController
    {
        private readonly UserManager<IdentityUser, int> _userManager;

        public RegistrationController(UserManager<IdentityUser, int> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IHttpActionResult> PasswordRegister([FromBody] PasswordRegisterModel model)
        {
            var identityUser = new IdentityUser(model.UserName);

            var result = await _userManager.CreateAsync(identityUser, model.Password);

            if (result.Succeeded) return Ok();

            return BadRequest();
        }

        [HttpPost]
        [Route("ForgottenPassword")]
        public async Task<IHttpActionResult> ForgottenPassword(ResetPasswordRequestModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName).ConfigureAwait(false);

            if (user == null) return Ok();

            var hasPassword = await _userManager.HasPasswordAsync(user.Id).ConfigureAwait(false);

            if (hasPassword)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user.Id).ConfigureAwait(false);

                // send email
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IHttpActionResult> ResetPassword([FromBody] ResetPasswordSubmissionModel model)
        {
            var result = await _userManager.ResetPasswordAsync(model.Id, model.Token, model.NewPassword).ConfigureAwait(false);

            if (result.Succeeded) return Ok();

            return BadRequest();
        }
    }
}