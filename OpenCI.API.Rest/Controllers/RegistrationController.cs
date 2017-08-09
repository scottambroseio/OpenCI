using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OpenCI.API.Rest.Attributes;
using OpenCI.API.Rest.Controllers.Contracts;
using OpenCI.API.Rest.Models.Registration;
using OpenCI.Identity.Dapper;

namespace OpenCI.API.Rest.Controllers
{
    public class RegistrationController : ApiController, IRegistrationController
    {
        private UserManager<IdentityUser, int> _userManager;

        public UserManager<IdentityUser, int> UserManager
        {
            get { return _userManager ?? HttpContext.Current.GetOwinContext().Get<UserManager<IdentityUser, int>>(); }
            set { _userManager = value; }
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

            var token = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);

            var confirmationLink = Url.Link("ConfirmEmail", null);

            await UserManager.SendEmailAsync(user.Id, "OpenCI Email Confirmation Request",
                $"\r\n\r\n" +
                $"Link: {confirmationLink}\r\n" +
                $"Id: {user.Id}\r\n" +
                $"Token: {token}"
            );

            return Ok();
        }
    }
}