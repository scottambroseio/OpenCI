using System.Threading.Tasks;
using System.Web.Http;
using OpenCI.API.Rest.Models.Account;

namespace OpenCI.API.Rest.Controllers.Contracts
{
    public interface IAccountController
    {
        Task<IHttpActionResult> ForgottenPassword(ResetPasswordRequestModel model);
        Task<IHttpActionResult> ResetPassword(ResetPasswordSubmissionModel model);
        Task<IHttpActionResult> ConfirmEmail(ConfirmEmailModel model);
        Task<IHttpActionResult> EnableTwoFactor();
        Task<IHttpActionResult> DisableTwoFactor();
    }
}
