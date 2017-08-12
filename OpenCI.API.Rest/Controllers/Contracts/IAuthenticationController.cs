using System.Threading.Tasks;
using System.Web.Http;
using OpenCI.API.Rest.Models.Authentication;

namespace OpenCI.API.Rest.Controllers.Contracts
{
    public interface IAuthenticationController
    {
        Task<IHttpActionResult> PasswordSignIn(PasswordSignInModel model);
        IHttpActionResult SignOut();
    }
}