using System.Threading.Tasks;

namespace OpenCI.API.Rest.Tests.Controllers.Contracts
{
    public interface IAuthenticationControllerTests
    {
        Task PasswordSignIn_ShouldReturnSuccessResponseWhenSignInAttemptIsSuccessfull();
        Task PasswordSignIn_ShouldReturnBadRequestWhenSignInAttemptIsUnsuccessfull();
        Task PasswordSignIn_ShouldReturnBadRequestWhenUserIsLockedOut();
        Task PasswordSignIn_ShouldReturnSuccessResponseWhenTwoFactorVerificationIsRequired();
    }
}
