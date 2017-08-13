using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OpenCI.API.Rest.Controllers.Contracts;
using OpenCI.API.Rest.Models.Registration;
using OpenCI.EmailTemplates.Client;
using OpenCI.Identity.Dapper;

namespace OpenCI.API.Rest.Controllers
{
    public class RegistrationController : ApiController, IRegistrationController
    {
        private readonly IEmailTemplatesClient _emailTemplatesClient;
        private UserManager<IdentityUser, int> _userManager;

        public RegistrationController(IEmailTemplatesClient emailTemplatesClient)
        {
            _emailTemplatesClient = emailTemplatesClient;
        }

        public UserManager<IdentityUser, int> UserManager
        {
            get => _userManager ?? HttpContext.Current.GetOwinContext().Get<UserManager<IdentityUser, int>>();
            set => _userManager = value;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Register", Name = nameof(PasswordRegister))]
        public async Task<IHttpActionResult> PasswordRegister([FromBody] PasswordRegisterModel model)

        {
            var newUser = new IdentityUser(model.UserName)
            {
                Email = model.UserName
            };

            var result = await UserManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded) return BadRequest();

            var user = await UserManager.FindByNameAsync(model.UserName);

            var confirmationLink = Url.Link("ConfirmEmail", null);
            var token = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
            var renderedEmail =
                await _emailTemplatesClient.GetRenderedResetPasswordTemplate(user.Id, token, confirmationLink);

            await UserManager.SendEmailAsync(user.Id, "OpenCI Email Confirmation Request", renderedEmail);

            return Ok();
        }
    }
}