using System.Data;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OpenCI.API.Rest.Controllers;
using OpenCI.API.Rest.Models.Roles;
using OpenCI.API.Rest.Tests.Controllers.Contracts;
using OpenCI.Identity.Dapper;

namespace OpenCI.API.Rest.Tests.Controllers
{
    [TestClass]
    public class RoleControllerTests : IRoleControllerTests
    {
        [TestMethod]
        public async Task CreateRole_ShouldReturnSuccessResponseWhenSuccessful()
        {
            var roleManager = GetMockedRoleManager();

            roleManager.Setup(r => r.CreateAsync(It.IsAny<IdentityRole>())).ReturnsAsync(IdentityResult.Success);

            var controller = new RoleController {RoleManager = roleManager.Object};

            var result = await controller.CreateRole(new CreateRoleModel());

            Assert.AreSame(typeof(OkResult), result.GetType());
        }

        [TestMethod]
        public async Task CreateRole_ShouldReturnBadRequestWhenUnsuccessful()
        {
            var roleManager = GetMockedRoleManager();

            roleManager.Setup(r => r.CreateAsync(It.IsAny<IdentityRole>())).ReturnsAsync(IdentityResult.Failed());

            var controller = new RoleController {RoleManager = roleManager.Object};

            var result = await controller.CreateRole(new CreateRoleModel());

            Assert.AreSame(typeof(BadRequestResult), result.GetType());
        }

        [TestMethod]
        public async Task UpdateRole_ShouldReturnSuccessResponseWhenSuccessful()
        {
            var roleManager = GetMockedRoleManager();

            roleManager.Setup(r => r.UpdateAsync(It.IsAny<IdentityRole>())).ReturnsAsync(IdentityResult.Success);
            roleManager.Setup(r => r.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(new IdentityRole(string.Empty));

            var controller = new RoleController {RoleManager = roleManager.Object};

            var result = await controller.UpdateRole(string.Empty, new UpdateRoleModel());

            Assert.AreSame(typeof(OkResult), result.GetType());
        }

        [TestMethod]
        public async Task UpdateRole_ShouldReturnBadRequestWhenUnsuccessful()
        {
            var roleManager = GetMockedRoleManager();

            roleManager.Setup(r => r.UpdateAsync(It.IsAny<IdentityRole>())).ReturnsAsync(IdentityResult.Failed());

            var controller = new RoleController {RoleManager = roleManager.Object};

            var result = await controller.UpdateRole(string.Empty, new UpdateRoleModel());

            Assert.AreSame(typeof(BadRequestResult), result.GetType());
        }

        [TestMethod]
        public async Task DeleteRole_ShouldReturnSuccessResponseWhenSuccessful()
        {
            var roleManager = GetMockedRoleManager();

            roleManager.Setup(r => r.DeleteAsync(It.IsAny<IdentityRole>())).ReturnsAsync(IdentityResult.Success);

            var controller = new RoleController {RoleManager = roleManager.Object};

            var result = await controller.DeleteRole(string.Empty);

            Assert.AreSame(typeof(OkResult), result.GetType());
        }

        [TestMethod]
        public async Task DeleteRole_ShouldReturnBadRequestWhenUnsuccessful()
        {
            var roleManager = GetMockedRoleManager();

            roleManager.Setup(r => r.DeleteAsync(It.IsAny<IdentityRole>())).ReturnsAsync(IdentityResult.Failed());

            var controller = new RoleController {RoleManager = roleManager.Object};

            var result = await controller.DeleteRole(string.Empty);

            Assert.AreSame(typeof(BadRequestResult), result.GetType());
        }

        private static Mock<RoleManager<IdentityRole, int>> GetMockedRoleManager()
        {
            var connectionHelper = new Mock<IDbConnection>();
            var roleStore = new Mock<RoleStore>(connectionHelper.Object);
            var roleManager = new Mock<RoleManager<IdentityRole, int>>(roleStore.Object);

            return roleManager;
        }
    }
}