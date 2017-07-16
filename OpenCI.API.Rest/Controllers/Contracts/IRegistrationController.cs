using OpenCI.API.Rest.Models;
using System.Threading.Tasks;
using System.Web.Http;

namespace OpenCI.API.Rest.Controllers.Contracts
{
    public interface IRegistrationController
    {
        Task<IHttpActionResult> PasswordRegister(PasswordRegisterModel passwordRegisterModel);
        //Task<IHttpActionResult> ExternalRegister(PasswordRegisterModel passwordRegisterModel);
    }
}
