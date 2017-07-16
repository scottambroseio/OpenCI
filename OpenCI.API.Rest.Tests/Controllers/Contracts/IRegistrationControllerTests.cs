using System.Threading.Tasks;

namespace OpenCI.API.Rest.Tests.Controllers.Contracts
{
    public interface IRegistrationControllerTests
    {
        Task PasswordRegister_ShouldReturnSuccessResponseWhenSuccessful();
        Task PasswordRegister_ShouldReturnBadRequestWhenUnsuccessful();
    }
}