using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using OpenCI.API.Rest.Models;
using OpenCI.Identity.Dapper;
using OpenCI.API.Rest.Controllers.Contracts;

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
        [Route("register")]
        public async Task<IHttpActionResult> PasswordRegister([FromBody] PasswordRegisterModel model)
        {
            var identityUser = new IdentityUser(model.UserName);

            var result = await _userManager.CreateAsync(identityUser, model.Password);

            if (result.Succeeded) return Ok();

            return BadRequest();
        }
    }
}
