using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OpenCI.API.Rest.Controllers.Contracts;
using OpenCI.API.Rest.Models.Account;
using OpenCI.Contracts.Business;
using OpenCI.Identity.Dapper;

namespace OpenCI.API.Rest.Controllers
{
    [RoutePrefix("Account")]
    public class AccountController : ApiController, IAccountController
    {
        private UserManager<IdentityUser, int> _userManager;

        public UserManager<IdentityUser, int> UserManager
        {
            get { return _userManager ?? HttpContext.Current.GetOwinContext().Get<UserManager<IdentityUser, int>>(); }
            set { _userManager = value; }
        }

        private readonly IEmailRenderService _emailRenderService;

        public AccountController(IEmailRenderService emailRenderService)
        {
            _emailRenderService = emailRenderService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("ForgottenPassword", Name = nameof(ForgottenPassword))]
        public async Task<IHttpActionResult> ForgottenPassword([FromBody] ResetPasswordRequestModel model)
        {
            var user = await UserManager.FindByNameAsync(model.UserName);

            if (user == null) return Ok();

            var hasPassword = await UserManager.HasPasswordAsync(user.Id);

            if (!hasPassword) return Ok();

            var resetLink = Url.Link("ResetPassword", null);
            var token = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
            var renderedEmail = await _emailRenderService.GetRenderedResetPasswordTemplate(user.Id, token, resetLink);

            await UserManager.SendEmailAsync(user.Id, "OpenCI Password Reset Request", renderedEmail);

            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("ResetPassword", Name = nameof(ResetPassword))]
        public async Task<IHttpActionResult> ResetPassword([FromBody] ResetPasswordSubmissionModel model)
        {
            var result = await UserManager.ResetPasswordAsync(model.Id, model.Token, model.NewPassword);

            if (result.Succeeded) return Ok();

            return BadRequest();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("ConfirmEmail", Name = nameof(ConfirmEmail))]
        public async Task<IHttpActionResult> ConfirmEmail([FromBody] ConfirmEmailModel model)
        {
            var result = await UserManager.ConfirmEmailAsync(model.Id, model.Token);

            if (result.Succeeded) return Ok();

            return BadRequest();
        }

        [HttpPost]
        [Route("EnableTwoFactor", Name = nameof(EnableTwoFactor))]
        public async Task<IHttpActionResult> EnableTwoFactor()
        {
            var userId = User.Identity.GetUserId<int>();

            var result = await UserManager.SetTwoFactorEnabledAsync(userId, true);

            if (result.Succeeded) return Ok();

            return BadRequest();
        }

        [HttpPost]
        [Route("DisableTwoFactor", Name = nameof(DisableTwoFactor))]
        public async Task<IHttpActionResult> DisableTwoFactor()
        {
            var userId = User.Identity.GetUserId<int>();

            var result = await UserManager.SetTwoFactorEnabledAsync(userId, false);

            if (result.Succeeded) return Ok();

            return BadRequest();
        }
    }
}
