using System.Threading.Tasks;
using System.Web.Http;
using OpenCI.API.Rest.Models.Registration;

namespace OpenCI.API.Rest.Controllers.Contracts
{
    public interface IRegistrationController
    {
        Task<IHttpActionResult> PasswordRegister(PasswordRegisterModel model);
        Task<IHttpActionResult> ForgottenPassword(ResetPasswordRequestModel model);
        Task<IHttpActionResult> ResetPassword(ResetPasswordSubmissionModel model);
        Task<IHttpActionResult> ConfirmEmail(ConfirmEmailModel model);
    }
}