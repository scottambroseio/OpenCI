using System.Threading.Tasks;

namespace OpenCI.API.Rest.Tests.Controllers.Contracts
{
    public interface IAuthenticationControllerTests
    {
        Task PasswordSignIn_ShouldReturnSuccessResponseWhenSignInAttemptIsSuccessful();
        Task PasswordSignIn_ShouldReturnBadRequestWhenSignInAttemptIsUnsuccessful();
        Task PasswordSignIn_ShouldReturnBadRequestWhenUserIsLockedOut();
        Task PasswordSignIn_ShouldReturnSuccessResponseWhenTwoFactorVerificationIsRequired();
    }
}
