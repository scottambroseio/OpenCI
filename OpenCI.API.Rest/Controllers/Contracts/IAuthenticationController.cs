using System.Threading.Tasks;
using System.Web.Http;
using OpenCI.API.Rest.Models;

namespace OpenCI.API.Rest.Controllers.Contracts
{
    public interface IAuthenticationController
    {
        Task<IHttpActionResult> PasswordSignIn(PasswordSignInModel model);
        //Task<IHttpActionResult> ExternalSignIn(PasswordSignInModel model);
        //Task<IHttpActionResult> ResetPassword(PasswordSignInModel model);
    }
}