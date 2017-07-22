using System.Threading.Tasks;
using System.Web.Http.Results;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OpenCI.API.Rest.Controllers;
using OpenCI.API.Rest.Models;
using OpenCI.API.Rest.Models.Registration;
using OpenCI.API.Rest.Tests.Controllers.Contracts;
using OpenCI.Identity;
using OpenCI.Identity.Dapper;

namespace OpenCI.API.Rest.Tests.Controllers
{
    [TestClass]
    public class RegistrationControllerTests : IRegistrationControllerTests
    {
        [TestMethod]
        public async Task PasswordRegister_ShouldReturnBadRequestWhenUnsuccessful()
        {
            var userManager = GetMockedUserManager();

            userManager.Setup(u => u.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed());

            var controller = new RegistrationController {UserManager = userManager.Object};

            var result = await controller.PasswordRegister(new PasswordRegisterModel());

            Assert.AreSame(typeof(BadRequestResult), result.GetType());
        }

        [TestMethod]
        public async Task PasswordRegister_ShouldReturnSuccessResponseWhenSuccessful()
        {
            var userManager = GetMockedUserManager();

            userManager.Setup(u => u.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var controller = new RegistrationController { UserManager = userManager.Object };

            var result = await controller.PasswordRegister(new PasswordRegisterModel());

            Assert.AreSame(typeof(OkResult), result.GetType());
        }

        private static Mock<UserManager<IdentityUser, int>> GetMockedUserManager()
        {
            var connectionHelper = new Mock<IConnectionHelper>();
            var userStore = new Mock<UserStore>(connectionHelper.Object);
            var userManager = new Mock<UserManager<IdentityUser, int>>(userStore.Object);

            return userManager;
        }
    }
}