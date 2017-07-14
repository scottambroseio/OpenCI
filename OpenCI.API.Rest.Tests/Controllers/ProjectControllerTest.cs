using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCI.API.Rest.Controllers;
using System.Web.Http.Results;
using Moq;
using OpenCI.Business.Models;
using OpenCI.Contracts.Business;
using System.Threading.Tasks;
using System;

namespace OpenCI.API.Rest.Tests.Controllers
{
    [TestClass]
    public class ProjectControllerTest
    {
        [TestMethod]
        public async Task Get_ShouldReturnTheCorrectProject()
        {
            var operations = new Mock<IProjectOperations>();
            var expected = new ProjectModel
            {
                Id = 1,
                Name = "Test"
            };
            var guid = Guid.NewGuid();

            operations.Setup(o => o.GetProject(guid)).Returns(Task.FromResult(expected));

            var controller = new ProjectController(operations.Object);

            var result = await controller.Get(guid) as OkNegotiatedContentResult<ProjectModel>;

            Assert.AreEqual(expected, result.Content);
        }

        [TestMethod]
        public async Task Get_ShouldReturnNullIfTheProjectDoesntExist()
        {
            var operations = new Mock<IProjectOperations>();
            var guid = Guid.NewGuid();

            operations.Setup(o => o.GetProject(guid)).Returns(Task.FromResult<ProjectModel>(null));

            var controller = new ProjectController(operations.Object);

            var result = await controller.Get(guid) as OkNegotiatedContentResult<ProjectModel>;

            Assert.AreEqual(null, result);
        }
    }
}
