using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OpenCI.API.Rest.Controllers;
using OpenCI.API.Rest.Models;
using OpenCI.API.Rest.Tests.Controllers.Contracts;
using OpenCI.Identity.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace OpenCI.API.Rest.Tests.Controllers
{
    [TestClass]
    public class RegistrationControllerTests : IRegistrationControllerTests
    {
        [TestMethod]
        public async Task PasswordRegister_ShouldReturnBadRequestWhenUnsuccessful()
        {
            var userManager = GetMockedUserManager();

            userManager.Setup(u => u.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Failed());

            var controller = new RegistrationController(userManager.Object);

            var result = await controller.PasswordRegister(new PasswordRegisterModel()) as BadRequestResult;

            Assert.AreSame(typeof(BadRequestResult), result.GetType());
        }

        [TestMethod]
        public async Task PasswordRegister_ShouldReturnSuccessResponseWhenSuccessful()
        {
            var userManager = GetMockedUserManager();

            userManager.Setup(u => u.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            var controller = new RegistrationController(userManager.Object);

            var result = await controller.PasswordRegister(new PasswordRegisterModel()) as OkResult;

            Assert.AreSame(typeof(OkResult), result.GetType());
        }

        private Mock<UserManager<IdentityUser, int>> GetMockedUserManager()
        {
            var connectionHelper = new Mock<IConnectionHelper>();
            var userStore = new Mock<UserStore>(connectionHelper.Object);
            var userManager = new Mock<UserManager<IdentityUser, int>>(userStore.Object);

            return userManager;
        }
    }
}
