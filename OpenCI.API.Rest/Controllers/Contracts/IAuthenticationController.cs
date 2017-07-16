using OpenCI.API.Rest.Models;
using System.Threading.Tasks;
using System.Web.Http;

namespace OpenCI.API.Rest.Controllers.Contracts
{
    public interface IAuthenticationController
    {
        Task<IHttpActionResult> PasswordSignIn([FromBody]PasswordSignInModel model);
        //Task<IHttpActionResult> ExternalSignIn([FromBody]PasswordSignInModel model);
    }
}
